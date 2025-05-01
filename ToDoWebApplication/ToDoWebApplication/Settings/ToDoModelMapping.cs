using NHibernate.Mapping.ByCode.Conformist;
using ToDoWebApplication.Models;

namespace ToDoWebApplication.Settings
{
    //Mapping nhibarnate
    public class ToDoModelMapping : ClassMapping<ToDoModel>
    {
        public ToDoModelMapping()
        {
            this.Schema("todo");
            this.Table("todoitem");
            Id(x => x.Id, x=> x.Column("Id"));

            this.Property(x => x.ExpiryDateTime, x => x.Column("ExpiryDateTime"));
            this.Property(x => x.Title, x => x.Column("Title"));
            this.Property(x => x.Description, x => x.Column("Description"));
            this.Property(x => x.Complete, x => x.Column("Complete"));
            this.Property(x => x.Done, x => x.Column("Done"));

        }
    }
}
