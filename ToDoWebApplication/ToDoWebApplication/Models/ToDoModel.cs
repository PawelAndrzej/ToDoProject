using ToDoWebApplication.Settings;

namespace ToDoWebApplication.Models
{
    public class ToDoModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Complete { get; set; }
        public DateTime ExpiryDateTime { get; set; }
        public bool Done { get; set; }
        public ToDoModel()
        {
            this.Title = Setting.ToDoDefaultTitle;
            this.Description = Setting.ToDoDefaultDescription;
            this.Complete = Setting.ToDoDefaultComplete;
            this.ExpiryDateTime = Setting.ToDoDefaultExpiryDateTime;
            this.Done = false;
        }
    }
}
