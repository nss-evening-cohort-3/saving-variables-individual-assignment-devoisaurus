namespace SavingVariables.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Expressions",
                c => new
                    {
                        ExpressionId = c.Int(nullable: false, identity: true),
                        Variable = c.String(nullable: false, maxLength: 4000),
                        Constant = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ExpressionId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Expressions");
        }
    }
}
