using System.Globalization;
using System.IO.Pipelines;
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
                Console.Write("Enter a date of activity (YYYY-MM-DD). You can type T or t of Today (or press Enter to keep current): ");
                input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    break;
                }

                if (input.Equals("T", StringComparison.OrdinalIgnoreCase))
                {
                    activity.Date = DateOnly.FromDateTime(DateTime.Now);
                    break;
                }


                if (DateOnly.TryParseExact(input, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                {
                    activity.Date = date;
                    break;
                }

                Console.WriteLine("Invalid date format. Please enter in YYYY-MM-DD format.");

            } while (true);

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
            Console.WriteLine("\nEnter new details for the activity (leave blank to keep current value).");
            Console.WriteLine("Current Data:");
            Console.WriteLine($"Name: {activity.Name}, Quantity: {activity.Quantity}, UoM: {activity.UoM}, Date: {activity.Date}\n");

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
                Console.Write("Enter new date of activity (YYYY-MM-DD). You can type T or t of Today (or press Enter to keep current): ");
                input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    break;
                }

                if (input.Equals("T", StringComparison.OrdinalIgnoreCase))
                {
                    resultActivity.Date = DateOnly.FromDateTime(DateTime.Now);
                    break;
                }


                if (DateOnly.TryParseExact(input, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                {
                    resultActivity.Date = date;
                    break;
                }

                Console.WriteLine("Invalid date format. Please enter in YYYY-MM-DD format.");

            } while (true);


            return resultActivity;
        }
        public Filter GetFilterChoice()
        {
            int choice;
            string? input;
            Filter resultFilter = new Filter();
            do
            {
                Console.WriteLine("Choose a filter type:");
                Console.WriteLine("1. Filter by Name");
                Console.WriteLine("2. Filter by Date");
                Console.WriteLine("3. No filter");
                Console.WriteLine();
                input = Console.ReadLine();

            } while (string.IsNullOrWhiteSpace(input) || Int32.TryParse(input, out choice) == false || choice < 1 || choice > 3);

            switch (choice)
            {
                case 1:
                    resultFilter.filterType = FilterType.FilterByActivityName;
                    InputActivityNameForFilter(ref resultFilter);
                    break;
                case 2:
                    resultFilter.filterType = FilterType.FilterByDate;
                    InputActivitiesDateRange(ref resultFilter);
                    break;
                case 3:
                    resultFilter.filterType = FilterType.NoFilter;
                    break;
            }

            return resultFilter;

        }

        private bool InputActivitiesDateRange(ref Filter filter)
        {
            bool result = true;
            string? input;
            DateOnly startingDate;
            DateOnly endingDate;
            Console.WriteLine();
            do
            {
                Console.Write("Enter starting date (YYYY-MM-DD). You can type T or t of Today  :");
                input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    break;
                }

                if (input.Equals("T", StringComparison.OrdinalIgnoreCase))
                {
                    filter.dateFrom = DateOnly.FromDateTime(DateTime.Now);
                    break;
                }

                if (DateOnly.TryParseExact(input, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out startingDate))
                {
                    filter.dateFrom = startingDate;
                    break;
                }

                Console.WriteLine("Invalid date format. Please enter in YYYY-MM-DD format.");

            } while (true);

            do
            {
                Console.Write("Enter ending date (YYYY-MM-DD). You can type T or t of Today:  ");
                input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    break;
                }

                if (input.Equals("T", StringComparison.OrdinalIgnoreCase))
                {
                    filter.dateTo = DateOnly.FromDateTime(DateTime.Now);
                    break;
                }

                if (DateOnly.TryParseExact(input, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out endingDate))
                {
                    if (endingDate <= filter.dateFrom)
                    {
                        Console.WriteLine("Ending Date can't be set up for a date before Starting Date.");
                    }
                    else
                    {

                        filter.dateTo = endingDate;
                        break;
                    }

                }
                else
                {
                    Console.WriteLine("Invalid date format. Please enter in YYYY-MM-DD format.");
                }

            } while (true);


            return result;
        }

        private bool InputActivityNameForFilter(ref Filter filter)
        {
            bool result = true;
            string? input = null;
            Console.WriteLine();
            do
            {
                Console.WriteLine("Enter an activity name that you want to filter.");
                input = Console.ReadLine();
            } while (string.IsNullOrEmpty(input));
            filter.activityName = input;
            return result;

        }

    }
}