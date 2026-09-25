namespace GymApp
{
    public class RevenueRecord
    {
        // שדות פרטיים: כל רשומה מייצגת חודש קלנדרי אחד, עם סכום מצטבר ומונה מכירות
        private int month;
        private int year;
        private double totalRevenue;
        private int membershipsSold;

        // בנאי: רשומה חדשה תמיד מתחילה ריקה - המכירה הראשונה מתווספת מיד אחרי היצירה
        public RevenueRecord(int month, int year)
        {
            this.month = month;
            this.year = year;
            this.totalRevenue = 0;
            this.membershipsSold = 0;
        }

        // מתודות קריאה
        public int GetMonth() { return month; }
        public int GetYear() { return year; }
        public double GetTotalRevenue() { return totalRevenue; }
        public int GetMembershipsSold() { return membershipsSold; }

        // בודקת אם הרשומה הזו שייכת לחודש ולשנה הנתונים
        public bool Matches(int month, int year)
        {
            return this.month == month && this.year == year;
        }

        // מוסיפה מכירה/חידוש בודד לרשומה - מעדכנת גם את הסכום וגם את המונה
        public void AddSale(double price)
        {
            totalRevenue += price;
            membershipsSold++;
        }

        // מציגה את הרשומה לדוח
        public override string ToString()
        {
            return month + "/" + year + " | הכנסות: " + totalRevenue + " ₪ | מנויים שנמכרו: " + membershipsSold;
        }
    }
}