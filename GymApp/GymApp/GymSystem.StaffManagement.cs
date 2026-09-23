using System;

namespace GymApp
{
    public partial class GymSystem
    {
        // REQ-004: הוספת עובד (מאמן) חדש. יוצרת חשבון User מסוג Trainer ומקשרת אותו ל-Employee.
        // מחזירה true בהצלחה, false בכל כשל - כולל לפני שנוצר חשבון, כדי לא להשאיר חשבון User יתום.
        public bool AddEmployee(string name, string phone, string email, string role, DateTime startDate, out string initialPassword)
        {
            initialPassword = "";

            // 1. בדיקות תקינות בסיסיות
            if (role == null || role.Trim() == "")
                return false;
            if (startDate == default(DateTime))
                return false;

            // 2. יש מקום פנוי גם ב-users וגם ב-employees? בודקים לפני יצירת החשבון
            if (FindFreeUserIndex() == -1 || FindFreeEmployeeIndex() == -1)
                return false;

            // 3. רק עכשיו יוצרים את חשבון ה-User, מסוג Trainer
            User? user = CreateAccount("Trainer", name, phone, email, out initialPassword);
            if (user == null)
                return false;

            // 4. יוצרים ושומרים את רשומת ה-Employee
            int index = FindFreeEmployeeIndex();
            Employee employee = new Employee(user, role.Trim(), startDate);
            employees[index] = employee;
            return true;
        }
    }
}