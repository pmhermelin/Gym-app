namespace GymApp
{
    public partial class GymSystem
    {
        // מיון בועות של החלק המלא בלבד (0..count-1) לפי תאריך התחלה, מהקרוב לרחוק (סעיף 7.20).
        private void SortScheduleByDate(ScheduleItem[] items, int count)
        {
            for (int pass = 0; pass < count - 1; pass++)
            {
                bool swapped = false;
                for (int i = 0; i < count - 1 - pass; i++)
                {
                    if (items[i].GetStartDateTime() > items[i + 1].GetStartDateTime())
                    {
                        ScheduleItem temp = items[i];
                        items[i] = items[i + 1];
                        items[i + 1] = temp;
                        swapped = true;
                    }
                }
                if (!swapped)
                    break;
            }
        }

        // REQ-018: לוח אישי של המתאמן: הרשמות פעילות לשיעורים עתידיים ופגישות מתוכננות עתידיות,
        // ממוינות מהקרוב לרחוק. מחזירה כמה פריטים נכתבו בפועל (עד גבול המערך). לא משנה נתונים.
        public int BuildTraineeSchedule(Trainee trainee, ScheduleItem[] results)
        {
            if (trainee == null || results == null)
                return 0;

            int count = 0;

            for (int i = 0; i < registrations.Length; i++)
            {
                ClassRegistration? registration = registrations[i];
                if (registration == null || !registration.IsActive())
                    continue;
                if (registration.GetTrainee() != trainee)
                    continue;

                GymClass registeredClass = registration.GetGymClass();
                if (!registeredClass.IsFutureActive())
                    continue;

                if (count == results.Length)
                    break;
                results[count] = new ScheduleItem(registration.GetId(), registeredClass.GetStartDateTime(),
                                                  registeredClass.GetDurationMinutes(), "Class",
                                                  registeredClass.GetTrainer().GetUser().GetFullName(),
                                                  registeredClass.GetStatus());
                count++;
            }

            for (int i = 0; i < appointments.Length; i++)
            {
                Appointment? appointment = appointments[i];
                if (appointment == null || !appointment.IsFutureActive())
                    continue;
                if (appointment.GetTrainee() != trainee)
                    continue;

                if (count == results.Length)
                    break;
                results[count] = new ScheduleItem(appointment.GetId(), appointment.GetStartDateTime(),
                                                  appointment.GetDurationMinutes(), "Appointment",
                                                  appointment.GetTrainer().GetUser().GetFullName(),
                                                  appointment.GetStatus());
                count++;
            }

            SortScheduleByDate(results, count);
            return count;
        }
    }
}
