using HabitTracker.Acitivities;
using HabitTracker.DBConnector;


DBConnector dBConnector = new DBConnector();
//dBConnector.CreateDatabase();
//dBConnector.SeedDatabase();
dBConnector.DeleteAllActivities();
dBConnector.SeedDatabase();
var activities = new List<Activity>();
dBConnector.GetActivitiesQuery(out activities);
foreach (var activity in activities)
{
    Console.WriteLine($"Name: {activity.Name}, Quantity: {activity.Quantity}, UoM: {activity.UoM}, Date: {activity.Date}");
}

