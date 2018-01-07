namespace Books.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ForeignKeys : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Books", "Author_ID1", "dbo.Authors");
            DropIndex("dbo.Books", new[] { "Author_ID1" });
            DropColumn("dbo.Books", "Author_ID");
            RenameColumn(table: "dbo.Books", name: "Author_ID1", newName: "Author_ID");
            AlterColumn("dbo.Books", "Author_ID", c => c.Int(nullable: false));
            CreateIndex("dbo.Books", "Author_ID");
            AddForeignKey("dbo.Books", "Author_ID", "dbo.Authors", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "Author_ID", "dbo.Authors");
            DropIndex("dbo.Books", new[] { "Author_ID" });
            AlterColumn("dbo.Books", "Author_ID", c => c.Int());
            RenameColumn(table: "dbo.Books", name: "Author_ID", newName: "Author_ID1");
            AddColumn("dbo.Books", "Author_ID", c => c.Int(nullable: false));
            CreateIndex("dbo.Books", "Author_ID1");
            AddForeignKey("dbo.Books", "Author_ID1", "dbo.Authors", "ID");
        }
    }
}
