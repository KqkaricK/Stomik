namespace Stomik.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rename : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "public.poset", name: "ид", newName: "Id");
            RenameColumn(table: "public.poset", name: "фио", newName: "FIO");
            RenameColumn(table: "public.poset", name: "пасспорт", newName: "Pas");
            RenameColumn(table: "public.poset", name: "номер_телефона", newName: "Nomber");
            RenameColumn(table: "public.poset", name: "адресс", newName: "Adress");
            RenameColumn(table: "public.priem", name: "код_приема", newName: "kod_priem");
            RenameColumn(table: "public.priem", name: "код_посетителя", newName: "kod_pos");
            RenameColumn(table: "public.priem", name: "код_врача", newName: "kod_vrach");
            RenameColumn(table: "public.priem", name: "код_услуги", newName: "kod_yslugi");
            RenameColumn(table: "public.priem", name: "дата", newName: "date");
            RenameColumn(table: "public.priem", name: "диагноз", newName: "diagnos");
            RenameColumn(table: "public.vrach", name: "код_врача", newName: "Id");
            RenameColumn(table: "public.vrach", name: "фио", newName: "FIO");
            RenameColumn(table: "public.vrach", name: "кабинет", newName: "cabinet");
            RenameColumn(table: "public.vrach", name: "должность", newName: "dol");
            RenameColumn(table: "public.yslygi", name: "код_услуги", newName: "Id");
            RenameColumn(table: "public.yslygi", name: "название", newName: "Name");
            RenameColumn(table: "public.yslygi", name: "стоимость", newName: "Money");
            RenameIndex(table: "public.priem", name: "IX_код_посетителя", newName: "IX_kod_pos");
            RenameIndex(table: "public.priem", name: "IX_код_врача", newName: "IX_kod_vrach");
            RenameIndex(table: "public.priem", name: "IX_код_услуги", newName: "IX_kod_yslugi");
        }
        
        public override void Down()
        {
            RenameIndex(table: "public.priem", name: "IX_kod_yslugi", newName: "IX_код_услуги");
            RenameIndex(table: "public.priem", name: "IX_kod_vrach", newName: "IX_код_врача");
            RenameIndex(table: "public.priem", name: "IX_kod_pos", newName: "IX_код_посетителя");
            RenameColumn(table: "public.yslygi", name: "Money", newName: "стоимость");
            RenameColumn(table: "public.yslygi", name: "Name", newName: "название");
            RenameColumn(table: "public.yslygi", name: "Id", newName: "код_услуги");
            RenameColumn(table: "public.vrach", name: "dol", newName: "должность");
            RenameColumn(table: "public.vrach", name: "cabinet", newName: "кабинет");
            RenameColumn(table: "public.vrach", name: "FIO", newName: "фио");
            RenameColumn(table: "public.vrach", name: "Id", newName: "код_врача");
            RenameColumn(table: "public.priem", name: "diagnos", newName: "диагноз");
            RenameColumn(table: "public.priem", name: "date", newName: "дата");
            RenameColumn(table: "public.priem", name: "kod_yslugi", newName: "код_услуги");
            RenameColumn(table: "public.priem", name: "kod_vrach", newName: "код_врача");
            RenameColumn(table: "public.priem", name: "kod_pos", newName: "код_посетителя");
            RenameColumn(table: "public.priem", name: "kod_priem", newName: "код_приема");
            RenameColumn(table: "public.poset", name: "Adress", newName: "адресс");
            RenameColumn(table: "public.poset", name: "Nomber", newName: "номер_телефона");
            RenameColumn(table: "public.poset", name: "Pas", newName: "пасспорт");
            RenameColumn(table: "public.poset", name: "FIO", newName: "фио");
            RenameColumn(table: "public.poset", name: "Id", newName: "ид");
        }
    }
}
