namespace Lesson_10_CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModelUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Positions", "EmployeeID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Positions", "EmployeeID");
        }
    }
}
