namespace GymApp
{
    public partial class GymSystem
    {
        private Appointment[] appointments = new Appointment[500];
        private int nextAppointmentId = 1;

        private int FindFreeAppointmentIndex()
        {
            for (int i = 0; i < appointments.Length; i++)
            {
                if (appointments[i] == null)
                    return i;
            }
            return -1;
        }

        private bool TraineeHasConflict(Trainee trainee, DateTime start, int durationMinutes)
        {
            for (int i = 0; i < appointments.Length; i++)
            {
                Appointment? existing = appointments[i];
                if (existing == null || existing.GetStatus() != "Scheduled")
                    continue;
                if (existing.GetTrainee() != trainee)
                    continue;

                if (TimesOverlap(start, durationMinutes, existing.GetStartDateTime(), existing.GetDurationMinutes()))
                    return true;
            }

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

                if (TimesOverlap(start, durationMinutes, registeredClass.GetStartDateTime(), registeredClass.GetDurationMinutes()))
                    return true;
            }
            return false;
        }

        // REQ-013: קביעת פגישה עתידית בין מאמן למתאמן משויך, ללא חפיפה לאף אחד מהם.
        public Appointment? ScheduleAppointment(Employee trainer, Trainee trainee, DateTime start, int duration)
        {
            if (!IsAssignedToTrainer(trainee, trainer))
                return null;
            if (trainer.GetUser().GetUserType() != "Trainer")
                return null;
            if (start <= DateTime.Now || duration <= 0)
                return null;

            int index = FindFreeAppointmentIndex();
            if (index == -1)
                return null;

            if (TrainerHasConflict(trainer, start, duration, null))
                return null;
            if (TraineeHasConflict(trainee, start, duration))
                return null;

            string id = "APP" + nextAppointmentId.ToString("D4");
            nextAppointmentId++;

            Appointment appointment = new Appointment(id, trainer, trainee, start, duration);
            appointments[index] = appointment;
            return appointment;
        }
    }
}
