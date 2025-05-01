namespace ToDoWebApplication.Helper
{
    public class Helper
    {
        public static Tuple<DateTime,DateTime> CurrentWeek
        {
            get
            {
                DateTime endWeek = DateTime.Today;
                DateTime startWeek = DateTime.Today;
                bool startWeekFind = false;
                bool endWeekFind = false;
                for (int i = 0; i <= 7; i++)
                {
                    if (endWeekFind == false && endWeek.DayOfWeek != DayOfWeek.Sunday)
                    {
                        endWeek = endWeek.AddDays(1);
                    }
                    else
                    {
                        endWeekFind = true;
                    }
                    if (startWeekFind == false && startWeek.DayOfWeek != DayOfWeek.Monday)
                    {
                        startWeek = startWeek.AddDays(-1);
                    }
                    else
                    {
                        startWeekFind = true;
                    }
                }
                return new Tuple<DateTime, DateTime>(startWeek, endWeek);
            }
        }
    }
}
