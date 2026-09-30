namespace GymApp
{
    // פריט תצוגה זמני ללוח האישי (סעיף 6.16): נבנה משיעור או מפגישה קיימים, לא נשמר כמקור מידע ב-GymSystem.
    public class ScheduleItem
    {
        private string id;
        private DateTime startDateTime;
        private int durationMinutes;
        private string activityType;
        private string trainerName;
        private string status;

        public ScheduleItem(string id, DateTime startDateTime, int durationMinutes,
                            string activityType, string trainerName, string status)
        {
            this.id = id;
            this.startDateTime = startDateTime;
            this.durationMinutes = durationMinutes;
            this.activityType = activityType;
            this.trainerName = trainerName;
            this.status = status;
        }

        public DateTime GetStartDateTime() { return startDateTime; }
     
        public override string ToString()
        {
            return activityType + " #" + id + " | Date: " + startDateTime.ToString("yyyy-MM-dd")
                 + " | Time: " + startDateTime.ToString("HH:mm") + " | Duration: " + durationMinutes
                 + " min | Trainer: " + trainerName + " | Status: " + status;
        }
    }
}
