using System;

namespace GymApp
{
    public partial class GymSystem
    {
        // מערך המנויים: 200 תאים, כולל חידושים היסטוריים (לפי ההחלטות הסופיות).
        // מאותחל כאן ולא בבנאי שב-GymSystem.cs, כדי לא לגעת בקובץ המשותף.
        private Membership[] memberships = new Membership[200];

        // מונה ליצירת מזהי מנוי (MEM0001, MEM0002 ...). הקידומת MEM אושרה ע"י הצוות
        private int nextMembershipId = 1;

        // REQ-005: יצירה או חידוש של מנוי למתאמן קיים.
        // סוג המנוי קובע את המחיר ואת משך הזכאות (לפי ההחלטות הסופיות; שמות הסוגים אושרו ע"י הצוות):
        // Monthly = 200 ל-30 יום, Quarterly = 540 ל-90 יום, Yearly = 1800 ל-365 יום.
        // מחזירה true בהצלחה, false בכל כשל - וכל הבדיקות נעשות לפני שמשהו נשמר,
        // כך שלא ייתכן מנוי בלי רישום הכנסה או רישום הכנסה בלי מנוי.
        public bool CreateOrRenewMembership(string traineeId, string membershipType, DateTime startDate)
        {
            // 1. מאתרים מתאמן קיים ופעיל
            if (traineeId == null || traineeId.Trim() == "")
                return false;
            Trainee? trainee = FindTraineeById(traineeId.Trim());
            if (trainee == null || !trainee.IsActive())
                return false;

            // 2. סוג המנוי קובע מחיר ומשך. סוג לא מוכר - כשל
            double price;
            int durationDays;
            if (membershipType == "Monthly")
            {
                price = 200;
                durationDays = 30;
            }
            else if (membershipType == "Quarterly")
            {
                price = 540;
                durationDays = 90;
            }
            else if (membershipType == "Yearly")
            {
                price = 1800;
                durationDays = 365;
            }
            else
            {
                return false;
            }

            // 3. תאריכים: חייב להיות תאריך התחלה, ותאריך הסיום מחושב ממנו
            if (startDate == default(DateTime))
                return false;
            DateTime endDate = startDate.AddDays(durationDays);

            // 4. בדיקות מחיר ותאריכים (לפי מסמך העיצוב, גם אם הערכים קבועים)
            if (price <= 0 || endDate <= startDate)
                return false;

            // 5. יש מקום פנוי במערך המנויים?
            int membershipIndex = FindFreeMembershipIndex();
            if (membershipIndex == -1)
                return false;

            // 6. מנויים שתוקפם עבר מסומנים Expired, ואז בודקים שאין מנוי חופף לאותו מתאמן
            RefreshMembershipStatuses(DateTime.Now);
            if (MembershipOverlaps(trainee, startDate, endDate))
                return false;

            // 7. רישום ההכנסה יהיה בחודש של היום. אם עוד אין רשומה לחודש הזה,
            //    חייב להיות מקום פנוי ב-revenueRecords - בודקים לפני שיוצרים את המנוי
            DateTime saleDate = DateTime.Now;
            if (FindRevenueRecord(saleDate.Month, saleDate.Year) == null && FindFreeRevenueRecordIndex() == -1)
                return false;

            // 8. רק עכשיו יוצרים ושומרים את המנוי
            string id = "MEM" + nextMembershipId.ToString("D4");
            nextMembershipId++;
            Membership membership = new Membership(id, trainee, membershipType, price, startDate, endDate, saleDate);
            memberships[membershipIndex] = membership;

            // 9. רישום המכירה פעם אחת בלבד (RecordMembershipSale של KAN-10)
            RecordMembershipSale(price, saleDate);
            return true;
        }

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
