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

            var estudanteRetorno = new EstudanteDto(novoEstudante.Id, novoEstudante.Nome);

            return Results.Ok(estudanteRetorno);
        });

        //Retornar todos os estudantes
        rotasEstudantes.MapGet("", async (AppDbContext context) =>
        {
            var estudantes = await context
            .Estudantes
            .Where(estudante => estudante.Ativo)
            .Select(estudante => new EstudanteDto(estudante.Id, estudante.Nome))
            .ToListAsync();
            return estudantes;
        });

        //Atualizar registro
        rotasEstudantes.MapPut("{id:guid}", async (Guid id,UpdateEstudanteRequest request, AppDbContext context) =>
        {
            var estudante = await context.Estudantes.SingleOrDefaultAsync(estudante => estudante.Id == id);

            if(estudante == null)
            {
                return Results.NotFound();
            }

            estudante.AtualizarNome(request.Nome);

            await context.SaveChangesAsync();

            return Results.Ok(new EstudanteDto(estudante.Id, estudante.Nome));
        });

        //SoftDelete
        rotasEstudantes.MapDelete("{id}", async (Guid id, AppDbContext context) => {
            var estudante = await context.Estudantes.SingleOrDefaultAsync(estudante => estudante.Id == id);

            if (estudante == null)
            {
                return Results.NotFound();
            }

            estudante.Desativar();

            await context.SaveChangesAsync();

            return Results.Ok();
        });
    }


}

