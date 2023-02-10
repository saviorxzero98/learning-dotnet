using DatabaseManager.Entities;
using FluentMigrator;
using SqlKata;
using SqlKata.Compilers;

namespace DatabaseManager.Migrations
{
    [Migration(20200808003)]
    public class AddView : Migration
    {
        private const string SqlServerType = "sqlServer";
        private const string PostgreSqlType = "postgres";
        private const string SqliteType = "sqlite";

        public override void Up()
        {
            CreateView(SqlServerType, UserProfileView.ViewName);
            CreateView(PostgreSqlType, UserProfileView.ViewName);
            CreateView(SqliteType, UserProfileView.ViewName);
        }

        public override void Down()
        {
            DropView(UserProfileView.ViewName);
        }

        private void CreateView(string dbType, string viewName)
        {
            string selectStatement = GetViewSelectStatement(dbType);

            if (!string.IsNullOrEmpty(selectStatement))
            {
                IfDatabase(dbType).Execute.Sql($"CREATE VIEW {viewName} AS {selectStatement}");
            }
        }

        private string GetViewSelectStatement(string dbType)
        {
            var query = new Query(UserProfile.TableName)
                                .Select(nameof(UserProfile.Name),
                                        nameof(UserProfile.Password));

            Compiler compiler;
            switch (dbType)
            {
                case SqlServerType:
                    compiler = new SqlServerCompiler();
                    break;
                case PostgreSqlType:
                    compiler = new PostgresCompiler();
                    break;
                case SqliteType:
                    compiler = new SqliteCompiler();
                    break;
                default:
                    return string.Empty;
            }

            SqlResult sqlResult = compiler.Compile(query);
            return sqlResult.Sql;
        }

        private void DropView(string viewName)
        {
            Execute.Sql($"DROP VIEW {viewName}");
        }
    }
}
