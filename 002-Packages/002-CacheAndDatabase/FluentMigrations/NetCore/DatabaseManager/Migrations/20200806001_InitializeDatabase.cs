using DatabaseManager.Entities;
using FluentMigrator;

namespace DatabaseManager.Migrations
{
    [Migration(20200808001)]
    public class InitializeDatabase : Migration
    {
        public override void Up()
        {
            Create.Table(UserProfile.TableName)
                  .WithColumn(nameof(UserProfile.Id)).AsInt64().PrimaryKey().Identity()
                  .WithColumn(nameof(UserProfile.Name)).AsString()
                  .WithColumn(nameof(UserProfile.IsBlocked)).AsBoolean()
                  .WithColumn(nameof(UserProfile.CreateDate)).AsDateTime();
        }

        public override void Down()
        {
            Delete.Table(UserProfile.TableName);
        }
    }
}
