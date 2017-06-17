namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingPersonTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Persons",
                c => new
                    {
                        TaxCode = c.String(nullable: false, maxLength: 128),
                        Firstname = c.String(),
                        Surname = c.String(),
                    })
                .PrimaryKey(t => t.TaxCode);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Persons");
        }
    }
}
