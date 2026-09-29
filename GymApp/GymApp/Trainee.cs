using System;

namespace GymApp
{
    public class Trainee
    {
        // שדות פרטיים: מתאמן אינו מחלקת בסיס של User - הוא מחזיק הפניה לחשבון (הרכבה, ללא ירושה)
        private User user;
        private Employee? assignedTrainer;   // מאמן פעיל משויך, או null
        private WorkoutPlan? activeWorkoutPlan;   // תוכנית אימונים פעילה, או null (מסמך העיצוב 6.3)

        // הערה: השדה activeNutritionPlan (מסמך העיצוב 6.3) יתווסף יחד עם המחלקה NutritionPlan (KAN-19).

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

        // גישה לתוכנית האימונים הפעילה (null אם אין)
        public WorkoutPlan? GetWorkoutPlan() { return activeWorkoutPlan; }

        // מפנה את המתאמן לתוכנית הפעילה שלו. נקראת רק מתוך GymSystem אחרי פעולה מוצלחת,
        // כך שלמתאמן יש תמיד הפניה אחת בלבד לתוכנית פעילה
        public void SetWorkoutPlan(WorkoutPlan plan)
        {
            activeWorkoutPlan = plan;
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
