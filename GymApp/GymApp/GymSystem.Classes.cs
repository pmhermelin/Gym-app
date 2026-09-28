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

        // בודקת חפיפה בין שני טווחי זמן חצי-פתוחים: קוד מדויק מסעיף 4.4 במסמך העיצוב הטכני
        private bool TimesOverlap(DateTime start1, int duration1, DateTime start2, int duration2)
        {
            DateTime end1 = start1.AddMinutes(duration1);
            DateTime end2 = start2.AddMinutes(duration2);
            return start1 < end2 && start2 < end1;
        }

        // בודקת אם למאמן הנתון יש שיעור פעיל שחופף לטווח הזמן המבוקש.
        // ignoredClassId מאפשר להתעלם משיעור מסוים (לשימוש עתידי ב-UpdateClass, KAN-15) - לא בשימוש כרגע ב-AddClass.
        //
        // הערה חשובה על scope: לפי סעיף 7.7/7.20 המתודה אמורה לסרוק גם פגישות (appointments) פעילות של המאמן.
        // מחלקת Appointment עדיין לא קיימת בקוד (שייכת ל-REQ-013 / KAN-20, שטרם מומשה - KAN-14 חוסמת אותה, לא להפך).
        // בדומה לתבנית הקיימת ב-DeactivateEmployee (GymSystem.StaffManagement.cs, TODO לגבי בדיקת שיעורים עתידיים),
        // המתודה סורקת כרגע רק שיעורים. יש להרחיב לסריקת appointments כאשר KAN-20 יתמזג למאסטר.
        private bool TrainerHasConflict(Employee trainer, DateTime start, int durationMinutes, string? ignoredClassId)
        {
            for (int i = 0; i < classes.Length; i++)
            {
                GymClass? existing = classes[i];
                if (existing == null)
                    continue;
                if (existing.GetStatus() != "Active")
                    continue;
                if (existing.GetId() == ignoredClassId)
                    continue;
                if (existing.GetTrainer() != trainer)
                    continue;

                if (TimesOverlap(start, durationMinutes, existing.GetStartDateTime(), existing.GetDurationMinutes()))
                    return true;
            }

            // TODO (תלוי ב-KAN-20 / Appointment - עדיין לא קיים בקוד):
            // להוסיף כאן סריקה של appointments פעילים של אותו trainer, לפי סעיף 7.7/7.20.

            return false;
        }

        // REQ-007 (KAN-48): הוספת שיעור עתידי תוך מניעת חפיפות בלוח המאמן.
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

            // 4. בדיקת חפיפה בלוח הזמנים של המאמן (7.7, 7.20: TrainerHasConflict + TimesOverlap)
            if (TrainerHasConflict(trainer, start, durationMinutes, null))
                return null;

            // 5. רק עכשיו יוצרים מזהה ושומרים את השיעור (7.20: GenerateEntityId, קידומת CLS)
            string id = "CLS" + nextClassId.ToString("D4");
            nextClassId++;

            GymClass gymClass = new GymClass(id, name.Trim(), activityType.Trim(), start, durationMinutes, trainer, capacity);
            classes[index] = gymClass;
            return gymClass;
        }
    }
}