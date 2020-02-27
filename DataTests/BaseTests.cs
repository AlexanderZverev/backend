using DbUp;
using NUnit.Framework;
using System;
using System.Linq;
using System.Reflection;

namespace DataTests
{
    [TestFixture]
    class BaseTests
    {
        protected string ConnectionString =>
            "Server=localhost\\SQLEXPRESS;Database=testDbForBackendTask;Trusted_Connection=True;";

        [SetUp]
        public void BaseSetUp()
        {
            TestContext.Out.WriteLine(AppDomain.CurrentDomain.GetAssemblies().First(assembly => assembly.FullName.Contains("Data")).FullName);
            EnsureDatabase.For.SqlDatabase(ConnectionString);
            DeployChanges.To
                .SqlDatabase(ConnectionString, null)
                .WithScriptsEmbeddedInAssembly(Assembly.GetAssembly(typeof(Data.Abstract.Repository))) 
                .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly()) 
               
                .Build().PerformUpgrade();
        }

        [TearDown]
        public void BaseTearDown() => DropDatabase.For.SqlDatabase(ConnectionString);
    }
}