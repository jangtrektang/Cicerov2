namespace Cicero.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CodeName = c.String(nullable: false, maxLength: 50),
                        DisplayName = c.String(nullable: false, maxLength: 150),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.CodeName, unique: true);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 100),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(nullable: false),
                        Password = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserRole",
                c => new
                    {
                        User_Id = c.Int(nullable: false),
                        Role_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Role_Id })
                .ForeignKey("dbo.User", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.Role", t => t.Role_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.Role_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRole", "Role_Id", "dbo.Role");
            DropForeignKey("dbo.UserRole", "User_Id", "dbo.User");
            DropIndex("dbo.UserRole", new[] { "Role_Id" });
            DropIndex("dbo.UserRole", new[] { "User_Id" });
            DropIndex("dbo.Role", new[] { "CodeName" });
            DropTable("dbo.UserRole");
            DropTable("dbo.User");
            DropTable("dbo.Role");
        }
    }
}
