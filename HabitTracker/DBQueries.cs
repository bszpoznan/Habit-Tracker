using HabitTracker.Acitivities;
namespace HabitTracker.DBQueriesParameters

{
    public class DBQueries
    {
        public const string connectionString = "Data Source=HabitTracker.db;";

        public const string tableExistsQuery = @"
            SELECT COUNT(*)  
                FROM sqlite_master
                WHERE type='table' AND name='Activities'";
        public const string createTableCommand = @"
            CREATE TABLE IF NOT EXISTS Activities (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT NOT NULL,
                Quantity REAL NOT NULL,
                UoM TEXT NOT NULL,
                Date DATE,
                CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP
            )";
        public const string checkIfActivityExistsQuery = @"
            SELECT COUNT(*)
                FROM Activities
                WHERE Name = @Name AND Date = @Date";


        public const string checkActivityIDQuery = @"
            SELECT Id
                FROM Activities
                WHERE Name = @Name AND Date = @Date
                LIMIT 1";        
        public const string addActivityQuery = @"
            INSERT INTO Activities (Name, Quantity, UoM, Date, CreatedAt)
            VALUES (@Name, @Quantity, @UoM, @Date, datetime('now'))";

        public const string getActivitiesQuery = @"
            SELECT * FROM Activities
            ORDER BY Date DESC, CreatedAt DESC";

        public const string deleteActivityCommand = @"
            DELETE FROM Activities
            WHERE Id = @Id";

        public const string deleteAllCommand = @"
            DELETE FROM Activities";

        public const string updateActivityCommand = @"
            UPDATE Activities
            SET Name = @Name, Quantity = @Quantity,  UoM = @UoM, Date = @Date
            WHERE Id = @Id";
        public const string getActivityFilteredByNameQuery = @"
            SELECT * FROM Activities
            WHERE Name = @Name
            ORDER BY Date DESC, CreatedAt DESC";
        public const string getActivityFilteredByDateQuery = @"
            SELECT * FROM Activities
            WHERE Date = @Date";

        public const string getActivityByIdQuery = @"
            SELECT * FROM Activities
            WHERE Id = @Id";
    



        public List<Activity> dataSeedsActivities = new List<Activity>()
        {
            new Activity { Name = "Running", Quantity = 10, UoM = "min.", Date = new DateOnly(2025, 5, 12) },
            new Activity { Name = "Running", Quantity = 15, UoM = "min.", Date = new DateOnly(2025, 5, 13) },
            new Activity { Name = "Running", Quantity = 30, UoM = "min.", Date = new DateOnly(2025, 5, 14) },
            new Activity { Name = "Water Drunk", Quantity = 2, UoM = "glasses", Date = new DateOnly(2025, 5, 12) },
            new Activity { Name = "Water Drunk", Quantity = 3, UoM = "glasses", Date = new DateOnly(2025, 5, 13) },
            new Activity { Name = "Water Drunk", Quantity = 4, UoM = "glasses", Date = new DateOnly(2025, 5, 14) },
            new Activity { Name = "Reading", Quantity = 30, UoM = "pages", Date = new DateOnly(2025, 5, 19) },
            new Activity { Name = "Push Ups", Quantity = 15, UoM = "repetitions", Date = new DateOnly(2025, 5, 20) }

        };
       /* public string[] dataSeedsActivities = { "Running", "Running", "Running", "Water Drunk", "Water Drunk", "Water Drunk", "Reading", "Push Ups" };
        public string[] dataSeedsUoM = { "min.", "min.", "min.", "glasses", "glasses", "glasses", "pages", "repetitions" };
        public float[] dataSeedQuantities = { 10, 15, 30, 2, 3, 4, 30, 15 };

        public DateOnly[] dataSeedDates = new DateOnly[]
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
        public readonly int numberOfSeeds = 8;*/
    }



}