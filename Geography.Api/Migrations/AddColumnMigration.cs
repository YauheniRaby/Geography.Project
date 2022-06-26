using FluentMigrator;

namespace Geography.Api.Migrations
{
    [Migration(20220620105000)]
    public class AddColumnMigration : Migration
    {
        public override void Down()
        {

        }

        public override void Up()
        {
            Alter.Table(Constants.TableName).AddColumn(Constants.ColCoil).AsInt32().NotNullable().WithDefaultValue(1);
        } 
    }
}
