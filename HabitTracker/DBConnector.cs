using HabitTracker.DBQueriesParameters;
using HabitTracker.Acitivities;
using Microsoft.Data.Sqlite;

namespace HabitTracker.DBConnector
{

    public class DBConnector
    {

        DBQueries dBQueries = new DBQueries();

        public bool CheckIfTableExists()
        {
            bool tableExists = false;
            using (var connection = new SqliteConnection(DBQueries.connectionString))
            {
                connection.Open();
                var checkTable = connection.CreateCommand();
                checkTable.CommandText = DBQueries.tableExistsQuery;
                tableExists = (long)checkTable.ExecuteScalar() > 0;
                connection.Close();
            }
            return tableExists;
        }
        public void CreateDatabase()
        {
            using (var connection = new SqliteConnection(DBQueries.connectionString))
            {
                connection.Open();
                var commandCreateTable = connection.CreateCommand();
                commandCreateTable.CommandText = DBQueries.createTableCommand;
                commandCreateTable.ExecuteNonQuery();
                connection.Close();
            }
        }

        public bool CheckIfActivityExists(string name, DateOnly date)
        {
            bool activityExists = false;
            using (var connection = new SqliteConnection(DBQueries.connectionString))
            {
                connection.Open();
                var checkActivityQuery = connection.CreateCommand();
                checkActivityQuery.CommandText = DBQueries.checkIfActivityExistsQuery;
                checkActivityQuery.Parameters.AddWithValue("@Name", name);
                checkActivityQuery.Parameters.AddWithValue("@Date", date.ToString("yyyy-MM-dd"));
                activityExists = (long)checkActivityQuery.ExecuteScalar() > 0;
                connection.Close();
            }

            return activityExists;
        }



        public int CheckActivityID(string name, DateOnly date)
        {
            int activityID = -1;
            using (var connection = new SqliteConnection(DBQueries.connectionString))
            {
                connection.Open();
                var checkActivityQuery = connection.CreateCommand();
                checkActivityQuery.CommandText = DBQueries.checkActivityIDQuery;
                checkActivityQuery.Parameters.AddWithValue("@Name", name);
                checkActivityQuery.Parameters.AddWithValue("@Date", date.ToString("yyyy-MM-dd"));
                activityID = (int)checkActivityQuery.ExecuteScalar();
                connection.Close();
            }

            return activityID;
        }
        public bool GetActivitiesQuery(out List<Activity> activities)
        {
            activities = new List<Activity>();
            using (var connection = new SqliteConnection(DBQueries.connectionString))
            {
                connection.Open();
                var commandGetActivities = connection.CreateCommand();
                commandGetActivities.CommandText = DBQueries.getActivitiesQuery;
                using (var reader = commandGetActivities.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var activity = new Activity
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Quantity = reader.GetFloat(2),
                            UoM = reader.GetString(3),
                            Date = DateOnly.FromDateTime(reader.GetDateTime(4))
                            
                        };
                        activities.Add(activity);
                    }
                }
                connection.Close();
            }
            return activities.Count > 0;
        }

        public Activity GetActivityById(int id)
        {
            Activity activity = new Activity();
            using (var connection = new SqliteConnection(DBQueries.connectionString))
            {
                connection.Open();
                var commandGetActivity = connection.CreateCommand();
                commandGetActivity.CommandText = DBQueries.getActivityByIdQuery;
                commandGetActivity.Parameters.AddWithValue("@Id", id);
                using (var reader = commandGetActivity.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        activity.Id = reader.GetInt32(0);
                        activity.Name = reader.GetString(1);
                        activity.Quantity = reader.GetFloat(2);
                        activity.UoM = reader.GetString(3);
                        activity.Date = DateOnly.FromDateTime(reader.GetDateTime(4));
                    }
                }
                connection.Close();
            }
            return activity;
        }

        public void SeedDatabase()
        {
            foreach (Activity activity in dBQueries.dataSeedsActivities)
            {
                if (!CheckIfActivityExists(activity.Name, activity.Date))
                {
                    AddActivity(activity);
                }
                else
                { }

            }

        }

        public void AddActivity(Activity activity)
        {
            using (var connection = new SqliteConnection(DBQueries.connectionString))
            {
                connection.Open();
                var commandAddActivity = connection.CreateCommand();
                commandAddActivity.CommandText = DBQueries.addActivityQuery;
                commandAddActivity.Parameters.AddWithValue("@Name", activity.Name);
                commandAddActivity.Parameters.AddWithValue("@Quantity", activity.Quantity);
                commandAddActivity.Parameters.AddWithValue("@Date", activity.Date.ToString("yyyy-MM-dd"));
                commandAddActivity.Parameters.AddWithValue("@UoM", activity.UoM);
                commandAddActivity.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void DeleteActivity(int id)
        {
            using (var connection = new SqliteConnection(DBQueries.connectionString))
            {
                connection.Open();
                var deleteCommand = connection.CreateCommand();
                deleteCommand.CommandText = DBQueries.deleteActivityCommand;
                deleteCommand.Parameters.AddWithValue("@Id", id);
                deleteCommand.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void DeleteAllActivities()
        {
            using (var connection = new SqliteConnection(DBQueries.connectionString))
            {
                connection.Open();
                var deleteAllCommand = connection.CreateCommand();
                deleteAllCommand.CommandText = DBQueries.deleteAllCommand;
                deleteAllCommand.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void UpdateActivity(int id, Activity activity)
        {
            using (var connection = new SqliteConnection(DBQueries.connectionString))
            {
                connection.Open();
                using (var commandUpdateActivity = connection.CreateCommand())
                {
                    commandUpdateActivity.CommandText = DBQueries.updateActivityCommand;
                    commandUpdateActivity.Parameters.AddWithValue("@Id", id);
                    commandUpdateActivity.Parameters.AddWithValue("@Name", activity.Name);
                    commandUpdateActivity.Parameters.AddWithValue("@Quantity", activity.Quantity);
                    commandUpdateActivity.Parameters.AddWithValue("@UoM", activity.UoM);
                    commandUpdateActivity.Parameters.AddWithValue("@Date", activity.Date.ToString("yyyy-MM-dd"));
                    commandUpdateActivity.ExecuteNonQuery();

                }
                connection.Close();
            }

        }

        public void UpdateActivityAddQuantity()
        {
            
        }

    }
}