using PSN2018.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PSN2018.Models
{
    public class PSN2018Context : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public PSN2018Context() : base("name=PSN2018Context")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<PSN2018Context, Configuration>());
        }

        public System.Data.Entity.DbSet<PSN2018.Models.Atividade> Atividades { get; set; }

        public System.Data.Entity.DbSet<PSN2018.Models.AtividadeRequerida> AtividadeRequeridas { get; set; }

        public System.Data.Entity.DbSet<PSN2018.Models.Chat> Chats { get; set; }

        public System.Data.Entity.DbSet<PSN2018.Models.Cliente> Clientes { get; set; }

        public System.Data.Entity.DbSet<PSN2018.Models.EquiepeFuncionarios> EquiepeFuncionarios { get; set; }

        public System.Data.Entity.DbSet<PSN2018.Models.Equipe> Equipes { get; set; }

        public System.Data.Entity.DbSet<PSN2018.Models.Funcao> Funcaos { get; set; }

        public System.Data.Entity.DbSet<PSN2018.Models.Funcionario> Funcionarios { get; set; }

        public System.Data.Entity.DbSet<PSN2018.Models.GerenteProjeto> GerenteProjetoes { get; set; }

        public System.Data.Entity.DbSet<PSN2018.Models.Kanban> Kanbans { get; set; }

        public System.Data.Entity.DbSet<PSN2018.Models.Login> Logins { get; set; }

        public System.Data.Entity.DbSet<PSN2018.Models.Projeto> Projetoes { get; set; }

        public System.Data.Entity.DbSet<PSN2018.Models.Relatorio> Relatorios { get; set; }

        public System.Data.Entity.DbSet<PSN2018.Models.Requisito> Requisitoes { get; set; }

        public System.Data.Entity.DbSet<PSN2018.Models.Reuniao> Reuniaos { get; set; }

        public System.Data.Entity.DbSet<PSN2018.Models.Skill> Skills { get; set; }

        public System.Data.Entity.DbSet<PSN2018.Models.SkillFuncionarios> SkillFuncionarios { get; set; }

        public System.Data.Entity.DbSet<PSN2018.Models.Sprint> Sprints { get; set; }

        public System.Data.Entity.DbSet<PSN2018.Models.UploadFileResult> UploadFileResults { get; set; }
    }
}
