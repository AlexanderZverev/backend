namespace Data.Abstract
{
    public abstract class Repository
    {
        protected string ConnectionString { get; }

        protected Repository(string connectionString) => ConnectionString = connectionString;
    }
}