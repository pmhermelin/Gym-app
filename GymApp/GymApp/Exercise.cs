using System;

namespace GymApp
{
    public class Exercise
    {
        // שדות פרטיים: תרגיל בסיסי במאגר התרגילים - בלי סטים וחזרות (אלה שייכים ל-PlanExercise)
        private string id;            // מזהה תרגיל, פורמט EXE0001 (הקידומת EXE אושרה ע"י הצוות)
        private string name;          // שם התרגיל
        private string description;   // תיאור קצר
        private string status;        // Active / Inactive

        // בנאי: תרגיל חדש במאגר תמיד מתחיל כפעיל
        public Exercise(string id, string name, string description)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.status = "Active";
        }

        // מתודות קריאה
        public string GetId() { return id; }
        public string GetName() { return name; }
        public string GetDescription() { return description; }

        // תרגיל זמין לבחירה בתוכנית רק אם הוא פעיל
        public bool IsActive()
        {
            return status == "Active";
        }

        // מציגה מזהה, שם ותיאור
        public override string ToString()
        {
            return id + " | " + name + " | " + description;
        }
    }
}
