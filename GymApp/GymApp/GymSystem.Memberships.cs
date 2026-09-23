using System;

namespace GymApp
{
    public partial class GymSystem
    {
        // מערך המנויים: 200 תאים, כולל חידושים היסטוריים (לפי ההחלטות הסופיות).
        // מאותחל כאן ולא בבנאי שב-GymSystem.cs, כדי לא לגעת בקובץ המשותף.
        private Membership[] memberships = new Membership[200];

        // מחזירה את האינדקס הראשון הפנוי (null) במערך memberships, או -1 אם מלא
        private int FindFreeMembershipIndex()
        {
            for (int i = 0; i < memberships.Length; i++)
            {
                if (memberships[i] == null)
                    return i;
            }
            return -1;
        }

        // מחפשת במערך memberships את המנוי התקף של המתאמן בתאריך המבוקש; מחזירה null אם אין.
        // מצב המנוי אינו נשמר ב-Trainee - הוא נגזר תמיד מסריקת המערך (מקור אמת יחיד).
        private Membership? GetMembershipFor(Trainee trainee, DateTime date)
        {
            for (int i = 0; i < memberships.Length; i++)
            {
                if (memberships[i] != null
                    && memberships[i].GetTrainee() == trainee
                    && memberships[i].IsActiveOn(date))
                    return memberships[i];
            }
            return null;
        }

        // מסמנת כ-Expired מנויים פעילים שתאריך הסיום שלהם כבר עבר.
        // לא מוחקת דבר - המנויים הקודמים נשארים במערך כהיסטוריה.
        private void RefreshMembershipStatuses(DateTime today)
        {
            for (int i = 0; i < memberships.Length; i++)
            {
                if (memberships[i] != null
                    && memberships[i].GetStatus() == "Active"
                    && memberships[i].GetEndDate() < today)
                    memberships[i].MarkExpired();
            }
        }

        // בודקת אם למתאמן יש מנוי שאינו מבוטל, שהטווח שלו חופף לטווח החדש [startDate, endDate].
        // שני טווחים חופפים אם כל אחד מתחיל לפני שהשני מסתיים.
        private bool MembershipOverlaps(Trainee trainee, DateTime startDate, DateTime endDate)
        {
            for (int i = 0; i < memberships.Length; i++)
            {
                Membership m = memberships[i];
                if (m == null || m.GetTrainee() != trainee)
                    continue;
                if (m.GetStatus() == "Cancelled")
                    continue;

                if (startDate <= m.GetEndDate() && m.GetStartDate() <= endDate)
                    return true;
            }
            return false;
        }
    }
}
