using System;

namespace GymApp
{
    public partial class GymSystem
    {
        // מערך השיעורים: 200 תאים (לפי ההחלטות הסופיות).
        // מאותחל כאן ולא בבנאי שב-GymSystem.cs, כדי לא לגעת בקובץ המשותף
        // (תבנית זהה ל-trainees ב-GymSystem.Trainees.cs ול-memberships ב-GymSystem.Memberships.cs).
        private GymClass[] classes = new GymClass[200];

        // מונה ליצירת מזהי שיעור (CLS0001, CLS0002 ...). הקידומת CLS אושרה ב"החלטות סופיות".
        private int nextClassId = 1;

        // מחזירה את האינדקס הראשון הפנוי (null) במערך classes, או -1 אם מלא
        private int FindFreeClassIndex()
        {
            for (int i = 0; i < classes.Length; i++)
            {
                if (classes[i] == null)
                    return i;
            }
            return -1;
        }

        // REQ-007 (KAN-48): הוספת שיעור עתידי.
        // מחזירה את השיעור החדש בהצלחה, null בכל כשל - לפי הפסאודו-קוד המדויק בסעיף 7.7.
        public GymClass? AddClass(string name, string activityType, DateTime start, int durationMinutes,
                                   string trainerId, int capacity)
        {
            // 1. בדיקות קלט בסיסיות: שדות חובה, מועד עתידי, משך וקיבולת חיוביים (7.7)
            if (name == null || name.Trim() == "")
                return null;
            if (activityType == null || activityType.Trim() == "")
                return null;
            if (start <= DateTime.Now)
                return null;
            if (durationMinutes <= 0)
                return null;
            if (capacity <= 0)
                return null;
            if (trainerId == null || trainerId.Trim() == "")
                return null;

            // 2. יש מקום פנוי במערך classes? נבדק לפני איתור המאמן, לפי סדר הבדיקות בפסאודו-קוד של 7.7
            int index = FindFreeClassIndex();
            if (index == -1)
                return null;

            // 3. מאמן פעיל, קיים ומסוג Trainer - אותה בדיקה כמו ב-AddTrainee/ReassignTrainee
            Employee? trainer = FindEmployeeByUserId(trainerId.Trim());
            if (trainer == null || !trainer.IsActive())
                return null;
            if (trainer.GetUser().GetUserType() != "Trainer")
                return null;

            // 4. רק עכשיו יוצרים מזהה ושומרים את השיעור (7.20: GenerateEntityId, קידומת CLS)
            string id = "CLS" + nextClassId.ToString("D4");
            nextClassId++;

            GymClass gymClass = new GymClass(id, name.Trim(), activityType.Trim(), start, durationMinutes, trainer, capacity);
            classes[index] = gymClass;
            return gymClass;
        }
    }
}