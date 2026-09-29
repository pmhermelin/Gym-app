using System;

namespace GymApp
{
    public class NutritionItem
    {
        // שדות פרטיים: פריט אחד בתוך ארוחה (מסמך העיצוב 6.10)
        private string name;                    // שם הפריט (למשל Oats)
        private string quantityOrInstruction;   // כמות או הנחיה (למשל 80 g)

        // בנאי: נקרא רק אחרי ששדות החובה נבדקו ב-GymSystem (שם וכמות לא ריקים)
        public NutritionItem(string name, string quantityOrInstruction)
        {
            this.name = name;
            this.quantityOrInstruction = quantityOrInstruction;
        }

        // מתודות קריאה - משמשות את ValidateMeal ב-GymSystem
        public string GetName() { return name; }
        public string GetQuantityOrInstruction() { return quantityOrInstruction; }

        // עדכון שם וכמות - נקרא רק אחרי אימות ב-GymSystem
        public void Update(string name, string quantityOrInstruction)
        {
            this.name = name;
            this.quantityOrInstruction = quantityOrInstruction;
        }

        // פורמט: Item: Oats | Quantity/Instruction: 80 g
        public override string ToString()
        {
            return "Item: " + name + " | Quantity/Instruction: " + quantityOrInstruction;
        }
    }
}
