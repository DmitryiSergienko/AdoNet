namespace Lesson_10_CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixPictureCustomerRelation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Customers", "Picture_Id", "dbo.Pictures");
            DropIndex("dbo.Customers", new[] { "Picture_Id" });
            AddColumn("dbo.Pictures", "CustomerID", c => c.Int(nullable: false));
            CreateIndex("dbo.Pictures", "CustomerID");
            AddForeignKey("dbo.Pictures", "CustomerID", "dbo.Customers", "Id", cascadeDelete: true);
            DropColumn("dbo.Customers", "Pictures");
            DropColumn("dbo.Customers", "Picture_Id");
            DropColumn("dbo.Pictures", "Customer_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Pictures", "Customer_ID", c => c.Int(nullable: false));
            AddColumn("dbo.Customers", "Picture_Id", c => c.Int(nullable: false));
            AddColumn("dbo.Customers", "Pictures", c => c.Binary());
            DropForeignKey("dbo.Pictures", "CustomerID", "dbo.Customers");
            DropIndex("dbo.Pictures", new[] { "CustomerID" });
            DropColumn("dbo.Pictures", "CustomerID");
            CreateIndex("dbo.Customers", "Picture_Id");
            AddForeignKey("dbo.Customers", "Picture_Id", "dbo.Pictures", "Id", cascadeDelete: true);
        }
    }
}
