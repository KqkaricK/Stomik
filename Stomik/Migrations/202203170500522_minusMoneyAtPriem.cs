namespace Stomik.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class minusMoneyAtPriem : DbMigration
    {
        public override void Up()
        {
            DropColumn("public.priem", "цена");
        }
        
        public override void Down()
        {
            AddColumn("public.priem", "цена", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
