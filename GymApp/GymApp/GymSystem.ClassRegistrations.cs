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

        private ClassRegistration? FindRegistrationById(string id)
        {
            for (int i = 0; i < registrations.Length; i++)
            {
                ClassRegistration? existing = registrations[i];
                if (existing != null && existing.GetId() == id)
                    return existing;
            }
            return null;
        }

        // REQ-018: ביטול הרשמה עתידית של המתאמן. המונה קטן פעם אחת, רק אחרי ביטול מוצלח.
        // הרשומה נשארת במערך בסטטוס Cancelled. בכל כשל שום דבר לא משתנה. אינה מבטלת פגישות.
        public bool CancelRegistration(Trainee trainee, string registrationId)
        {
            if (trainee == null || registrationId == null || registrationId.Trim() == "")
                return false;

            ClassRegistration? registration = FindRegistrationById(registrationId.Trim());
            if (registration == null || !registration.IsActive())
                return false;
            if (registration.GetTrainee() != trainee)
                return false;

            GymClass gymClass = registration.GetGymClass();
            if (gymClass.GetStartDateTime() <= DateTime.Now)
                return false;

            if (!registration.Cancel())
                return false;

            gymClass.DecreaseRegistrantCount();
            return true;
        }
    }
}
