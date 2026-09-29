using System;

namespace GymApp
{
    public class Meal
    {
        // שדות פרטיים: ארוחה אחת בתפריט עם מערך קבוע של פריטים (מסמך העיצוב 6.11)
        private string nameOrTime;       // שם הארוחה או זמן ביום (למשל Breakfast / 08:00)
        private NutritionItem[] items;   // פריטי הארוחה - תמיד רצופים מאינדקס 0
        private int itemCount;           // מספר הפריטים בפועל

        // בנאי: יוצר ארוחה ריקה עם מקום ל-maxItems פריטים.
        // ארוחה ריקה לא נשמרת בתפריט - GymSystem מוסיפה לה פריט ובודקת אותה (ValidateMeal) לפני השמירה
        public Meal(string nameOrTime, int maxItems)
        {
            this.nameOrTime = nameOrTime;
            this.items = new NutritionItem[maxItems];
            this.itemCount = 0;
        }

        // מתודות קריאה
        public string GetNameOrTime() { return nameOrTime; }
        public int GetItemCount() { return itemCount; }

        // מחזירה פריט לפי אינדקס (מ-0), או null אם האינדקס לא קיים
        public NutritionItem? GetItem(int index)
        {
            if (index < 0 || index >= itemCount)
                return null;
            return items[index];
        }

        // עדכון שם/זמן הארוחה - נקרא רק אחרי אימות ב-GymSystem
        public void SetNameOrTime(string nameOrTime)
        {
            this.nameOrTime = nameOrTime;
        }

        // מוסיפה פריט אם יש מקום במערך
        public bool AddItem(NutritionItem item)
        {
            if (item == null)
                return false;
            if (itemCount == items.Length)
                return false;

            items[itemCount] = item;
            itemCount++;
            return true;
        }

        // מסירה פריט לפי אינדקס ומזיזה שמאלה את הפריטים שאחריו, כדי שלא יישאר "חור" במערך
        public bool RemoveItem(int index)
        {
            if (index < 0 || index >= itemCount)
                return false;

            for (int i = index; i < itemCount - 1; i++)
                items[i] = items[i + 1];

            items[itemCount - 1] = null!;   // התא האחרון מתפנה
            itemCount--;
            return true;
        }

        // מעדכנת שם וכמות של פריט קיים לפי אינדקס
        public bool UpdateItem(int index, string name, string quantityOrInstruction)
        {
            if (index < 0 || index >= itemCount)
                return false;

            items[index].Update(name, quantityOrInstruction);
            return true;
        }

        // מוודאת שיש בארוחה לפחות פריט אחד
        public bool HasItems()
        {
            return itemCount > 0;
        }

        // מציגה את הארוחה ואת הפריטים לפי הסדר שבו נשמרו:
        // Meal: Breakfast
        //   Item: Oats | Quantity/Instruction: 80 g
        public override string ToString()
        {
            string text = "Meal: " + nameOrTime;
            for (int i = 0; i < itemCount; i++)
                text += "\n  " + items[i].ToString();
            return text;
        }
    }
}
