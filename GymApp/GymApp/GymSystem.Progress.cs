using System;

namespace GymApp
{
    public partial class GymSystem
    {
        // מערך רשומות ההתקדמות: 1000 תאים (לפי ההחלטות הסופיות).
        // מאותחל כאן ולא בבנאי שב-GymSystem.cs, כדי לא לגעת בקובץ המשותף.
        private ProgressRecord[] progressRecords = new ProgressRecord[1000];

        // שמות המדדים הקבועים, פעם אחת בלבד במערכת (עמוד "החלטות סופיות": משקל, אחוז שומן, היקף מותניים).
        // נכתבים באנגלית כמו שאר המחרוזות בקוד. metricNames[i] מתאים ל-GetMetricValue(i) בכל רשומה
        private string[] metricNames = { "Weight (kg)", "Body Fat (%)", "Waist (cm)" };

        // אינדקס המדד "אחוז שומן" - הערך שלו חייב להיות עד 100
        private const int BodyFatMetricIndex = 1;

        // מונה ליצירת מזהי רשומות (PRG0001, PRG0002 ...). הקידומת PRG אושרה ע"י הצוות
        private int nextProgressRecordId = 1;

        // מחזירה את שם המדד לפי אינדקס תקין, או null אם האינדקס לא קיים.
        // Program עוברת על i = 0, 1, 2 ... עד שמוחזר null, ומציגה את השם לצד GetMetricValue(i)
        public string? GetMetricName(int index)
        {
            if (index < 0 || index >= metricNames.Length)
                return null;
            return metricNames[index];
        }

        // מחזירה את האינדקס הראשון הפנוי (null) במערך progressRecords, או -1 אם מלא
        private int FindFreeProgressRecordIndex()
        {
            for (int i = 0; i < progressRecords.Length; i++)
            {
                if (progressRecords[i] == null)
                    return i;
            }
            return -1;
        }

        // מחפשת רשומת התקדמות לפי מזהה מדויק; מחזירה null אם לא נמצאה
        private ProgressRecord? FindProgressRecordById(string id)
        {
            if (id == null)
                return null;

            for (int i = 0; i < progressRecords.Length; i++)
            {
                if (progressRecords[i] != null && progressRecords[i].GetId() == id.Trim())
                    return progressRecords[i];
            }
            return null;
        }

        // בודקת את ערכי המדדים: מספר הערכים שווה למספר השמות ב-metricNames,
        // כל ערך הוא מספר חיובי (לא NaN ולא אינסוף), ואחוז שומן אינו עולה על 100
        private bool ValidateMetricValues(double[] values)
        {
            if (values == null || values.Length != metricNames.Length)
                return false;

            for (int i = 0; i < values.Length; i++)
            {
                if (double.IsNaN(values[i]) || double.IsInfinity(values[i]))
                    return false;
                if (values[i] <= 0)
                    return false;
            }
            return values[BodyFatMetricIndex] <= 100;
        }

        // REQ-010: הוספת רשומת התקדמות למתאמן משויך.
        // מחזירה true בהצלחה, false בכל כשל - ואז לא נשמרת רשומה
        public bool AddProgressRecord(Employee trainer, Trainee trainee, DateTime recordDate,
                                      double[] values, string note)
        {
            // 1. המאמן והמתאמן פעילים והמתאמן משויך למאמן
            if (!IsAssignedToTrainer(trainee, trainer))
                return false;

            // 2. התאריך אינו עתידי, והערכים תקינים
            if (recordDate.Date > DateTime.Today)
                return false;
            if (!ValidateMetricValues(values))
                return false;

            // 3. יש מקום במערך
            int index = FindFreeProgressRecordIndex();
            if (index == -1)
                return false;

            // 4. יוצרים מזהה ורשומה מקומית, ורק אז שומרים בתא הפנוי.
            // המתאמן והמאמן נשמרים כהפניות לאובייקטים ולא כטקסט
            string id = "PRG" + nextProgressRecordId.ToString("D4");
            nextProgressRecordId++;
            string cleanNote = note == null ? "" : note.Trim();
            progressRecords[index] = new ProgressRecord(id, trainee, trainer, recordDate, values, cleanNote);
            return true;
        }

        // REQ-010: עדכון רשומה קיימת - רק ערכי המדדים וההערה, באותו אובייקט.
        // אין לשנות תאריך או בעלות, ואין ליצור רשומה חדשה
        public bool UpdateProgressRecord(Employee trainer, Trainee trainee, string recordId,
                                         double[] values, string note)
        {
            // 1. המתאמן עדיין משויך למאמן המחובר
            if (!IsAssignedToTrainer(trainee, trainer))
                return false;

            // 2. הרשומה קיימת ושייכת למתאמן הנבחר
            ProgressRecord? record = FindProgressRecordById(recordId);
            if (record == null || record.GetTrainee() != trainee)
                return false;

            // 3. הערכים החדשים תקינים
            if (!ValidateMetricValues(values))
                return false;

            // 4. העדכון עצמו דרך UpdateMetrics
            string cleanNote = note == null ? "" : note.Trim();
            return record.UpdateMetrics(values, cleanNote);
        }

        // ממיינת את count התאים הראשונים במערך לפי recordDate מהישן לחדש (Bubble Sort, מסמך העיצוב 7.20).
        // מפסיקה מוקדם אם במעבר שלם לא הייתה החלפה
        private void SortProgressByDate(ProgressRecord[] results, int count)
        {
            for (int pass = 0; pass < count - 1; pass++)
            {
                bool swapped = false;
                for (int i = 0; i < count - 1 - pass; i++)
                {
                    if (results[i].GetRecordDate() > results[i + 1].GetRecordDate())
                    {
                        ProgressRecord temp = results[i];
                        results[i] = results[i + 1];
                        results[i + 1] = temp;
                        swapped = true;
                    }
                }
                if (!swapped)
                    break;
            }
        }
    }
}
