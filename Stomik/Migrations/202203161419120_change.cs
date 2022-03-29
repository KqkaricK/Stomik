namespace Stomik.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("public.priem", "код_посетителя", "public.poset");
            DropIndex("public.priem", new[] { "код_посетителя" });
            DropPrimaryKey("public.poset");
            AddColumn("public.poset", "ид", c => c.Int(nullable: false, identity: true));
            AlterColumn("public.poset", "пасспорт", c => c.Long(nullable: false));
            AlterColumn("public.priem", "код_посетителя", c => c.Int(nullable: false));
            AddPrimaryKey("public.poset", "ид");
            CreateIndex("public.priem", "код_посетителя");
            AddForeignKey("public.priem", "код_посетителя", "public.poset", "ид", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("public.priem", "код_посетителя", "public.poset");
            DropIndex("public.priem", new[] { "код_посетителя" });
            DropPrimaryKey("public.poset");
            AlterColumn("public.priem", "код_посетителя", c => c.Long(nullable: false));
            AlterColumn("public.poset", "пасспорт", c => c.Long(nullable: false, identity: true));
            DropColumn("public.poset", "ид");
            AddPrimaryKey("public.poset", "пасспорт");
            CreateIndex("public.priem", "код_посетителя");
            AddForeignKey("public.priem", "код_посетителя", "public.poset", "пасспорт", cascadeDelete: true);
        }
    }
}
