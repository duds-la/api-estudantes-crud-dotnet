using ApiCrud.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiCrud.Estudantes;
public static class EstudantesRotas
{
    public static void AddRotasEstudantes(this WebApplication app)
    {
        //Criando um agrupador de rotas
        var rotasEstudantes = app.MapGroup("estudantes");
        //Criando um estudante
        rotasEstudantes.MapPost(
            "", async (AddEstudanteRequest request, AppDbContext context) =>
        {
            var jaExiste = await context.Estudantes
            .AnyAsync(estudante => estudante.Nome == request.Nome);


            if (jaExiste)
            {
                return Results.Conflict("Já existe um estudante com esse nome cadastrado");
            }

            var novoEstudante = new Estudante(request.Nome);

            await context.Estudantes.AddAsync(novoEstudante);

            //Onde quero logar as coisas mt bom para debug
            //optionsBuilder.LogTo(Console.WriteLine)

            await context.SaveChangesAsync();

            return Results.Ok(novoEstudante);
        });
    }


}

