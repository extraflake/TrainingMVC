namespace TrainingMSHR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateModelDepartment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Departments", "CreatedOn", c => c.DateTimeOffset(nullable: false, precision: 7));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Departments", "CreatedOn");
        }
    }
}
