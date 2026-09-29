using System;

namespace GymApp
{
    public class WorkoutPlan
    {
        // שדות פרטיים: תוכנית אימונים פעילה של מתאמן אחד, עם מערך קבוע של תרגילים
        private string id;                 // מזהה תוכנית, פורמט WPL0001 (הקידומת WPL אושרה ע"י הצוות)
        private Trainee trainee;           // בעל התוכנית
        private Employee trainer;          // המאמן שעדכן אותה לאחרונה
        private DateTime updatedAt;        // מועד העדכון האחרון
        private PlanExercise[] exercises;  // תרגילי התוכנית - תמיד רצופים מאינדקס 0
        private int exerciseCount;         // מספר התרגילים בפועל
        private string status;             // Active / Inactive

        // בנאי: יוצר תוכנית פעילה וריקה עם מקום ל-maxExercises תרגילים
        public WorkoutPlan(string id, Trainee trainee, Employee trainer, int maxExercises)
        {
            this.id = id;
            this.trainee = trainee;
            this.trainer = trainer;
            this.updatedAt = DateTime.Now;
            this.exercises = new PlanExercise[maxExercises];
            this.exerciseCount = 0;
            this.status = "Active";
        }

        // מתודות קריאה
        public string GetId() { return id; }
        public Trainee GetTrainee() { return trainee; }
        public Employee GetTrainer() { return trainer; }
        public DateTime GetUpdatedAt() { return updatedAt; }
        public int GetExerciseCount() { return exerciseCount; }

        public bool IsActive()
        {
            return status == "Active";
        }

        // מוסיפה תרגיל אם יש מקום במערך ואם אותו תרגיל עוד לא נמצא בתוכנית
        public bool AddExercise(PlanExercise item)
        {
            if (item == null)
                return false;
            if (exerciseCount == exercises.Length)
                return false;
            if (ContainsExercise(item.GetExercise().GetId()))
                return false;

            exercises[exerciseCount] = item;
            exerciseCount++;
            return true;
        }

        // מסירה תרגיל לפי מזהה ומזיזה שמאלה את התרגילים שאחריו, כדי שלא יישאר "חור" במערך
        public bool RemoveExercise(string exerciseId)
        {
            int index = FindExerciseIndex(exerciseId);
            if (index == -1)
                return false;

            for (int i = index; i < exerciseCount - 1; i++)
                exercises[i] = exercises[i + 1];

            exercises[exerciseCount - 1] = null!;   // התא האחרון מתפנה (null! - מערך של הפניות שמותר בו תא ריק)
            exerciseCount--;
            return true;
        }

        // מעדכנת סטים, חזרות והנחיות של תרגיל קיים בתוכנית
        public bool UpdateExercise(string exerciseId, int sets, int reps, string instructions)
        {
            int index = FindExerciseIndex(exerciseId);
            if (index == -1)
                return false;

            exercises[index].Update(sets, reps, instructions);
            return true;
        }

        // בדיקת כפילות: האם התרגיל כבר נמצא בתוכנית
        public bool ContainsExercise(string exerciseId)
        {
            return FindExerciseIndex(exerciseId) != -1;
        }

        // מעדכנת את המאמן ואת מועד העדכון - נקראת רק אחרי פעולה מוצלחת ב-GymSystem
        public void Touch(Employee trainer)
        {
            this.trainer = trainer;
            this.updatedAt = DateTime.Now;
        }

        // מחזירה את האינדקס של תרגיל בתוכנית לפי מזהה, או -1 אם לא נמצא
        private int FindExerciseIndex(string exerciseId)
        {
            for (int i = 0; i < exerciseCount; i++)
            {
                if (exercises[i].GetExercise().GetId() == exerciseId)
                    return i;
            }
            return -1;
        }

        // פורמט לפי סעיף 6.5 במסמך הדרישות:
        // Trainee: noam_levi | Trainer: dana_cohen | Updated: 2026-08-01
        // Exercise: Squat | Sets: 3 | Repetitions: 10 | Instructions: Controlled movement
        public override string ToString()
        {
            string text = "Trainee: " + trainee.GetUser().GetFullName()
                          + " | Trainer: " + trainer.GetUser().GetFullName()
                          + " | Updated: " + updatedAt.ToString("yyyy-MM-dd");

            for (int i = 0; i < exerciseCount; i++)
                text += "\n" + exercises[i].ToString();

            return text;
        }
    }
}
