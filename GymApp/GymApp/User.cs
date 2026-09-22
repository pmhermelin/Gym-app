namespace GymApp
{
    public class User
    {
        // שדות פרטיים: רק המחלקה עצמה יכולה לגעת בהם (אנקפסולציה)
        private string id;
        private string password;
        private string fullName;
        private string phone;
        private string email;
        private string userType;   // Manager / Trainer / Trainee
        private string status;     // Active / Inactive

        // בנאי: מאתחל משתמש חדש כפעיל
        public User(string id, string password, string fullName,
                    string phone, string email, string userType)
        {
            this.id = id;
            this.password = password;
            this.fullName = fullName;
            this.phone = phone;
            this.email = email;
            this.userType = userType;
            this.status = "Active";
        }

        // מתודות קריאה: מחזירות פרטים בלי לאפשר לשנות אותם מבחוץ
        public string GetId() { return id; }
        public string GetFullName() { return fullName; }
        public string GetPhone() { return phone; }
        public string GetEmail() { return email; }
        public string GetUserType() { return userType; }
        public string GetStatus() { return status; }

        // משווה סיסמה שהוקלדה לסיסמה השמורה, בלי לחשוף את השמורה
        public bool CheckPassword(string input)
        {
            return password == input;
        }

        // בודקת אם החשבון פעיל
        public bool IsActive()
        {
            return status == "Active";
        }

        // מחיקה לוגית: לא מוחקים את החשבון, רק מסמנים אותו כלא פעיל
        public void Deactivate()
        {
            status = "Inactive";
        }

        // מציגה מזהה, שם, סוג וסטטוס בלבד (בלי סיסמה)
        public override string ToString()
        {
            return id + " | " + fullName + " | " + userType + " | " + status;
        }
    }
}