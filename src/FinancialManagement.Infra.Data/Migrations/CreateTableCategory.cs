using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialManagement.Infra.Data.Migrations
{
    [Migration(1)]
    public class CreateTableCategory : Migration
    {
        readonly string TABLE_NAME = "categories";

        public override void Down()
        {
            Delete.Table(TABLE_NAME);
        }

        public override void Up()
        {
            Create.Table(TABLE_NAME)
                  .WithColumn("Id")
                  .AsInt64()
                  .PrimaryKey()
                  .Identity()

                  .WithColumn("Name")
                  .AsString()
                  .Unique()
                  .NotNullable()

                  .WithColumn("CreatedAt")
                  .AsDateTime()
                  .WithDefaultValue(DateTime.UtcNow);
        }
    }
}
