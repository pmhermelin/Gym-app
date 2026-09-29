using System;

namespace GymApp
{
    public partial class GymSystem
    {
        // מערך תפריטי התזונה: 100 תאים (לפי ההחלטות הסופיות).
        // מאותחל כאן ולא בבנאי שב-GymSystem.cs, כדי לא לגעת בקובץ המשותף.
        private NutritionPlan[] nutritionPlans = new NutritionPlan[100];

        // גדלי המערכים הפנימיים - מסמך העיצוב לא קובע אותם, לכן הוגדרו כקבועים שקל לשנות
        private const int MaxPlanMeals = 10;   // מספר מרבי של ארוחות בתפריט אחד
        private const int MaxMealItems = 10;   // מספר מרבי של פריטים בארוחה אחת

        // מונה ליצירת מזהי תפריט (NPL0001, NPL0002 ...). הקידומת NPL אושרה ע"י הצוות
        private int nextNutritionPlanId = 1;

        // מחזירה את האינדקס הראשון הפנוי (null) במערך nutritionPlans, או -1 אם מלא
        private int FindFreeNutritionPlanIndex()
        {
            for (int i = 0; i < nutritionPlans.Length; i++)
            {
                if (nutritionPlans[i] == null)
                    return i;
            }
            return -1;
        }

        // בודקת שטקסט חובה אינו ריק (שם ארוחה, שם פריט, כמות/הנחיה)
        private bool IsFilled(string text)
        {
            return text != null && text.Trim() != "";
        }

        // ValidateMeal (מסמך העיצוב 7.12): לארוחה יש nameOrTime תקין ולפחות NutritionItem תקין אחד,
        // וכל הפריטים בה עם שם וכמות/הנחיה לא ריקים
        private bool ValidateMeal(Meal meal)
        {
            if (meal == null)
                return false;
            if (!IsFilled(meal.GetNameOrTime()))
                return false;
            if (!meal.HasItems())
                return false;

            for (int i = 0; i < meal.GetItemCount(); i++)
            {
                NutritionItem? item = meal.GetItem(i);
                if (item == null || !IsFilled(item.GetName()) || !IsFilled(item.GetQuantityOrInstruction()))
                    return false;
            }
            return true;
        }

        // REQ-012: יצירה ועדכון של תפריט תזונה פעיל אחד למתאמן משויך.
        // האינדקסים מתחילים מ-0 (Program תמיר ממספור 1.. שמוצג למשתמש).
        // action:
        //   "AddMeal"    - ארוחה חדשה עם הפריט הראשון שלה (mealNameOrTime, itemName, quantityOrInstruction).
        //                  יוצר תפריט חדש אם אין פעיל. כך לא נשמרת אף פעם ארוחה ריקה.
        //   "RemoveMeal" - הסרת ארוחה (mealIndex)
        //   "UpdateMeal" - שינוי שם/זמן של ארוחה (mealIndex, mealNameOrTime)
        //   "AddItem"    - הוספת פריט לארוחה קיימת (mealIndex, itemName, quantityOrInstruction)
        //   "RemoveItem" - הסרת פריט (mealIndex, itemIndex). אסור להסיר את הפריט האחרון - הארוחה הייתה נשארת ריקה
        //   "UpdateItem" - עדכון פריט (mealIndex, itemIndex, itemName, quantityOrInstruction)
        // מחזירה true בהצלחה, false בכל כשל. בכשל לא נוצר תפריט, לא נשארת ארוחה חלקית ולא מתעדכן updatedAt.
        public bool CreateOrUpdateNutritionPlan(Employee trainer, Trainee trainee, string action,
                                                int mealIndex, int itemIndex, string mealNameOrTime,
                                                string itemName, string quantityOrInstruction)
        {
            // 1. המאמן רשאי לערוך רק מתאמן פעיל שמשויך אליו
            if (!IsAssignedToTrainer(trainee, trainer))
                return false;

            NutritionPlan? plan = trainee.GetNutritionPlan();

            if (action == "AddMeal")
            {
                // 2. בונים ארוחה מקומית עם הפריט הראשון ובודקים אותה לפני שמכניסים לתפריט
                if (!IsFilled(mealNameOrTime) || !IsFilled(itemName) || !IsFilled(quantityOrInstruction))
                    return false;

                Meal meal = new Meal(mealNameOrTime.Trim(), MaxMealItems);
                meal.AddItem(new NutritionItem(itemName.Trim(), quantityOrInstruction.Trim()));
                if (!ValidateMeal(meal))
                    return false;

                if (plan == null)
                {
                    // 3א. אין תפריט פעיל - יוצרים חדש, רק אם יש מקום במערך nutritionPlans
                    int index = FindFreeNutritionPlanIndex();
                    if (index == -1)
                        return false;

                    string planId = "NPL" + nextNutritionPlanId.ToString("D4");
                    NutritionPlan newPlan = new NutritionPlan(planId, trainee, trainer, MaxPlanMeals);
                    if (!newPlan.AddMeal(meal))
                        return false;

                    // רק עכשיו שומרים: מזהה, מקום במערך והפניה מהמתאמן
                    nextNutritionPlanId++;
                    nutritionPlans[index] = newPlan;
                    trainee.SetNutritionPlan(newPlan);
                    return true;
                }

                // 3ב. יש תפריט פעיל - מוסיפים לאותו אובייקט (AddMeal בודקת מקום)
                if (!plan.AddMeal(meal))
                    return false;
            }
            else
            {
                // שאר הפעולות אפשריות רק כשקיים תפריט פעיל וארוחה קיימת באינדקס
                if (plan == null)
                    return false;
                Meal? meal = plan.GetMeal(mealIndex);
                if (meal == null)
                    return false;

                // 4. הוספה, הסרה או עדכון דרך מתודות NutritionPlan ו-Meal - כל בדיקה לפני השינוי
                bool success;
                if (action == "RemoveMeal")
                {
                    success = plan.RemoveMeal(mealIndex);
                }
                else if (action == "UpdateMeal")
                {
                    if (!IsFilled(mealNameOrTime))
                        return false;
                    success = plan.UpdateMeal(mealIndex, mealNameOrTime.Trim());
                }
                else if (action == "AddItem")
                {
                    if (!IsFilled(itemName) || !IsFilled(quantityOrInstruction))
                        return false;
                    success = meal.AddItem(new NutritionItem(itemName.Trim(), quantityOrInstruction.Trim()));
                }
                else if (action == "RemoveItem")
                {
                    // אין להשאיר ארוחה ריקה: את הפריט האחרון אפשר להסיר רק דרך RemoveMeal
                    if (meal.GetItemCount() <= 1)
                        return false;
                    success = meal.RemoveItem(itemIndex);
                }
                else if (action == "UpdateItem")
                {
                    if (!IsFilled(itemName) || !IsFilled(quantityOrInstruction))
                        return false;
                    success = meal.UpdateItem(itemIndex, itemName.Trim(), quantityOrInstruction.Trim());
                }
                else
                {
                    return false;   // פעולה לא מוכרת
                }

                if (!success)
                    return false;
            }

            // 5. רק אחרי פעולה מוצלחת: מעדכנים מאמן ותאריך עדכון, והמתאמן ממשיך להפנות לאותו תפריט
            plan.Touch(trainer);
            trainee.SetNutritionPlan(plan);
            return true;
        }
    }
}
