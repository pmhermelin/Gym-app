using System;

namespace GymApp
{
    public partial class GymSystem
    {
        // מאגר התרגילים: 100 תאים (לפי ההחלטות הסופיות). מתמלא בנתוני האתחול של המערכת.
        private Exercise[] exerciseCatalog = new Exercise[100];

        // מערך תוכניות האימונים: 100 תאים (לפי ההחלטות הסופיות).
        // שני המערכים מאותחלים כאן ולא בבנאי שב-GymSystem.cs, כדי לא לגעת בקובץ המשותף.
        private WorkoutPlan[] workoutPlans = new WorkoutPlan[100];

        // מספר מרבי של תרגילים בתוכנית אחת (גודל המערך הפנימי של WorkoutPlan)
        private const int MaxPlanExercises = 20;

        // מונה ליצירת מזהי תוכנית (WPL0001, WPL0002 ...). הקידומת WPL אושרה ע"י הצוות
        private int nextWorkoutPlanId = 1;

        // מחפשת תרגיל במאגר לפי מזהה מדויק; מחזירה null אם לא נמצא
        private Exercise? FindExerciseById(string id)
        {
            if (id == null)
                return null;

            for (int i = 0; i < exerciseCatalog.Length; i++)
            {
                if (exerciseCatalog[i] != null && exerciseCatalog[i].GetId() == id.Trim())
                    return exerciseCatalog[i];
            }
            return null;
        }

        // מחזירה את האינדקס הראשון הפנוי (null) במערך workoutPlans, או -1 אם מלא
        private int FindFreeWorkoutPlanIndex()
        {
            for (int i = 0; i < workoutPlans.Length; i++)
            {
                if (workoutPlans[i] == null)
                    return i;
            }
            return -1;
        }

        // REQ-011: יצירה ועדכון של תוכנית אימונים פעילה אחת למתאמן משויך.
        // action: "Add" - הוספת תרגיל (יוצר תוכנית חדשה אם אין פעילה),
        //         "Remove" - הסרת תרגיל, "Update" - עדכון סטים/חזרות/הנחיות של תרגיל קיים.
        // מחזירה true בהצלחה, false בכל כשל. בכשל לא נוצרת תוכנית ולא מתעדכן updatedAt.
        public bool CreateOrUpdateWorkoutPlan(Employee trainer, Trainee trainee, string action,
                                              string exerciseId, int sets, int repetitions, string instructions)
        {
            // 1. המאמן רשאי לערוך רק מתאמן פעיל שמשויך אליו
            if (!IsAssignedToTrainer(trainee, trainer))
                return false;
            if (action != "Add" && action != "Remove" && action != "Update")
                return false;
            if (exerciseId == null || exerciseId.Trim() == "")
                return false;
            string id = exerciseId.Trim();

            // 2. בהוספה ובעדכון: סטים וחזרות חיוביים והנחיות לא ריקות
            if (action == "Add" || action == "Update")
            {
                if (sets <= 0 || repetitions <= 0)
                    return false;
                if (instructions == null || instructions.Trim() == "")
                    return false;
            }

            WorkoutPlan? plan = trainee.GetWorkoutPlan();
            bool success;

            if (action == "Add")
            {
                // 3. התרגיל חייב להיות קיים ופעיל במאגר
                Exercise? exercise = FindExerciseById(id);
                if (exercise == null || !exercise.IsActive())
                    return false;
                PlanExercise item = new PlanExercise(exercise, sets, repetitions, instructions.Trim());

                if (plan == null)
                {
                    // 4א. אין תוכנית פעילה - יוצרים חדשה, רק אם יש מקום במערך workoutPlans
                    int index = FindFreeWorkoutPlanIndex();
                    if (index == -1)
                        return false;

                    string planId = "WPL" + nextWorkoutPlanId.ToString("D4");
                    WorkoutPlan newPlan = new WorkoutPlan(planId, trainee, trainer, MaxPlanExercises);
                    if (!newPlan.AddExercise(item))
                        return false;

                    // רק עכשיו שומרים: מזהה, מקום במערך והפניה מהמתאמן
                    nextWorkoutPlanId++;
                    workoutPlans[index] = newPlan;
                    trainee.SetWorkoutPlan(newPlan);
                    return true;
                }

                // 4ב. יש תוכנית פעילה - עורכים את אותה תוכנית (AddExercise בודקת מקום וכפילות)
                success = plan.AddExercise(item);
            }
            else
            {
                // הסרה או עדכון אפשריים רק כשקיימת תוכנית פעילה
                if (plan == null)
                    return false;

                if (action == "Remove")
                    success = plan.RemoveExercise(id);
                else
                    success = plan.UpdateExercise(id, sets, repetitions, instructions.Trim());
            }

            // 5. רק אחרי פעולה מוצלחת: מעדכנים מאמן ותאריך עדכון, והמתאמן ממשיך להפנות לאותה תוכנית
            if (!success)
                return false;

            plan.Touch(trainer);
            trainee.SetWorkoutPlan(plan);
            return true;
        }
    }
}
