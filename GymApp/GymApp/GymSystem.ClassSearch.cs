using System;

namespace GymApp
{
    public partial class GymSystem
    {
        // REQ-016: חיפוש שיעורים עתידיים פעילים לפי שם חלקי ומסננים אופציונליים.
        // מסנן שערכו null/ריק/"All" = בלי סינון באותו שדה. useDateFilter=false מדלג על מסנן התאריך.
        // מחזירה כמה תוצאות נכתבו ל-results (עד לגבול המערך). אף פעם לא משנה נתונים.
        public int SearchClasses(string name, bool useDateFilter, DateTime date, string type,
                                 string trainerId, GymClass[] results)
        {
            if (results == null)
                return 0;

            int count = 0;
            for (int i = 0; i < classes.Length; i++)
            {
                GymClass? gymClass = classes[i];
                if (gymClass == null || !gymClass.IsFutureActive())
                    continue;
                if (!MatchesClassFilters(gymClass, name, useDateFilter, date, type, trainerId))
                    continue;

                if (count == results.Length)
                    break;
                results[count] = gymClass;
                count++;
            }

            return count;
        }

        // בודקת שם חלקי (ללא תלות רישיות), תאריך (אם נבחר), סוג פעילות ומזהה מאמן.
        private bool MatchesClassFilters(GymClass gymClass, string name, bool useDateFilter,
                                         DateTime date, string type, string trainerId)
        {
            if (name != null && name.Trim() != ""
                && !gymClass.GetName().Contains(name.Trim(), StringComparison.OrdinalIgnoreCase))
                return false;

            if (useDateFilter && gymClass.GetStartDateTime().Date != date.Date)
                return false;

            if (type != null && type.Trim() != "" && type.Trim() != "All"
                && !string.Equals(gymClass.GetActivityType(), type.Trim(), StringComparison.OrdinalIgnoreCase))
                return false;

            if (trainerId != null && trainerId.Trim() != "" && trainerId.Trim() != "All"
                && !string.Equals(gymClass.GetTrainer().GetUser().GetId(), trainerId.Trim(), StringComparison.OrdinalIgnoreCase))
                return false;

            return true;
        }
    }
}
