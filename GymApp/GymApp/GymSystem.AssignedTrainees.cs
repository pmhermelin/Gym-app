using System;

namespace GymApp
{
    public partial class GymSystem
    {
        // מאתרת את רשומת ה-Employee המשויכת לחשבון User נתון (למשל currentUser של מאמן מחובר).
        // מחזירה null אם המשתמש אינו עובד.
        private Employee? FindEmployeeByUser(User user)
        {
            if (user == null)
                return null;

            for (int i = 0; i < employees.Length; i++)
            {
                if (employees[i] != null && employees[i].GetUser() == user)
                    return employees[i];
            }
            return null;
        }

        // בודקת שהמאמן רשאי לגשת למידע המקצועי של המתאמן: שניהם קיימים ופעילים,
        // והמאמן המשויך למתאמן הוא בדיוק המאמן הנתון (השוואת אובייקטים, לא טקסט).
        // נבדקת מחדש בכל פעולה מקצועית (התקדמות, תוכנית אימונים, תפריט, פגישה),
        // כדי ששינוי שיוך בינתיים (ReassignTrainee) לא ישאיר למאמן הקודם גישה.
        private bool IsAssignedToTrainer(Trainee trainee, Employee trainer)
        {
            if (trainee == null || trainer == null)
                return false;
            if (!trainee.IsActive() || !trainer.IsActive())
                return false;

            return trainee.GetAssignedTrainer() == trainer;
        }

        // REQ-009: חיפוש מתאמנים המשויכים למאמן, לפי מזהה מדויק או שם מלא/חלקי.
        // מחזירה את מספר התוצאות שנכתבו ל-results (עד לגבול המערך). אף פעם לא משנה נתונים.
        // מתאמן לא פעיל או שאינו משויך למאמן הזה לא מוחזר, ולכן לא ניתן לבחור אותו לעדכון.
        public int SearchAssignedTrainees(Employee trainer, string text, Trainee[] results)
        {
            // 1. המאמן חייב להיות עובד פעיל מסוג Trainer, ויש מערך תוצאות
            if (trainer == null || results == null)
                return 0;
            if (!trainer.IsActive() || trainer.GetUser().GetUserType() != "Trainer")
                return 0;

            // טקסט ריק (או null) = בלי סינון טקסט: מוצגים כל המתאמנים המשויכים
            string searchText = text == null ? "" : text.Trim();

            int count = 0;
            for (int i = 0; i < trainees.Length; i++)
            {
                // 2. מדלגים על תאים ריקים ועל מתאמנים שאינם משויכים/פעילים
                Trainee? trainee = trainees[i];
                if (trainee == null || !IsAssignedToTrainer(trainee, trainer))
                    continue;

                // 3. התאמת טקסט: מזהה מדויק או שם חלקי - אותה בדיקה כמו ב-SearchUsers (TextMatches)
                if (!TextMatches(trainee.GetUser(), searchText))
                    continue;

                // 4. כותבים עד גבול מערך התוצאות
                if (count == results.Length)
                    break;
                results[count] = trainee;
                count++;
            }

            return count;
        }
    }
}
