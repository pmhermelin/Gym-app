using System;

namespace GymApp
{
    public partial class GymSystem
    {
        // מערך המתאמנים: 80 תאים (לפי ההחלטות הסופיות).
        // מאותחל כאן ולא בבנאי שב-GymSystem.cs, כדי שכל תחום יחזיק את המערכים שלו
        // ולא ייווצרו קונפליקטים בקובץ המשותף.
        private Trainee[] trainees = new Trainee[80];

        // מחזירה את האינדקס הראשון הפנוי (null) במערך trainees, או -1 אם מלא
        private int FindFreeTraineeIndex()
        {
            for (int i = 0; i < trainees.Length; i++)
            {
                if (trainees[i] == null)
                    return i;
            }
            return -1;
        }

        // מחפשת מתאמן לפי מזהה חשבון ה-User שלו (TRA0001); מחזירה null אם לא נמצא
        private Trainee? FindTraineeById(string id)
        {
            for (int i = 0; i < trainees.Length; i++)
            {
                if (trainees[i] != null && trainees[i].GetUser().GetId() == id)
                    return trainees[i];
            }
            return null;
        }

        // REQ-005: הוספת מתאמן חדש. יוצרת חשבון User מסוג Trainee ומקשרת אותו ל-Trainee עם מאמן פעיל.
        // מחזירה true בהצלחה, false בכל כשל - כל הבדיקות נעשות לפני יצירת החשבון, כדי לא להשאיר חשבון User יתום.
        public bool AddTrainee(string name, string phone, string email, string trainerId, out string initialPassword)
        {
            initialPassword = "";

            // 1. מאתרים את המאמן: חייב להיות עובד קיים, פעיל, וחשבונו מסוג Trainer
            if (trainerId == null || trainerId.Trim() == "")
                return false;
            Employee? trainer = FindEmployeeByUserId(trainerId.Trim());
            if (trainer == null || !trainer.IsActive())
                return false;
            if (trainer.GetUser().GetUserType() != "Trainer")
                return false;

            // 2. יש מקום פנוי גם ב-users וגם ב-trainees? בודקים לפני יצירת החשבון
            if (FindFreeUserIndex() == -1 || FindFreeTraineeIndex() == -1)
                return false;

            // 3. רק עכשיו יוצרים את חשבון ה-User, מסוג Trainee (בדיקות שם/טלפון/דוא"ל נעשות ב-CreateAccount)
            User? user = CreateAccount("Trainee", name, phone, email, out initialPassword);
            if (user == null)
                return false;

            // 4. יוצרים ושומרים את רשומת ה-Trainee, משויכת למאמן
            int index = FindFreeTraineeIndex();
            Trainee trainee = new Trainee(user, trainer);
            trainees[index] = trainee;
            return true;
        }
    }
}
