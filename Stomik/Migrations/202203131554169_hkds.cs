namespace Stomik.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hkds : DbMigration
    {
        public override void Up()
        {
            CreateIndex("public.priem", "код_посетителя");
            CreateIndex("public.priem", "код_врача");
            CreateIndex("public.priem", "код_услуги");
            AddForeignKey("public.priem", "код_посетителя", "public.poset", "пасспорт", cascadeDelete: true);
            AddForeignKey("public.priem", "код_врача", "public.vrach", "код_врача", cascadeDelete: true);
            AddForeignKey("public.priem", "код_услуги", "public.yslygi", "код_услуги", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("public.priem", "код_услуги", "public.yslygi");
            DropForeignKey("public.priem", "код_врача", "public.vrach");
            DropForeignKey("public.priem", "код_посетителя", "public.poset");
            DropIndex("public.priem", new[] { "код_услуги" });
            DropIndex("public.priem", new[] { "код_врача" });
            DropIndex("public.priem", new[] { "код_посетителя" });
        }
    }
}
