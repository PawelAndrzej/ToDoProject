using NHibernate;
using NHibernate.Cfg;
using NHibernate.Connection;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;
using NHibernate.NetCore;
using ToDoWebApplication.Models;
namespace ToDoWebApplication.Settings
{
    public static class NHibernateSqlServerInstaller
    {
        public static IServiceCollection AddNHibernateSqlServer(this IServiceCollection services, string cnString)
        {
            var cfg = new Configuration();

            cfg.DataBaseIntegration(db =>
            {
                db.Dialect<MySQL8InnoDBDialect>();
                db.Driver<MySqlDataDriver>();
                db.ConnectionProvider<DriverConnectionProvider>();
                db.LogSqlInConsole = true;
                db.ConnectionString = cnString;
                db.Timeout = 30;/*seconds*/
                db.SchemaAction = SchemaAutoAction.Validate;
            });
            

            var mapping = new ModelMapper();
            
            mapping.AddMappings(typeof(ToDoModelMapping).Assembly.GetTypes());
           var mappingDocument = mapping.CompileMappingForAllExplicitlyAddedEntities();
            cfg.AddMapping(mappingDocument);
            services.AddHibernate(cfg);

            ISessionFactory sessions = cfg.BuildSessionFactory();

            ToDoModel m = new ToDoModel()
            {
               Complete = 10,
               Done = 1
            };


            var os = sessions.OpenSession();
            os.SaveOrUpdate(m);
            os.Flush();
            os.Refresh(m);
            os.Close();

            return services;
        }
    }
}

