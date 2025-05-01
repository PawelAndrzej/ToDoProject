namespace ToDoWebApplication.Models
{
    public class UserTempData
    {
        public virtual string Name { get; set; }
        public virtual string Value { get; set; }
        public UserTempData()
        {
            Name = String.Empty;
            Value = String.Empty;
        }
    }
}
