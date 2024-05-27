using ApiCrud.Estudantes;
using Microsoft.EntityFrameworkCore;

namespace ApiCrud.Data
{
    //O context é usado para um
    //conjunto de tabelas ou todo Banco de dados
    //então quando formos fazer algo relacionado a isso iremos
    //utilizar essa classe
    public class AppDbContext : DbContext
    {
        //Isso é como declaramos a tabela para ser usada no
        //nosso sistema
        public DbSet<Estudante> Estudantes { get; set; }

        //Vamos dizer como o app DB Context
        //vai se conectar ao nosso banco
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Aqui estamos dizendo como queremos que ele se comunique
            optionsBuilder.UseSqlite("Data Source=Banco.sqlite");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
