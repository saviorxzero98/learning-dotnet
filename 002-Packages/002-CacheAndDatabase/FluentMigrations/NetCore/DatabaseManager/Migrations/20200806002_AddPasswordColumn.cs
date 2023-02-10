using DatabaseManager.Entities;
using FluentMigrator;

namespace DatabaseManager.Migrations
{
    [Migration(20200808002)]
    public class AddPasswordColumn : Migration
    {
        public override void Up()
        {
            Alter.Table(UserProfile.TableName)
                 .AddColumn(nameof(UserProfile.Password)).AsString();
        }

        public override void Down()
        {
            
        }
    }
}
