

namespace ToDoWebApplication.Models
{
    //Todo base model
    public class ToDoModel
    {
        public virtual int Id { get; set; }
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual byte Complete { get; set; }
        public virtual DateTime ExpiryDateTime { get; set; }
        public virtual byte Done { get; set; }
        
        public ToDoModel()
        {
            Title = Settings.Setting.ToDoDefaultTitle;
            Description = Settings.Setting.ToDoDefaultTitle;
            Complete = Settings.Setting.ToDoDefaultComplete;
            Done = 0;
            ExpiryDateTime = Settings.Setting.ToDoDefaultExpiryDateTime;
        }
    }
}
