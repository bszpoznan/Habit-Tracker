namespace HabitTracker.Acitivities
{
    public class Activity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Quantity { get; set; }
        public string UoM { get; set; }
        public DateOnly Date { get; set; }
        public Activity()
        { }
        public Activity(Activity activity)
        {
            Id = activity.Id;
            Name = activity.Name;
            Quantity = activity.Quantity;
            UoM = activity.UoM;
            Date = activity.Date;
        }
    }       
    
}