using HabitTracker;
using HabitTracker.Acitivities;
using HabitTracker.DBConnector;
using HabitTracker.UI;


DBConnector dbConnector = new DBConnector();
var activities = dbConnector.GetAllActivities();

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
            if (!dbConnector.ActivityExists(newActivity.Name, newActivity.Date))
            {
                dbConnector.AddActivity(newActivity);
                Console.WriteLine("Activity added successfully.\n");
            }
            else
            {
                Console.WriteLine("This activity already exists for the given date.\n");
            }
            break;
        case 2:
            activities = dbConnector.GetAllActivities();
            if (activities.Count>0)
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
            Activity activityToUpdate = dbConnector.GetActivityById(idToUpdate);
            Activity updatedActivity = userInterface.UpdateActivityDetails(activityToUpdate);
            dbConnector.UpdateActivity(idToUpdate, updatedActivity);
            Console.WriteLine("Activity updated successfully.\n");
            break;
        case 4:
            int idToDelete = userInterface.GetActivityIDToDelete();
            dbConnector.DeleteActivity(idToDelete);
            Console.WriteLine("Activity deleted successfully.\n");
            break;  
        case 5:
            Filter filterChoice = userInterface.GetFilterChoice();
            if (filterChoice.filterType == FilterType.FilterByActivityName)
            {
                activities = dbConnector.GetActivitiesByName(filterChoice.activityName);
            }
            else if (filterChoice.filterType == FilterType.FilterByDate)
            {
                activities = dbConnector.GetActivitiesByDate(fromDate: filterChoice.dateFrom, toDate: filterChoice.dateTo);
            }
            else
            {
                activities = dbConnector.GetAllActivities();
            }

            foreach (var activity in activities)
            {
                Console.WriteLine($"Id: {activity.Id}, Name: {activity.Name}, Quantity: {activity.Quantity}, UoM: {activity.UoM}, Date: {activity.Date}");
            }

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
