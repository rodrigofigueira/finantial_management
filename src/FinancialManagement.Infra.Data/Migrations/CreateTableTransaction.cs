using FluentMigrator;

namespace FinancialManagement.Infra.Data.Migrations
{
    [Migration(2)]
    public class CreateTableTransaction : Migration
    {
        readonly string TABLE_NAME = "transactions";

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

                  .WithColumn("Value")
                  .AsDecimal()

                  .WithColumn("IdCategory")
                  .AsInt64()
                  .NotNullable()

                  .WithColumn("CreatedAt")
                  .AsDateTime()
                  .WithDefaultValue(DateTime.UtcNow);

            Create.ForeignKey()
                  .FromTable(TABLE_NAME).ForeignColumn("IdCategory")
                  .ToTable("categories").PrimaryColumn("Id");
        }
    }
}
