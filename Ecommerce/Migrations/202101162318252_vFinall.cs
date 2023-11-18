namespace Ecommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class vFinall : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Vendas");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Vendas",
                c => new
                    {
                        IdVenda = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.IdVenda);
            
        }
    }
}
