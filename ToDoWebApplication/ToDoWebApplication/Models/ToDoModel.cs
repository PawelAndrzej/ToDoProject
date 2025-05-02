using System.ComponentModel;

namespace ToDoWebApplication.Models
{
    //Todo base model
    public class ToDoModel
    {
        [DisplayName("Id")]
        public virtual int Id { get; set; }
        [DisplayName("Title")]
        public virtual string Title { get; set; }
        [DisplayName("Description")]
        public virtual string Description { get; set; }
        [DisplayName("Complete")]
        public virtual byte Complete { get; set; }
        [DisplayName("Expire date")]
        public virtual DateTime ExpiryDateTime { get; set; }
        [DisplayName("Done")]
        public virtual byte Done { get; set; }
        
        public ToDoModel()
        {
            Title = Settings.Setting.ToDoDefaultTitle;
            Description = Settings.Setting.ToDoDefaultDescription;
            Complete = Settings.Setting.ToDoDefaultComplete;
            Done = 0;
            ExpiryDateTime = Settings.Setting.ToDoDefaultExpiryDateTime;
        }
    }
}
