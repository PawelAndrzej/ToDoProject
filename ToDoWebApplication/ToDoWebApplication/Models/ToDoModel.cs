using Microsoft.AspNetCore.Components.Web.Virtualization;
using ToDoWebApplication.Settings;

namespace ToDoWebApplication.Models
{

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
            this.Title = Setting.ToDoDefaultTitle;
            this.Description = Setting.ToDoDefaultDescription;
            this.Complete = Setting.ToDoDefaultComplete;
            this.ExpiryDateTime = Setting.ToDoDefaultExpiryDateTime;
            this.Done = 0;
        }
    }
}
