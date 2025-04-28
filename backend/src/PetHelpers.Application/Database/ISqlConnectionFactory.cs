using System.Data;

namespace PetHelpers.Application.Database;

public interface ISqlConnectionFactory
{
    IDbConnection Create();
}