namespace Lesson_10_CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPictures : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Pictures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name_pict = c.String(nullable: false),
                        Customer_ID = c.Int(nullable: false),
                        Pictures = c.Binary(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Customers", "Picture_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Customers", "Picture_Id");
            AddForeignKey("dbo.Customers", "Picture_Id", "dbo.Pictures", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Customers", "Picture_Id", "dbo.Pictures");
            DropIndex("dbo.Customers", new[] { "Picture_Id" });
            DropColumn("dbo.Customers", "Picture_Id");
            DropTable("dbo.Pictures");
        }
    }
}
