namespace HabitTracker
{
    public enum FilterType
    {
        FilterByActivityName,
        FilterByDate,
        NoFilter
    }


    public class Filter
    {
        public FilterType filterType;
        public DateOnly dateFrom;
        public DateOnly dateTo;
        public string? activityName;

        public Filter()
        {
            filterType = FilterType.NoFilter;
            dateFrom = DateOnly.MinValue;
            dateTo = DateOnly.MaxValue;
            activityName = null;
        }

    }
}