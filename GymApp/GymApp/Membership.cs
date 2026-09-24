using System;

namespace GymApp
{
    public class Membership
    {
        // שדות פרטיים: כל רכישה או חידוש הם אובייקט חדש, כדי לשמור היסטוריה
        private string id;
        private Trainee trainee;       // בעל המנוי
        private string type;           // Monthly / Quarterly / Yearly
        private double price;          // מחיר חיובי
        private DateTime startDate;    // תחילת הזכאות
        private DateTime endDate;      // סיום הזכאות
        private DateTime saleDate;     // מועד היצירה/החידוש - לצורך רישום ההכנסה
        private string status;         // Active / Expired / Cancelled

        // בנאי: נקרא רק אחרי שכל הבדיקות ב-GymSystem עברו. מנוי חדש תמיד מתחיל כ-Active
        public Membership(string id, Trainee trainee, string type, double price,
                          DateTime startDate, DateTime endDate, DateTime saleDate)
        {
            this.id = id;
            this.trainee = trainee;
            this.type = type;
            this.price = price;
            this.startDate = startDate;
            this.endDate = endDate;
            this.saleDate = saleDate;
            this.status = "Active";
        }

        // מתודות קריאה
        public string GetId() { return id; }
        public Trainee GetTrainee() { return trainee; }
        public string GetMembershipType() { return type; }
        public double GetPrice() { return price; }
        public DateTime GetStartDate() { return startDate; }
        public DateTime GetEndDate() { return endDate; }
        public DateTime GetSaleDate() { return saleDate; }
        public string GetStatus() { return status; }

        // בודקת אם המנוי תקף בתאריך נתון: סטטוס Active והתאריך בתוך טווח הזכאות
        public bool IsActiveOn(DateTime date)
        {
            return status == "Active" && date >= startDate && date <= endDate;
        }

        // שינוי סטטוס מבוקר: מנוי שתוקפו עבר נשמר כהיסטוריה ומסומן Expired (לא נמחק)
        public void MarkExpired()
        {
            if (status == "Active")
                status = "Expired";
        }

        // שינוי סטטוס מבוקר: ביטול לוגי, הרשומה נשארת במערך
        public void Cancel()
        {
            if (status == "Active")
                status = "Cancelled";
        }

        // מציגה סוג, מחיר, תקופה וסטטוס
        public override string ToString()
        {
            return id + " | " + type + " | " + price + " | "
                   + startDate.ToString("yyyy-MM-dd") + " - " + endDate.ToString("yyyy-MM-dd")
                   + " | " + status;
        }
    }
}
