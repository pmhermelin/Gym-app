using System;

namespace GymApp
{
    public partial class GymSystem
    {
        // מערך החשבונות: 100 תאים (לפי ההחלטות הסופיות)
        private User[] users;

        // מונים ליצירת מזהים
        private int nextEmployeeId;
        private int nextTraineeId;

        // מחולל אקראי (ישמש ליצירת סיסמאות)
        private Random random;

        // המשתמש המחובר כרגע, או null אם אף אחד לא מחובר
        private User? currentUser;

        public GymSystem()
        {
            users = new User[100];
            nextEmployeeId = 1;
            nextTraineeId = 1;
            random = new Random();
        }

        // מחזירה את האינדקס הראשון הפנוי (null) במערך, או -1 אם המערך מלא
        private int FindFreeUserIndex()
        {
            for (int i = 0; i < users.Length; i++)
            {
                if (users[i] == null)
                    return i;
            }
            return -1;
        }

        // מחפשת משתמש לפי מזהה מדויק; מחזירה null אם לא נמצא
        private User? FindUserById(string id)
        {
            for (int i = 0; i < users.Length; i++)
            {
                if (users[i] != null && users[i].GetId() == id)
                    return users[i];
            }
            return null;
        }

        // מפיקה מזהה: קידומת + מונה בן 4 ספרות (EMP0001 / TRA0001)
        private string? GenerateUserId(string type)
        {
            if (type == "Trainer")
            {
                string id = "EMP" + nextEmployeeId.ToString("D4");
                nextEmployeeId++;
                return id;
            }
            if (type == "Trainee")
            {
                string id = "TRA" + nextTraineeId.ToString("D4");
                nextTraineeId++;
                return id;
            }
            return null;
        }
    }
}