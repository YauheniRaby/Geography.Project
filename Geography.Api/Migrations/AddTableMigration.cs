using FluentMigrator;
using FluentMigrator.SqlServer;

namespace Geography.Api.Migrations
{
    [Migration(20220620100000)]
    public class AddTableMigration : Migration
    {
        public override void Down()
        {
            
        }

        public override void Up()
        {
            Create.Table(Constants.TableName)
                .WithColumn(Constants.ColId).AsInt32().NotNullable().PrimaryKey().Identity(1, 1)
                .WithColumn(Constants.ColSputnik).AsInt32().NotNullable()
                .WithColumn(Constants.ColDateSnapshot).AsDate().NotNullable()
                .WithColumn(Constants.ColCloudiness).AsDecimal(3, 0).Nullable()
                .WithColumn(Constants.ColGeography).AsCustom(Constants.GeographyType).NotNullable();
        }
    }
}
