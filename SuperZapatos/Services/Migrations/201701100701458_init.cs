namespace Services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        description = c.String(),
                        price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        total_in_shelf = c.Int(nullable: false),
                        total_in_vault = c.Int(nullable: false),
                        Self = c.String(),
                        Store_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Stores", t => t.Store_id)
                .Index(t => t.Store_id);
            
            CreateTable(
                "dbo.Stores",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        address = c.String(),
                        Self = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Articles", "Store_id", "dbo.Stores");
            DropIndex("dbo.Articles", new[] { "Store_id" });
            DropTable("dbo.Stores");
            DropTable("dbo.Articles");
        }
    }
}
