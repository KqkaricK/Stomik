namespace Stomik.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class plusLogin : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "public.autoriz",
                c => new
                    {
                        login = c.String(nullable: false, maxLength: 128),
                        password = c.String(),
                    })
                .PrimaryKey(t => t.login);
            
        }
        
        public override void Down()
        {
            DropTable("public.autoriz");
        }
    }
}
