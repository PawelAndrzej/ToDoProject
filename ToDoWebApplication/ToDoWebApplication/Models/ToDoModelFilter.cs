using System.ComponentModel;

namespace ToDoWebApplication.Models
{
    public enum ExpireDateType
    {
        All,
        Today,
        Nextday,
        IncommingWeek
    }
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
