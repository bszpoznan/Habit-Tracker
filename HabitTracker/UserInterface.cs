using HabitTracker.Acitivities;
using HabitTracker.DBConnector;

namespace HabitTracker.UI   
{
    public class UserInterface
    {
        

        public void DispalyMenu()
        {
            Console.WriteLine("Welcome to the Habit Tracker!");
            Console.WriteLine("1. Add Activity");
            Console.WriteLine("2. View Activities");
            Console.WriteLine("3. Update Activity");
            Console.WriteLine("4. Delete Activity");
            Console.WriteLine("5. Filter Activities");
            Console.WriteLine("10. Exit");
            Console.WriteLine();

        }

        public int GetUserChoice()
        {
            int choice;
            string? input;
            do
            {
                Console.Write("Enter your choice (1-10): ");
                input = Console.ReadLine();
            } while (input == null || Int32.TryParse(input, out choice) == false || choice < 1 || choice > 10);
            return choice;
        }

        public Activity EnterActivityDetails()
        {
            Activity activity = new Activity();
            string? input;
            do
            {
                Console.Write("Enter activity name: ");
                input = Console.ReadLine();
            } while (input == null || input.Trim() == "" || input.Length < 3);

            activity.Name = input.Trim();

            do
            {
                Console.Write("Enter activity Unit Of Measure: ");
                input = Console.ReadLine();
            } while (input == null || input.Trim() == "" || input.Length < 2);
            activity.UoM = input.Trim();

            int quantity;
            do
            {
                Console.Write("Enter quantity of activity (only numbers): ");
                input = Console.ReadLine();
            } while (input == null || Int32.TryParse(input, out quantity) == false || quantity <= 0);
            activity.Quantity = quantity;

            DateOnly date;
            do
            {
                Console.Write("Enter date of activity (YYYY-MM-DD): ");
                input = Console.ReadLine();
            } while (input == null || DateOnly.TryParse(input, out date) == false);
            activity.Date = date;

            return activity;

        }

        public void DisplayActivities(List<Activity> activities)
        {
            if (activities.Count != 0)
            {
                Console.WriteLine("Activities:");
                foreach (var activity in activities)
                {
                    Console.WriteLine($"ID: {activity.Id}, Name: {activity.Name}, Quantity: {activity.Quantity}, UoM: {activity.UoM}, Date: {activity.Date}");
                }
            }
            else
            {
                Console.WriteLine("No activities found.");
                return;
            }
        }

        public int GetActivityIDToUpdate()
        {
            string? input;
            int id;
            do
            {
                Console.Write("Which activity do you want to update? Enter the ID: ");
                input = Console.ReadLine();
            } while (input == null || Int32.TryParse(input, out id) == false || id <= 0);
            return id;
        }

          public int GetActivityIDToDelete()
        {
            string? input;
            int id;
            do
            {
                Console.Write("Which activity do you want to delete? Enter the ID: ");
                input = Console.ReadLine();
            } while (input == null || Int32.TryParse(input, out id) == false || id <= 0);
            return id;
        }

        public Activity UpdateActivityDetails(Activity activity)
        {
            Activity resultActivity = new Activity(activity);
            Console.WriteLine("Enter new details for the activity (leave blank to keep current value).");
            Console.WriteLine("Current Data:");
            Console.WriteLine($"Name: {activity.Name}, Quantity: {activity.Quantity}, UoM: {activity.UoM}, Date: {activity.Date}");

            string? input;

            Console.Write("Enter new activity name (or press Enter to keep current): ");
            input = Console.ReadLine();
            if (input != null && input.Trim() != "")
            {
                resultActivity.Name = input.Trim();
            }

            Console.Write("Enter new activity Unit Of Measure (or press Enter to keep current): ");
            input = Console.ReadLine();
            if (input != null && input.Trim() != "")
            {
                resultActivity.UoM = input.Trim();
            }
            int quantity;
            input = null;
            do
            {
                Console.Write("Enter new quantity of activity (or press Enter to keep current): ");
                input = Console.ReadLine();
                if (int.TryParse(input, out quantity) == true || quantity <= 0)
                {
                    break;
                }
            } while (input == null || input == "");

            if (input != null && input.Trim() != "")
            {
                resultActivity.Quantity = quantity;
            }

            DateOnly date;
            do
            {
                Console.Write("Enter new date of activity (YYYY-MM-DD) (or press Enter to keep current): ");
                input = Console.ReadLine();
                if (DateOnly.TryParse(input, out date) == true)
                {
                    break;
                }
            } while (input == null || input == "");

            if (input != null && input.Trim() != "")
            {
                resultActivity.Date = date;
            }

            return resultActivity;
        }                

    }
}