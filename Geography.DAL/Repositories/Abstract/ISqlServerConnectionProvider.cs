using System.Data.Common;

namespace Geography.DAL.Repositories.Abstract
{
    public interface ISqlServerConnectionProvider
    {
        DbConnection GetDbConnection();
    }
}
