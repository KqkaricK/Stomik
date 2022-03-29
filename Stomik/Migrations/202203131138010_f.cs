namespace Stomik.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class f : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "public.poset",
                c => new
                    {
                        пасспорт = c.Long(nullable: false, identity: true),
                        фио = c.String(),
                        номер_телефона = c.Long(nullable: false),
                        адресс = c.String(),
                    })
                .PrimaryKey(t => t.пасспорт);
            
            CreateTable(
                "public.priem",
                c => new
                    {
                        код_приема = c.Int(nullable: false, identity: true),
                        код_посетителя = c.Long(nullable: false),
                        код_врача = c.Int(nullable: false),
                        код_услуги = c.Int(nullable: false),
                        дата = c.String(),
                        диагноз = c.String(),
                        цена = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.код_приема);
            
            CreateTable(
                "public.vrach",
                c => new
                    {
                        код_врача = c.Int(nullable: false, identity: true),
                        фио = c.String(),
                        кабинет = c.Int(nullable: false),
                        должность = c.String(),
                    })
                .PrimaryKey(t => t.код_врача);
            
            CreateTable(
                "public.yslygi",
                c => new
                    {
                        код_услуги = c.Int(nullable: false, identity: true),
                        название = c.String(),
                        стоимость = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.код_услуги);
            
        }
        
        public override void Down()
        {
            DropTable("public.yslygi");
            DropTable("public.vrach");
            DropTable("public.priem");
            DropTable("public.poset");
        }
    }
}
