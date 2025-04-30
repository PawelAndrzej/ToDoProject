namespace ToDoWebApplication.Settings
{
    public class Setting
    {
        public static string ToDoDefaultTitle
        {
            get
            {
                return "ToDo task";
            }
        }
        public static string ToDoDefaultDescription
        {
            get
            {
                return "Default description";
            }
        }
        public static int ToDoDefaultComplete
        {
            get
            {
                return 0;
            }
        }
        public static DateTime ToDoDefaultExpiryDateTime
        {
            get
            {
                return DateTime.Today.AddDays(1).AddMinutes(-1)
                    .AddDays(7);
            }
        }
    }
}
