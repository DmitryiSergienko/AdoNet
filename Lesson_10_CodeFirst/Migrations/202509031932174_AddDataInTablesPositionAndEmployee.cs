namespace Lesson_10_CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDataInTablesPositionAndEmployee : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Employees", name: "Position_Id", newName: "PositionId");
            RenameIndex(table: "dbo.Employees", name: "IX_Position_Id", newName: "IX_PositionId");
            DropColumn("dbo.Positions", "EmployeeID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Positions", "EmployeeID", c => c.Int(nullable: false));
            RenameIndex(table: "dbo.Employees", name: "IX_PositionId", newName: "IX_Position_Id");
            RenameColumn(table: "dbo.Employees", name: "PositionId", newName: "Position_Id");
        }
    }
}
