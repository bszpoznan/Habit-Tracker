namespace HabitTracker.DBQueries
{
    public class DBQueries
    {
        public readonly string createTableCommand = "";
        public readonly string addActivity = "";

        public readonly string[] dataSeedsActivities = { "Running", "Running", "Running", "Water Drunk", "Water Drunk", "Water Drunk", "Reading", "Push Ups" };
        public readonly string[] dataSeedsUoM = { "min.", "min.", "min.", "glasses", "glasses","glasses","pages", "repetitions" };
        public readonly float[] dataSeedQuantities = { 10, 15, 30, 2, 3, 4, 30, 15};

    public readonly DateOnly[] dataSeedDates = new DateOnly[]
        {
            new DateOnly(2025, 5, 12),
            new DateOnly(2025, 5, 13),
            new DateOnly(2025, 5, 14),
            new DateOnly(2025, 5, 12),
            new DateOnly(2025, 5, 13),
            new DateOnly(2025, 5, 14),
            new DateOnly(2025, 5, 19),
            new DateOnly(2025, 5, 20)
        };
        public readonly int numberOfSeeds = 8;
    }

}