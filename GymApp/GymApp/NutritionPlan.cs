using System;

namespace GymApp
{
    public class NutritionPlan
    {
        // שדות פרטיים: תפריט תזונה פעיל של מתאמן אחד, עם מערך קבוע של ארוחות (מסמך העיצוב 6.12)
        private string id;           // מזהה תפריט, פורמט NPL0001 (הקידומת NPL אושרה ע"י הצוות)
        private Trainee trainee;     // בעל התפריט
        private Employee trainer;    // המאמן שעדכן אותו לאחרונה
        private DateTime updatedAt;  // מועד העדכון האחרון
        private Meal[] meals;        // ארוחות התפריט - תמיד רצופות מאינדקס 0
        private int mealCount;       // מספר הארוחות בפועל
        private string status;       // Active / Inactive

        // בנאי: יוצר תפריט פעיל וריק עם מקום ל-maxMeals ארוחות
        public NutritionPlan(string id, Trainee trainee, Employee trainer, int maxMeals)
        {
            this.id = id;
            this.trainee = trainee;
            this.trainer = trainer;
            this.updatedAt = DateTime.Now;
            this.meals = new Meal[maxMeals];
            this.mealCount = 0;
            this.status = "Active";
        }

        // מתודות קריאה
        public string GetId() { return id; }
        public Trainee GetTrainee() { return trainee; }
        public Employee GetTrainer() { return trainer; }
        public DateTime GetUpdatedAt() { return updatedAt; }
        public int GetMealCount() { return mealCount; }

        public bool IsActive()
        {
            return status == "Active";
        }

        // מחזירה ארוחה לפי אינדקס (מ-0), או null אם האינדקס לא קיים.
        // משמשת את GymSystem לפעולות על פריטים בתוך ארוחה קיימת
        public Meal? GetMeal(int index)
        {
            if (index < 0 || index >= mealCount)
                return null;
            return meals[index];
        }

        // מוסיפה ארוחה תקינה (לפחות פריט אחד) אם יש מקום במערך
        public bool AddMeal(Meal meal)
        {
            if (meal == null || !meal.HasItems())
                return false;
            if (mealCount == meals.Length)
                return false;

            meals[mealCount] = meal;
            mealCount++;
            return true;
        }

        // מסירה ארוחה לפי אינדקס ומזיזה שמאלה את הארוחות שאחריה
        public bool RemoveMeal(int index)
        {
            if (index < 0 || index >= mealCount)
                return false;

            for (int i = index; i < mealCount - 1; i++)
                meals[i] = meals[i + 1];

            meals[mealCount - 1] = null!;   // התא האחרון מתפנה
            mealCount--;
            return true;
        }

        // מעדכנת את שם/זמן הארוחה לפי אינדקס (עדכון פריטים נעשה דרך GetMeal ומתודות Meal)
        public bool UpdateMeal(int index, string nameOrTime)
        {
            if (index < 0 || index >= mealCount)
                return false;

            meals[index].SetNameOrTime(nameOrTime);
            return true;
        }

        // מעדכנת את המאמן ואת מועד העדכון - נקראת רק אחרי פעולה מוצלחת ב-GymSystem
        public void Touch(Employee trainer)
        {
            this.trainer = trainer;
            this.updatedAt = DateTime.Now;
        }

        // מציג את התפריט המלא, באותו סגנון כמו תוכנית האימונים (סעיף 6.5 במסמך הדרישות):
        // Trainee: Noam Levi | Trainer: Dana Cohen | Updated: 2026-09-29
        // Meal: Breakfast
        //   Item: Oats | Quantity/Instruction: 80 g
        public override string ToString()
        {
            string text = "Trainee: " + trainee.GetUser().GetFullName()
                          + " | Trainer: " + trainer.GetUser().GetFullName()
                          + " | Updated: " + updatedAt.ToString("yyyy-MM-dd");

            for (int i = 0; i < mealCount; i++)
                text += "\n" + meals[i].ToString();

            return text;
        }
    }
}
