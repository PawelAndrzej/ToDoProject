using NHibernate.Mapping.ByCode.Conformist;
using ToDoWebApplication.Models;

namespace ToDoWebApplication.Settings
{
    public class UserTempDataMapping :ClassMapping<UserTempData>
    {
        public UserTempDataMapping()
        {
            this.Schema("todo");
            this.Table("usertempdata");
            Id(x => x.Name, x => x.Column("Name"));
           // this.Property(x => x.Name, x => x.Column("Name"));
            this.Property(x => x.Value, x => x.Column("Value"));
        }
    }
}
