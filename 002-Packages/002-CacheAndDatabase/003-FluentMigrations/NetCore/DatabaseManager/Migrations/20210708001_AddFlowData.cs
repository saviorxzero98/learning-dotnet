using DatabaseManager.Entities;
using FluentMigrator;

namespace DatabaseManager.Migrations
{
    [Migration(20210708001)]
    public class AddFlowData : Migration
    {
        public override void Up()
        {
            Create.Table(FlowData.TableName)
                  .WithColumn(nameof(FlowData.Id)).AsAnsiString().NotNullable().PrimaryKey()
                  .WithColumn(nameof(FlowData.Version)).AsInt32().NotNullable().PrimaryKey()
                  .WithColumn(nameof(FlowData.Content)).AsString();
        }

        public override void Down()
        {
            Delete.Table(FlowData.TableName);
        }
    }
}
