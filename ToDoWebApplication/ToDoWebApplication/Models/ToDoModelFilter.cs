using System.ComponentModel;

namespace ToDoWebApplication.Models
{
    //Expire date type enum use inf ToModelFilter
    public enum ExpireDateType
    {
        All,
        Today,
        Nextday,
        CurrentWeek
    }
    //Filter to get specific todo's
    public class ToDoModelFilter
    {
        public virtual ExpireDateType ExpireDateType { get; set; }
        [DisplayName("Search text")]
        public virtual string SearchText { get; set; }
        public ToDoModelFilter()
        {
            ExpireDateType = ExpireDateType.All;
            SearchText = String.Empty;
        }
    }
}
