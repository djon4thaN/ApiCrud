using ApiCrud.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiCrud.Bibliotecas;

public static class BibliotecasRotas
{
    public static void AddRotasBibliotecas(this WebApplication app)
    {
        var rotasBibliotecas = app.MapGroup("bibliotecas");

        // Para criar
        rotasBibliotecas.MapPost("",
            async (AddBibliotecaRequest request, AppDbContext context) =>
            {

            var jaExiste = await context.Bibliotecas
                .AnyAsync(biblioteca => biblioteca.Nome == request.Nome);

            if (jaExiste)
                return Results.Conflict("Ja existe!");

            var novoBiblioteca = new Biblioteca(request.Nome, request.Livro, request.Dia, request.Devolucao);
            await context.Bibliotecas.AddAsync(novoBiblioteca);
            await context.SaveChangesAsync();

            return Results.Ok(novoBiblioteca);
            });

        // Retornar todos os bibliotecarios cadastrados
        rotasBibliotecas.MapGet("livrosAlugados", async (AppDbContext context) =>
        {
            var bibliotecas = await context
                .Bibliotecas
                .Where(bibliotecas => bibliotecas.Ativo == true)
                .ToListAsync();
            return bibliotecas;
        });

        rotasBibliotecas.MapGet("livrosDevolvidos", async (AppDbContext context) =>
        {
            var bibliotecas = await context
                .Bibliotecas
                .Where(bibliotecas => bibliotecas.Ativo == false)
                .ToListAsync();
            return bibliotecas;
        });

        // Atualizar
        rotasBibliotecas.MapPut("{id:guid}",
            async (Guid id, UpdateLivroRequest request, AppDbContext context) =>
            {
            var biblioteca = await context.Bibliotecas
                .SingleOrDefaultAsync(biblioteca => biblioteca.Id == id);

            if (biblioteca == null)
                return Results.NotFound();

            biblioteca.AtualizarConteudo(request.Devolucao);
            biblioteca.Ativo = false;

                await context.SaveChangesAsync();
                return Results.Ok(biblioteca);
            });

        // Deletar
        rotasBibliotecas.MapDelete("{id}",
            async (Guid id, AppDbContext context) =>
            {
            var biblioteca = await context.Bibliotecas // DbSet<Biblioteca>
                .FindAsync(id); // Task<Biblioteca?>

                if (biblioteca == null)
                {
                    return Results.NotFound("O livro em questao não foi encontrado");
                }

                context.Bibliotecas.Remove(biblioteca);

            await context.SaveChangesAsync();
                return Results.Ok("Excluido com sucesso");

            });
    }
}
