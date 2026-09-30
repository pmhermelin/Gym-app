namespace GymApp
{
    public partial class GymSystem
    {
        private ClassRegistration[] registrations = new ClassRegistration[1000];
        private int nextRegistrationId = 1;

        private int FindFreeRegistrationIndex()
        {
            for (int i = 0; i < registrations.Length; i++)
            {
                if (registrations[i] == null)
                    return i;
            }
            return -1;
        }

        private bool HasActiveRegistration(Trainee trainee, GymClass gymClass)
        {
            for (int i = 0; i < registrations.Length; i++)
            {
                ClassRegistration? existing = registrations[i];
                if (existing != null && existing.IsActive()
                    && existing.GetTrainee() == trainee && existing.GetGymClass() == gymClass)
                    return true;
            }
            return false;
        }
        
        // REQ-017: הרשמת מתאמן לשיעור עתידי. המונה גדל רק אחרי שמירה מוצלחת; בכל כשל שום דבר לא משתנה.
        public ClassRegistration? RegisterForClass(Trainee trainee, string classId)
        {
            if (trainee == null || !trainee.IsActive())
                return null;
            if (classId == null || classId.Trim() == "")
                return null;

            GymClass? gymClass = FindClassById(classId.Trim());
            if (gymClass == null || !gymClass.IsFutureActive())
                return null;

            int index = FindFreeRegistrationIndex();
            if (index == -1)
                return null;

            if (GetMembershipFor(trainee, gymClass.GetStartDateTime()) == null)
                return null;
            if (!gymClass.HasAvailablePlace())
                return null;
            if (HasActiveRegistration(trainee, gymClass))
                return null;
            if (TraineeHasConflict(trainee, gymClass.GetStartDateTime(), gymClass.GetDurationMinutes()))
                return null;

            string id = "REG" + nextRegistrationId.ToString("D4");
            nextRegistrationId++;

            ClassRegistration registration = new ClassRegistration(id, trainee, gymClass);
            registrations[index] = registration;
            gymClass.IncreaseRegistrantCount();
            return registration;
        }
    }
}
