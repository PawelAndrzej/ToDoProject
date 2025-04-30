using System.ComponentModel;

namespace ToDoWebApplication.Models
{
    public class ToDoModelFilter
    {
        [DisplayName("All")]
        public virtual bool All{ get; set; }
        [DisplayName("Today")]
        public virtual bool Today { get; set; }
        [DisplayName("Next day")]
        public virtual bool NextDay { get; set; }
        [DisplayName("Incomming week")]
        public virtual bool IncommingWeek { get; set; }
        [DisplayName("Search text")]
        public virtual string SearchText { get; set; }
        public ToDoModelFilter()
        {
            All = true;
            SearchText = String.Empty;
        }
    }
}
