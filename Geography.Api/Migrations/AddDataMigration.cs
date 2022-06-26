using FluentMigrator;
using Geography.DAL.Enums;

namespace Geography.Api.Migrations
{
    [Migration(20220620103000)]
    public class AddDataMigration : Migration
    {
        public override void Down()
        {
            
        }

        public override void Up()
        {
            Insert.IntoTable(Constants.TableName)
                .Row(new{
                    Sputnik = (int)Sputnic.Kanopus,
                    Cloudiness = 50,
                    DateSnapshot = new DateTime(2022, 01, 10),
                    Geography = "POLYGON((3.6718750000000044 25.73989230448949,3.6718750000000044 18.916011030403887,14.921875000000004 18.916011030403887,14.921875000000004 25.73989230448949,3.6718750000000044 25.73989230448949))"
                })
                .Row(new{
                    Sputnik = (int)Sputnic.BKA,
                    Cloudiness = 10,
                    DateSnapshot = new DateTime(2021, 02, 11),
                    Geography = "POLYGON((35.23238462584629 57.60906073517954,35.54000181334629 56.16893557803185,37.03414243834629 55.972705249882125,37.95699400084629 57.30173453950533,35.23238462584629 57.60906073517954))"
                });
        }
    }
}
