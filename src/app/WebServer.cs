using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using Autofac;

namespace Procent.DependencyInjection.app
{
    public class WebServer
    {
        static IContainer _container;

        static void Main()
        {
            var builder = new ContainerBuilder();
            Assembly executingAssembly = Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(executingAssembly)
                .AsSelf()
                .AsImplementedInterfaces();

            // override default registration if needed
            builder.RegisterType<EmailValidator>()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<OperationContext>()
                .AsSelf()
                .InstancePerLifetimeScope();

            builder.Register(cc =>
            {
                var dbConnection = new SqlConnection();
                var transaction = dbConnection.BeginTransaction();

                var ctx = cc.Resolve<OperationContext>();
                ctx.Transaction = transaction;

                return dbConnection;
            })
            .As<IDbConnection>()
            .InstancePerLifetimeScope();

            _container = builder.Build();
        }

        static void Shutdown()
        {
            _container.Dispose();
        }

        public void RegisterUser(string email)
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var controller = scope.Resolve<UsersController>();

                controller.RegisterUser(email);
            }
        }
    }
}