namespace TrainingMSHR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addmodeldivision : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Divisions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Departments", "Division_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Departments", "Division_Id");
            AddForeignKey("dbo.Departments", "Division_Id", "dbo.Divisions", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Departments", "Division_Id", "dbo.Divisions");
            DropIndex("dbo.Departments", new[] { "Division_Id" });
            DropColumn("dbo.Departments", "Division_Id");
            DropTable("dbo.Divisions");
        }
    }
}
