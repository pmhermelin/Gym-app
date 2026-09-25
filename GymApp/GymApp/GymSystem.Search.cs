using System;

namespace GymApp
{
    public partial class GymSystem
    {
        // REQ-006: חיפוש משתמשים לפי טקסט (מזהה מדויק או שם חלקי, ללא תלות רישיות)
        // ומסננים אופציונליים (type, accountStatus, membershipStatus). "All" = בלי סינון באותו שדה.
        // אף פעם לא משנה נתונים. מחזירה כמה תוצאות נכתבו ל-results (עד לגבול המערך).
        public int SearchUsers(string text, string type, string accountStatus, string membershipStatus, User[] results)
        {
            int count = 0;

            for (int i = 0; i < users.Length; i++)
            {
                User? user = users[i];
                if (user == null)
                    continue;

                bool textOk = TextMatches(user, text);
                bool typeOk = type == "All" || user.GetUserType() == type;
                bool statusOk = accountStatus == "All" || user.GetStatus() == accountStatus;
                bool membershipOk = MembershipStatusMatches(user, membershipStatus);

                if (textOk && typeOk && statusOk && membershipOk && count < results.Length)
                {
                    results[count] = user;
                    count++;
                }
            }

            return count;
        }

        // התאמת טקסט: מזהה מדויק (ללא תלות רישיות), או הכלה חלקית בשם (גם היא ללא תלות רישיות).
        // טקסט ריק תמיד "מכיל" כל שם - כלומר משמש בפועל כ"בלי סינון טקסט".
        private bool TextMatches(User user, string text)
        {
            if (string.Equals(user.GetId(), text, StringComparison.OrdinalIgnoreCase))
                return true;
            return user.GetFullName().Contains(text, StringComparison.OrdinalIgnoreCase);
        }

        // בודקת את מסנן מצב המנוי. "All" = בלי סינון. משתמש שאינו מתאמן נפסל אוטומטית כשהמסנן פעיל.
        private bool MembershipStatusMatches(User user, string membershipStatus)
        {
            if (membershipStatus == "All")
                return true;

            Trainee? trainee = FindTraineeByUser(user);
            if (trainee == null)
                return false;

            bool hasActiveMembership = GetMembershipFor(trainee, DateTime.Now) != null;
            string currentStatus = hasActiveMembership ? "Active" : "Inactive";
            return currentStatus == membershipStatus;
        }

        // מאתרת את רשומת ה-Trainee המשויכת לחשבון User נתון; מחזירה null אם המשתמש אינו מתאמן
        private Trainee? FindTraineeByUser(User user)
        {
            for (int i = 0; i < trainees.Length; i++)
            {
                if (trainees[i] != null && trainees[i].GetUser() == user)
                    return trainees[i];
            }
            return null;
        }
    }
}
