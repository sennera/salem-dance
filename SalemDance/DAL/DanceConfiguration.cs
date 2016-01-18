using System.Data.Entity;
using System.Data.Entity.SqlServer;

namespace SalemDance.DAL
{
    public class DanceConfiguration : DbConfiguration
    {
        public DanceConfiguration()
        {
            SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy());
        }
    }
}