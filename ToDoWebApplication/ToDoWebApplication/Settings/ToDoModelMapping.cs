using NHibernate.Mapping.ByCode.Conformist;
using ToDoWebApplication.Models;

namespace ToDoWebApplication.Settings
{
    public class ToDoModelMapping : JoinedSubclassMapping<ToDoModel>
    {
        public ToDoModelMapping()
        {
            this.Table("todoitem");

            
        }
    }
}
