using FluentMigrator;

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
            if (!Schema.Table(TABLE_NAME).Exists())
            {
                Create.Table(TABLE_NAME)
                      .WithColumn("Id")
                      .AsInt32()
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
}
