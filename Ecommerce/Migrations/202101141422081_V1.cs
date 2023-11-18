namespace Ecommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categorias",
                c => new
                    {
                        IdCat = c.Int(nullable: false, identity: true),
                        NomeCat = c.String(),
                    })
                .PrimaryKey(t => t.IdCat);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Nome = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        MoradaC = c.String(),
                        NIFC = c.Int(),
                        MoradaEmp = c.String(),
                        NIFEmp = c.Int(),
                        ImageID = c.Int(),
                        NIFF = c.Int(),
                        EmpresaID = c.String(maxLength: 128),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.EmpresaID)
                .ForeignKey("dbo.Images", t => t.ImageID)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.ImageID)
                .Index(t => t.EmpresaID);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Pedidos",
                c => new
                    {
                        IdPd = c.Int(nullable: false, identity: true),
                        DataEntrega = c.DateTime(nullable: false),
                        DataPd = c.DateTime(nullable: false),
                        EstadoPd = c.String(),
                        Levantamento = c.String(nullable: false),
                        MoradaPd = c.String(nullable: false),
                        ValorPd = c.Single(nullable: false),
                        MetodoPag = c.String(nullable: false),
                        Id_Cliente = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.IdPd)
                .ForeignKey("dbo.AspNetUsers", t => t.Id_Cliente)
                .Index(t => t.Id_Cliente);
            
            CreateTable(
                "dbo.Produtos",
                c => new
                    {
                        IdProduto = c.Int(nullable: false, identity: true),
                        NomeP = c.String(nullable: false, maxLength: 60),
                        PrecoP = c.Single(nullable: false),
                        QuantP = c.Int(nullable: false),
                        EstadoP = c.String(),
                        OrigemP = c.String(nullable: false, maxLength: 60),
                        CategoriaP = c.String(nullable: false, maxLength: 60),
                        DesctP = c.Single(nullable: false),
                        Description = c.String(nullable: false),
                        Id_Empresa = c.String(maxLength: 128),
                        Pedidos_IdPd = c.Int(),
                    })
                .PrimaryKey(t => t.IdProduto)
                .ForeignKey("dbo.AspNetUsers", t => t.Id_Empresa)
                .ForeignKey("dbo.Pedidos", t => t.Pedidos_IdPd)
                .Index(t => t.Id_Empresa)
                .Index(t => t.Pedidos_IdPd);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        ImageID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        ImagePath = c.String(),
                    })
                .PrimaryKey(t => t.ImageID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.Vendas",
                c => new
                    {
                        IdVenda = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.IdVenda);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Produtos", "Pedidos_IdPd", "dbo.Pedidos");
            DropForeignKey("dbo.Produtos", "Id_Empresa", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "ImageID", "dbo.Images");
            DropForeignKey("dbo.AspNetUsers", "EmpresaID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Pedidos", "Id_Cliente", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.Produtos", new[] { "Pedidos_IdPd" });
            DropIndex("dbo.Produtos", new[] { "Id_Empresa" });
            DropIndex("dbo.Pedidos", new[] { "Id_Cliente" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "EmpresaID" });
            DropIndex("dbo.AspNetUsers", new[] { "ImageID" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropTable("dbo.Vendas");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Images");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.Produtos");
            DropTable("dbo.Pedidos");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Categorias");
        }
    }
}
