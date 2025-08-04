using HabitTracker.Acitivities;
using HabitTracker.DBConnector;
using HabitTracker.UI;


DBConnector dBConnector = new DBConnector();
//dBConnector.CreateDatabase();
//dBConnector.SeedDatabase();
//dBConnector.DeleteAllActivities();
//dBConnector.SeedDatabase();
var activities = new List<Activity>();
dBConnector.GetActivitiesQuery(out activities);
/*foreach (var activity in activities)
{
    Console.WriteLine($"Name: {activity.Name}, Quantity: {activity.Quantity}, UoM: {activity.UoM}, Date: {activity.Date}");
}
*/

bool exit = false;
UserInterface userInterface = new UserInterface();

while (!exit)
{
    userInterface.DispalyMenu();
    int choice = userInterface.GetUserChoice();
    switch (choice)
    {
        case 1:

            Activity newActivity = userInterface.EnterActivityDetails();
            if (!dBConnector.CheckIfActivityExists(newActivity.Name, newActivity.Date))
            {
                dBConnector.AddActivity(newActivity);
                Console.WriteLine("Activity added successfully.\n");
            }
            else
            {
                Console.WriteLine("This activity already exists for the given date.\n");
            }
            break;
        case 2:
            if (dBConnector.GetActivitiesQuery(out activities))
            {
                Console.WriteLine("Activities:");
                foreach (var activity in activities)
                {
                    Console.WriteLine($"Id: {activity.Id}, Name: {activity.Name}, Quantity: {activity.Quantity}, UoM: {activity.UoM}, Date: {activity.Date}");
                }
            }
            else
            {
                Console.WriteLine("No activities found.\n");
            }
            break;
        case 3:
            int idToUpdate = userInterface.GetActivityIDToUpdate();
            Activity activityToUpdate = dBConnector.GetActivityById(idToUpdate);
            Activity updatedActivity = userInterface.UpdateActivityDetails(activityToUpdate);
            dBConnector.UpdateActivity(idToUpdate, updatedActivity);
            Console.WriteLine("Activity updated successfully.\n");
            break;
        case 4:
            int idToDelete = userInterface.GetActivityIDToDelete();
            dBConnector.DeleteActivity(idToDelete);
            Console.WriteLine("Activity deleted successfully.\n");
            break;  

        case 10:
            exit = true;
            Console.WriteLine("Exiting the application.");
            break;
        default:
            Console.WriteLine("Invalid choice. Please try again.");
            break;
    }
}
