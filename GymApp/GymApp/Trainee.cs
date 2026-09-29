using System;

namespace GymApp
{
    public class Trainee
    {
        // שדות פרטיים: מתאמן אינו מחלקת בסיס של User - הוא מחזיק הפניה לחשבון (הרכבה, ללא ירושה)
        private User user;
        private Employee? assignedTrainer;   // מאמן פעיל משויך, או null
        private WorkoutPlan? activeWorkoutPlan;   // תוכנית אימונים פעילה, או null (מסמך העיצוב 6.3)
        private NutritionPlan? activeNutritionPlan;   // תפריט תזונה פעיל, או null (מסמך העיצוב 6.3)

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

        // גישה לתפריט התזונה הפעיל (null אם אין)
        public NutritionPlan? GetNutritionPlan() { return activeNutritionPlan; }

        // מפנה את המתאמן לתפריט הפעיל שלו. נקראת רק מתוך GymSystem אחרי פעולה מוצלחת,
        // כך שלמתאמן יש תמיד הפניה אחת בלבד לתפריט פעיל
        public void SetNutritionPlan(NutritionPlan plan)
        {
            activeNutritionPlan = plan;
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
