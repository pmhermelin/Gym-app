using System;

namespace GymApp
{
    public partial class GymSystem
    {
        // REQ-019 (וגם הצפייה של המאמן ב-REQ-010): אוספת ל-results את רשומות ההתקדמות של המתאמן,
        // ממוינות לפי תאריך מהישן לחדש. מחזירה את מספר הרשומות שנכתבו (עד גבול המערך).
        // הרשאה: רק המתאמן המחובר עצמו, או המאמן המחובר שהמתאמן משויך אליו. אחרת מוחזר 0.
        // Program מעבירה את מה שהחזירה GetCurrentTrainee() (מתאמן) או מתאמן שנבחר ב-SearchAssignedTrainees (מאמן)
        public int GetTraineeProgress(Trainee trainee, ProgressRecord[] results)
        {
            if (trainee == null || results == null || currentUser == null)
                return 0;

            // 1. בדיקת הרשאה מול המשתמש המחובר
            bool isSelf = trainee.GetUser() == currentUser && trainee.IsActive();
            Employee? trainer = GetCurrentEmployee();
            bool isAssignedTrainer = trainer != null && IsAssignedToTrainer(trainee, trainer);
            if (!isSelf && !isAssignedTrainer)
                return 0;

            // 2. אוספים רק רשומות שמפנות לאותו מתאמן, עד גבול מערך התוצאות
            int count = 0;
            for (int i = 0; i < progressRecords.Length && count < results.Length; i++)
            {
                if (progressRecords[i] != null && progressRecords[i].GetTrainee() == trainee)
                {
                    results[count] = progressRecords[i];
                    count++;
                }
            }

            // 3. ממיינים רק את החלק המלא, מהישן לחדש
            SortProgressByDate(results, count);
            return count;
        }
    }
}
