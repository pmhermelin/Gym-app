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
        // REQ-004: השבתת עובד קיים (מחיקה לוגית - דרך חשבון ה-User המקושר, בלי למחוק את הרשומה).
        // מחזירה true בהצלחה, false אם העובד לא נמצא, כבר לא פעיל, או שיש לו עדיין מתאמנים פעילים משויכים.
        public bool DeactivateEmployee(string employeeId)
        {
            if (employeeId == null || employeeId.Trim() == "")
                return false;

            Employee? employee = FindEmployeeByUserId(employeeId.Trim());
            if (employee == null || !employee.IsActive())
                return false;

            // אסור להשבית מאמן שיש לו עדיין מתאמנים פעילים משויכים - קודם יש לשייך אותם מחדש (ReassignTrainee)
            if (HasAssignedActiveTrainees(employee))
                return false;

            // TODO: לפי מסמך העיצוב יש לבדוק גם שאין למאמן שיעורים עתידיים פעילים.
            // עדיין לא ניתן לממש - מחלקת GymClass (KAN-14/15, נתנאל) עוד לא קיימת בקוד. להשלים כשזה יתמזג.

            employee.Deactivate();
            return true;
        }

        // עוזרת ל-DeactivateEmployee: בודקת אם יש מתאמן פעיל כלשהו שהמאמן הנתון משויך אליו
        private bool HasAssignedActiveTrainees(Employee trainer)
        {
            for (int i = 0; i < trainees.Length; i++)
            {
                if (trainees[i] != null && trainees[i].GetAssignedTrainer() == trainer && trainees[i].IsActive())
                    return true;
            }
            return false;
        }

        // REQ-004: שיוך מתאמן קיים למאמן אחר (למשל אחרי שהמאמן הנוכחי הושבת).
        // מחזירה true בהצלחה, false בכל כשל - כולל אם המתאמן לא פעיל, המאמן החדש לא תקין, או שהוא כבר המאמן הנוכחי.
        public bool ReassignTrainee(string traineeId, string newTrainerId)
        {
            if (traineeId == null || traineeId.Trim() == "")
                return false;
            if (newTrainerId == null || newTrainerId.Trim() == "")
                return false;

            // 1. המתאמן חייב להיות קיים ופעיל
            Trainee? trainee = FindTraineeById(traineeId.Trim());
            if (trainee == null || !trainee.IsActive())
                return false;

            // 2. המאמן החדש חייב להיות עובד קיים, פעיל, וחשבונו מסוג Trainer (אותה בדיקה כמו ב-AddTrainee)
            Employee? newTrainer = FindEmployeeByUserId(newTrainerId.Trim());
            if (newTrainer == null || !newTrainer.IsActive())
                return false;
            if (newTrainer.GetUser().GetUserType() != "Trainer")
                return false;

            // 3. אין טעם "לשייך מחדש" לאותו מאמן שכבר משויך
            if (trainee.GetAssignedTrainer() == newTrainer)
                return false;

            // 4. רק עכשיו מעדכנים את השיוך
            trainee.SetAssignedTrainer(newTrainer);
            return true;
        }
    }
}
