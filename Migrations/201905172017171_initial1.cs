namespace PSN2018.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AtividadeRequeridas",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        intAtividadeID_FK = c.Int(nullable: false),
                        intRequisitoID_FK = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Atividades",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        intGrauDif = c.Int(nullable: false),
                        strDsc = c.String(nullable: false),
                        dteDataIni = c.DateTime(),
                        dteDataFin = c.DateTime(),
                        intStatus = c.Int(nullable: false),
                        intSprintID_FK = c.Int(nullable: false),
                        intFuncID_FK = c.Int(nullable: false),
                        dteDataPrev = c.DateTime(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Chats",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        dteData = c.DateTime(nullable: false),
                        strCPFCNPJ_Sender = c.String(),
                        strCPFCNPJ_Receiver = c.String(),
                        strMensagem = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Clientes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        strCPFCNPJ = c.String(nullable: false, maxLength: 20),
                        strNome = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.EquiepeFuncionarios",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        intEquipeID_FK = c.Int(nullable: false),
                        intFuncID_FK = c.Int(nullable: false),
                        strTipo = c.String(nullable: false, maxLength: 15),
                        intStatus = c.Int(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Equipes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        strDsc = c.String(nullable: false, maxLength: 50),
                        intProjetoID_FK = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Funcaos",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        strDsc = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Funcionarios",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        strCPF = c.String(nullable: false, maxLength: 14),
                        strNome = c.String(nullable: false, maxLength: 100),
                        dblProdutiv = c.Double(nullable: false),
                        intAtividades = c.Int(nullable: false),
                        intFuncaoID_FK = c.Int(nullable: false),
                        strFuncao = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.GerenteProjetoes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        strCPF = c.String(nullable: false, maxLength: 14),
                        strNome = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Kanbans",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        intProjetoID_FK = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Logins",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        strCPFCNPJ = c.String(nullable: false, maxLength: 20),
                        strSenha = c.String(nullable: false, maxLength: 50),
                        strNome = c.String(),
                        intNivel = c.Int(nullable: false),
                        ImageUrl = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Projetoes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        strDsc = c.String(nullable: false, maxLength: 100),
                        strObjetivo = c.String(nullable: false, maxLength: 500),
                        intClienteID_FK = c.Int(nullable: false),
                        intGPID_FK = c.Int(nullable: false),
                        dteCriacao = c.DateTime(nullable: false),
                        decPercProj = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Relatorios",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        intReuniaoID_FK = c.Int(nullable: false),
                        intRelatorioID = c.Int(nullable: false),
                        strDsc = c.String(nullable: false, maxLength: 1000),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Requisitoes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        strDsc = c.String(nullable: false, maxLength: 500),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Reuniaos",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        dteData = c.DateTime(nullable: false),
                        strCPFCNPJ = c.String(),
                        strTitulo = c.String(nullable: false, maxLength: 100),
                        strDsc = c.String(nullable: false, maxLength: 200),
                        intStatus = c.Int(),
                        intProjetoID_FK = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.SkillFuncionarios",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        intFuncID_FK = c.Int(nullable: false),
                        intSkillID_FK = c.Int(nullable: false),
                        decPerc = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Skills",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        strDsc = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Sprints",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        intKanbanID_FK = c.Int(nullable: false),
                        intSprintID = c.Int(nullable: false),
                        strDsc = c.String(nullable: false, maxLength: 50),
                        dteDataIni = c.DateTime(),
                        dteDataFin = c.DateTime(),
                        decPerc = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.UploadFileResults",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Tamanho = c.Int(nullable: false),
                        Tipo = c.String(),
                        Caminho = c.String(),
                        intProjetoID_FK = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UploadFileResults");
            DropTable("dbo.Sprints");
            DropTable("dbo.Skills");
            DropTable("dbo.SkillFuncionarios");
            DropTable("dbo.Reuniaos");
            DropTable("dbo.Requisitoes");
            DropTable("dbo.Relatorios");
            DropTable("dbo.Projetoes");
            DropTable("dbo.Logins");
            DropTable("dbo.Kanbans");
            DropTable("dbo.GerenteProjetoes");
            DropTable("dbo.Funcionarios");
            DropTable("dbo.Funcaos");
            DropTable("dbo.Equipes");
            DropTable("dbo.EquiepeFuncionarios");
            DropTable("dbo.Clientes");
            DropTable("dbo.Chats");
            DropTable("dbo.Atividades");
            DropTable("dbo.AtividadeRequeridas");
        }
    }
}
