using System;

namespace GymApp
{
    public class Employee
    {
        // שדות פרטיים: אין סטטוס נפרד - הכל מגיע דרך חשבון ה-User המקושר
        private User user;
        private string role;
        private DateTime startDate;

        public Employee(User user, string role, DateTime startDate)
        {
            this.user = user;
            this.role = role;
            this.startDate = startDate;
        }

        public User GetUser() { return user; }
        public string GetRole() { return role; }
        public DateTime GetStartDate() { return startDate; }

        // מבוססת על סטטוס חשבון ה-User, לא על שדה נפרד
        public bool IsActive()
        {
            return user.IsActive();
        }

        // משביתה את חשבון ה-User המקושר. אין שדה סטטוס נוסף ב-Employee עצמה
        public void Deactivate()
        {
            user.Deactivate();
        }
    }
}