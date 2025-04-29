using ToDoWebApplication.Configuration;

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
            this.Title = Settings.ToDoDefaultTitle;
            this.Description = Settings.ToDoDefaultDescription;
            this.Complete = Settings.ToDoDefaultComplete;
            this.ExpiryDateTime = Settings.ToDoDefaultExpiryDateTime;
            this.Done = false;
        }
    }
}
