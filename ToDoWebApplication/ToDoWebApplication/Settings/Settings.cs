namespace ToDoWebApplication.Settings
{
    //Default data todo
    public class Setting
    {
      
        public static byte DefualtDeltaComplete
        {
            get
            {
                return 10;
            }
        }
        public static string ToDoDefaultTitle
        {
            get
            {
                return "Title todo";
            }
        }
        public static string ToDoDefaultDescription
        {
            get
            {
                return "Description todo";
            }
        }
        public static byte ToDoDefaultComplete
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
