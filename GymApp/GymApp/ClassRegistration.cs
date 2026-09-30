namespace GymApp
{
    public class ClassRegistration
    {
        private string id;
        private Trainee trainee;
        private GymClass gymClass;
        private DateTime registrationDate;
        private string status;

        public ClassRegistration(string id, Trainee trainee, GymClass gymClass)
        {
            this.id = id;
            this.trainee = trainee;
            this.gymClass = gymClass;
            this.registrationDate = DateTime.Now;
            this.status = "Active";
        }

        public string GetId() { return id; }
        public Trainee GetTrainee() { return trainee; }
        public GymClass GetGymClass() { return gymClass; }
        public DateTime GetRegistrationDate() { return registrationDate; }
        public string GetStatus() { return status; }

        public bool IsActive() { return status == "Active"; }

        public override string ToString()
        {
            return "Registration #" + id + " | Class: #" + gymClass.GetId() + " " + gymClass.GetName()
                 + " | Trainee: " + trainee.GetUser().GetFullName() + " | Status: " + status;
        }
    }
}
