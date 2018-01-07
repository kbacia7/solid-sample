namespace Books.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Age = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Single(nullable: false),
                        Author_ID = c.Int(nullable: false),
                        Author_ID1 = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Authors", t => t.Author_ID1)
                .Index(t => t.Author_ID1);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "Author_ID1", "dbo.Authors");
            DropIndex("dbo.Books", new[] { "Author_ID1" });
            DropTable("dbo.Books");
            DropTable("dbo.Authors");
        }
    }
}
