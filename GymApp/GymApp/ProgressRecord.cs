using System;

namespace GymApp
{
    public class ProgressRecord
    {
        // שדות פרטיים: רשומת התקדמות של מתאמן בתאריך מסוים (מסמך העיצוב 6.13).
        // שמות המדדים נשמרים פעם אחת בלבד ב-GymSystem (metricNames), וכאן נשמרים רק הערכים באותו סדר
        private string id;               // מזהה רשומה, פורמט PRG0001 (הקידומת PRG אושרה ע"י הצוות)
        private Trainee trainee;         // המתאמן
        private Employee trainer;        // המאמן שתיעד
        private DateTime recordDate;     // תאריך המדידה - אינו עתידי, ואינו משתנה בעדכון
        private double[] metricValues;   // ערכי המדדים, metricValues[i] מתאים ל-metricNames[i]
        private int metricCount;         // מספר המדדים ברשומה
        private string note;             // הערת המאמן

        // בנאי: נקרא רק אחרי שכל הנתונים נבדקו ב-GymSystem.
        // הערכים מועתקים למערך חדש, כדי ששינוי במערך שהועבר מבחוץ לא ישנה את הרשומה
        public ProgressRecord(string id, Trainee trainee, Employee trainer, DateTime recordDate,
                              double[] values, string note)
        {
            this.id = id;
            this.trainee = trainee;
            this.trainer = trainer;
            this.recordDate = recordDate.Date;
            this.metricCount = values.Length;
            this.metricValues = new double[metricCount];
            for (int i = 0; i < metricCount; i++)
                this.metricValues[i] = values[i];
            this.note = note;
        }

        // מתודות קריאה
        public string GetId() { return id; }
        public Trainee GetTrainee() { return trainee; }
        public Employee GetTrainer() { return trainer; }
        public DateTime GetRecordDate() { return recordDate; }
        public string GetNote() { return note; }
        public int GetMetricCount() { return metricCount; }

        // מחזירה ערך מדד לפי אינדקס; באינדקס לא תקין מוחזר 0
        public double GetMetricValue(int index)
        {
            if (index < 0 || index >= metricCount)
                return 0;
            return metricValues[index];
        }

        // עדכון ערכי המדדים וההערה באותו אובייקט. התאריך, המתאמן והמאמן לא משתנים.
        // נכשלת (false) אם מספר הערכים שונה ממספר המדדים ברשומה
        public bool UpdateMetrics(double[] values, string note)
        {
            if (values == null || values.Length != metricCount)
                return false;

            for (int i = 0; i < metricCount; i++)
                metricValues[i] = values[i];
            this.note = note;
            return true;
        }

        // מציגה את פרטי הרשומה הבסיסיים. שמות המדדים מותאמים לערכים בעת ההצגה ב-Program
        // (GetMetricName(i) של GymSystem לצד GetMetricValue(i)), לכן הם לא מופיעים כאן
        public override string ToString()
        {
            return "Record: " + id + " | Date: " + recordDate.ToString("yyyy-MM-dd")
                   + " | Trainee: " + trainee.GetUser().GetFullName()
                   + " | Trainer: " + trainer.GetUser().GetFullName()
                   + " | Note: " + note;
        }
    }
}
