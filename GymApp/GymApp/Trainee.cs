using System;

namespace GymApp
{
    public class Trainee
    {
        // שדות פרטיים: מתאמן אינו מחלקת בסיס של User - הוא מחזיק הפניה לחשבון (הרכבה, ללא ירושה)
        private User user;
        private Employee? assignedTrainer;   // מאמן פעיל משויך, או null

        // הערה: השדות activeWorkoutPlan ו-activeNutritionPlan (מסמך העיצוב 6.3)
        // יתווספו יחד עם המחלקות WorkoutPlan ו-NutritionPlan (KAN-18 / KAN-19),
        // כדי שהקוד יתקמפל כבר עכשיו בלי תלות במחלקות שעוד לא קיימות.

        // בנאי: יוצר מתאמן עם שיוך מאמן (אפשר גם בלי מאמן)
        public Trainee(User user, Employee? trainer = null)
        {
            this.user = user;
            this.assignedTrainer = trainer;
        }

        // מתודות קריאה: מחזירות את החשבון ואת המאמן בלי לאפשר לשנות אותם מבחוץ
        public User GetUser() { return user; }
        public Employee? GetAssignedTrainer() { return assignedTrainer; }

        // מעדכנת שיוך מאמן. נקראת רק מתוך GymSystem, אחרי שכל הבדיקות עברו
        public void SetAssignedTrainer(Employee trainer)
        {
            assignedTrainer = trainer;
        }

        // מבוססת על סטטוס חשבון ה-User, לא על שדה נפרד (מקור אמת יחיד)
        public bool IsActive()
        {
            return user.IsActive();
        }

        // מציגה את פרטי החשבון ואת המאמן המשויך (בלי סיסמה)
        public override string ToString()
        {
            string trainerName = "None";
            if (assignedTrainer != null)
                trainerName = assignedTrainer.GetUser().GetFullName();

            return user.ToString() + " | Trainer: " + trainerName;
        }
    }
}
