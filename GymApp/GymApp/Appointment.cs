namespace GymApp
{
    public class Appointment
    {
        private string id;
        private Employee trainer;
        private Trainee trainee;
        private DateTime startDateTime;
        private int durationMinutes;
        private string status;

        public Appointment(string id, Employee trainer, Trainee trainee, DateTime startDateTime, int durationMinutes)
        {
            this.id = id;
            this.trainer = trainer;
            this.trainee = trainee;
            this.startDateTime = startDateTime;
            this.durationMinutes = durationMinutes;
            this.status = "Scheduled";
        }

        public string GetId() { return id; }
        public Employee GetTrainer() { return trainer; }
        public Trainee GetTrainee() { return trainee; }
        public DateTime GetStartDateTime() { return startDateTime; }
        public int GetDurationMinutes() { return durationMinutes; }
        public string GetStatus() { return status; }

        public DateTime GetEndDateTime() { return startDateTime.AddMinutes(durationMinutes); }

        public bool IsFutureActive() { return status == "Scheduled" && startDateTime > DateTime.Now; }

        public override string ToString()
        {
            return "Appointment #" + id + " | Trainer: " + trainer.GetUser().GetFullName()
                 + " | Trainee: " + trainee.GetUser().GetFullName()
                 + "\nDate: " + startDateTime.ToString("yyyy-MM-dd") + " | Time: " + startDateTime.ToString("HH:mm")
                 + " | Duration: " + durationMinutes + " min | Status: " + status;
        }
    }
}
