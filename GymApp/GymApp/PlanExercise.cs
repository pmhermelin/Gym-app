using System;

namespace GymApp
{
    public class PlanExercise
    {
        // שדות פרטיים: תרגיל כפי שהוא מופיע בתוכנית אימונים מסוימת.
        // סטים, חזרות והנחיות נשמרים כאן ולא ב-Exercise, כדי שאותו תרגיל יוכל להופיע
        // בתוכניות שונות עם הגדרות שונות.
        private Exercise exercise;     // הפניה לתרגיל מהמאגר (הרכבה)
        private int sets;              // מספר סטים חיובי
        private int repetitions;       // מספר חזרות חיובי
        private string instructions;   // הנחיות מותאמות

        // בנאי: נקרא רק אחרי שהערכים נבדקו ב-GymSystem
        public PlanExercise(Exercise exercise, int sets, int reps, string instructions)
        {
            this.exercise = exercise;
            this.sets = sets;
            this.repetitions = reps;
            this.instructions = instructions;
        }

        // מתודות קריאה
        public Exercise GetExercise() { return exercise; }
        public int GetSets() { return sets; }
        public int GetRepetitions() { return repetitions; }

        // עדכון הערכים של התרגיל בתוכנית - נקרא רק אחרי אימות ב-GymSystem
        public void Update(int sets, int reps, string instructions)
        {
            this.sets = sets;
            this.repetitions = reps;
            this.instructions = instructions;
        }

        // פורמט לפי סעיף 6.5 במסמך הדרישות:
        // Exercise: Squat | Sets: 3 | Repetitions: 10 | Instructions: Controlled movement
        public override string ToString()
        {
            return "Exercise: " + exercise.GetName() + " | Sets: " + sets
                   + " | Repetitions: " + repetitions + " | Instructions: " + instructions;
        }
    }
}
