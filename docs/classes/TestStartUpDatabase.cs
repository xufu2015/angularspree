using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using AutoMapper;
using Create.Data;
using Create.Store.Data;
using Create2;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Create.IntegrationTest.Setup
{
    public class TestStartUpDatabase : Startup, IDisposable
    {
     public static Mock<IHttpContextAccessor> mockAccessor=new Mock<IHttpContextAccessor>();
    public TestStartUpDatabase(IHostingEnvironment env) : base(env)
        {
    }


        public override void SetupDatabase(IServiceCollection services)
        {
            var connectionStringBuilder = new SqlConnectionStringBuilder
            {
                DataSource = @"(LocalDB)\MSSQLLocalDB",
                InitialCatalog = "VS2017Db_Create.Local",
                IntegratedSecurity = true,
            };
            var connectionString = connectionStringBuilder.ToString();
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString,
                    b =>
                    {
                        b.MigrationsAssembly("Create.Data");
                        b.UseRowNumberForPaging();
                    });
                //  options.UseOpenIddict();
            });

            services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(connectionString,
                    b =>
                    {
                        b.MigrationsAssembly("Create.Store.Data");
                        b.UseRowNumberForPaging();
                    });
            });
        }

        public override void EnsureDatabaseCreated(ApplicationDbContext dbContext)
        {
            DestroyDatabase();
            CreateDatabase();
        }


        public void Dispose()
        {
            DestroyDatabase();
        }

        private static void CreateDatabase()
        {
            ExecuteSqlCommand(Master, $@"
			  IF(db_id(N'VS2017Db_Create.Local') IS NULL)
			  BEGIN
                CREATE DATABASE [VS2017Db_Create.Local]
                ON (NAME = 'VS2017Db_Create.Local',
                FILENAME = '{Filename}')
              END");

            var connectionStringBuilder = new SqlConnectionStringBuilder
            {
                DataSource = @"(LocalDB)\MSSQLLocalDB",
                InitialCatalog = "VS2017Db_Create.Local",

                IntegratedSecurity = true,
            };
            var connectionString = connectionStringBuilder.ToString();

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            using (var context = new ApplicationDbContext(optionsBuilder.Options, mockAccessor.Object))
            {
                context.Database.Migrate();
                context.SaveChanges();
            }
            var optionsStoreBuilder = new DbContextOptionsBuilder<StoreDbContext>();
            optionsStoreBuilder.UseSqlServer(connectionString);
            using (var context = new StoreDbContext(optionsStoreBuilder.Options))
            {
                context.Database.Migrate();
                context.SaveChanges();
            }
        }


        private static void DestroyDatabase()
        {
            var fileNames = ExecuteSqlQuery(Master, @"
                SELECT [physical_name] FROM [sys].[master_files]
                WHERE [database_id] = DB_ID('VS2017Db_Create.Local')",
                row => (string) row["physical_name"]);

            if (fileNames.Any())
            {
                ExecuteSqlCommand(Master,
                    @"ALTER DATABASE [VS2017Db_Create.Local] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;EXEC sp_detach_db 'VS2017Db_Create.Local', 'true'");

                fileNames.ForEach(File.Delete);
            }
            if (File.Exists(Filename))
            {
                File.Delete(Filename);
            }
            if (File.Exists(LogFilename))
            {
                File.Delete(LogFilename);
            }
        }

        private static void ExecuteSqlCommand(
            SqlConnectionStringBuilder connectionStringBuilder,
            string commandText)
        {
            using (var connection = new SqlConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = commandText;
                    command.ExecuteNonQuery();
                }
            }
        }

        private static List<T> ExecuteSqlQuery<T>(
            SqlConnectionStringBuilder connectionStringBuilder,
            string queryText,
            Func<SqlDataReader, T> read)
        {
            var result = new List<T>();
            using (var connection = new SqlConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = queryText;
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(read(reader));
                        }
                    }
                }
            }
            return result;
        }

        private static SqlConnectionStringBuilder Master =>
            new SqlConnectionStringBuilder
            {
                DataSource = @"(LocalDB)\MSSQLLocalDB",
                InitialCatalog = "master",
                IntegratedSecurity = true
            };

        private static string Filename => Path.Combine(
            Path.GetDirectoryName(
                typeof(ApplicationDbContext).GetTypeInfo().Assembly.Location),
            "VS2017Db_Create.Local.mdf");

        private static string LogFilename => Path.Combine(
            Path.GetDirectoryName(
                typeof(ApplicationDbContext).GetTypeInfo().Assembly.Location),
            "VS2017Db_Create.Local_log.ldf");
    }
}