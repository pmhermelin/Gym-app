using System;

namespace GymApp
{
    public partial class GymSystem
    {
        // מערך רשומות ההכנסה: 120 תאים - עד עשר שנות פעילות חודשית (לפי ההחלטות הסופיות)
        private RevenueRecord[] revenueRecords = new RevenueRecord[120];

        // מחפשת רשומת הכנסה קיימת התואמת לחודש ולשנה; מחזירה null אם לא נמצאה
        private RevenueRecord? FindRevenueRecord(int month, int year)
        {
            for (int i = 0; i < revenueRecords.Length; i++)
            {
                if (revenueRecords[i] != null && revenueRecords[i].Matches(month, year))
                    return revenueRecords[i];
            }
            return null;
        }

        // מחזירה את האינדקס הראשון הפנוי (null) במערך revenueRecords, או -1 אם מלא
        private int FindFreeRevenueRecordIndex()
        {
            for (int i = 0; i < revenueRecords.Length; i++)
            {
                if (revenueRecords[i] == null)
                    return i;
            }
            return -1;
        }

        // REQ-003: שליפת דוח הכנסות קיים לחודש ולשנה נתונים.
        // מחזירה את הרשומה אם קיימת, או null אם לא - לעולם לא יוצרת רשומה חדשה בזמן צפייה בלבד.
        public RevenueRecord? GetMonthlyRevenue(int month, int year)
        {
            if (month < 1 || month > 12)
                return null;
            if (year <= 0)
                return null;

            return FindRevenueRecord(month, year);
        }

        // עוזרת פנימית ל-CreateOrRenewMembership (KAN-12, יוסי): מתעדת מכירה/חידוש מנוי ברשומת
        // ההכנסות של חודש הפעולה. אם אין רשומה לחודש הזה - יוצרת אחת; אם יש - מוסיפה אליה.
        private bool RecordMembershipSale(double price, DateTime saleDate)
        {
            int month = saleDate.Month;
            int year = saleDate.Year;

            RevenueRecord? record = FindRevenueRecord(month, year);
            if (record != null)
            {
                record.AddSale(price);
                return true;
            }

            int index = FindFreeRevenueRecordIndex();
            if (index == -1)
                return false;

            RevenueRecord newRecord = new RevenueRecord(month, year);
            revenueRecords[index] = newRecord;
            newRecord.AddSale(price);
            return true;
        }
    }
}