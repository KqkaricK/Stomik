namespace Stomik.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class plusOtmetka : DbMigration
    {
        public override void Up()
        {
            AddColumn("public.priem", "Otmetka", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("public.priem", "Otmetka");
        }
    }
}
