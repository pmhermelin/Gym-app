using System;

namespace GymApp
{
    public class GymClass
    {
        // שדות פרטיים - טבלת שדות בסעיף 6.5 במסמך העיצוב הטכני
        private string id;                    // מזהה שיעור, פורמט CLS0001 (קידומת CLS אושרה ב"החלטות סופיות")
        private string name;                  // שם השיעור
        private string activityType;          // סוג פעילות
        private DateTime startDateTime;       // תאריך ושעת התחלה
        private int durationMinutes;          // משך בדקות; ולידציה (חיובי) מתבצעת ב-GymSystem.AddClass, לא כאן
        private Employee trainer;             // מאמן פעיל - הרכבה, לא string מזהה (5.2)
        private int capacity;                 // קיבולת
        private int activeRegistrantCount;    // מונה נגזר - משתנה רק דרך Increase/DecreaseRegistrantCount
        private string status;                // "Active" / "Cancelled"

        // בנאי: נקרא רק אחרי שכל הבדיקות ב-GymSystem עברו. שיעור חדש תמיד Active עם 0 נרשמים (6.5)
        public GymClass(string id, string name, string activityType, DateTime startDateTime,
                         int durationMinutes, Employee trainer, int capacity)
        {
            this.id = id;
            this.name = name;
            this.activityType = activityType;
            this.startDateTime = startDateTime;
            this.durationMinutes = durationMinutes;
            this.trainer = trainer;
            this.capacity = capacity;
            this.activeRegistrantCount = 0;
            this.status = "Active";
        }

        // מתודות קריאה
        public string GetId() { return id; }
        public string GetName() { return name; }
        public string GetActivityType() { return activityType; }
        public DateTime GetStartDateTime() { return startDateTime; }
        public int GetDurationMinutes() { return durationMinutes; }
        public Employee GetTrainer() { return trainer; }
        public int GetCapacity() { return capacity; }
        public int GetActiveRegistrantCount() { return activeRegistrantCount; }
        public string GetStatus() { return status; }

        // מחשבת מועד סיום: התחלה + משך (נוסחה מדויקת מסעיף 4.4)
        public DateTime GetEndDateTime()
        {
            return startDateTime.AddMinutes(durationMinutes);
        }

        // בודקת אם השיעור פעיל ומועדו עתידי
        public bool IsFutureActive()
        {
            return status == "Active" && startDateTime > DateTime.Now;
        }

        // בודקת אם יש מקום פנוי בשיעור
        public bool HasAvailablePlace()
        {
            return activeRegistrantCount < capacity;
        }

        // מגדילה את מונה הנרשמים בתוך גבולות הקיבולת.
        // נקראת אך ורק אחרי שמירה מוצלחת של ClassRegistration פעילה (RegisterForClass) - לא מבנאי, לא מ-Program (6.5)
        public bool IncreaseRegistrantCount()
        {
            if (activeRegistrantCount >= capacity)
                return false;
            activeRegistrantCount++;
            return true;
        }

        // מקטינה את מונה הנרשמים, לא מתחת לאפס.
        // נקראת אך ורק אחרי ביטול מוצלח באמצעות CancelRegistration (6.5)
        public bool DecreaseRegistrantCount()
        {
            if (activeRegistrantCount <= 0)
                return false;
            activeRegistrantCount--;
            return true;
        }

        // ביטול לוגי של השיעור: משנה סטטוס בלבד, הרשומה נשארת במערך (6.5, 3.1)
        public void Cancel()
        {
            status = "Cancelled";
        }
    }
}
