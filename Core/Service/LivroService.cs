using Blazor.Core.DataAccess;
using Blazor.Core.Domain.Dto;
using Blazor.Core.Domain.Entity;

namespace Blazor.Core.Service;

public class LivroService(DataFactory dataFactory)
{
    public Task<List<LivroEntity>> Get() =>
        dataFactory.ExecuteAsync(uow => uow.LivroRepository.ToListAsync());

    public Task<LivroEntity?> Get(int id) =>
        dataFactory.ExecuteAsync(uow => uow.LivroRepository.FindAsync(id).AsTask());

    public async Task Create(LivroEntity livro)
    {
        await using Uow uow = await dataFactory.CreateUowAsync();
        uow.LivroRepository.Add(livro);
        await uow.SaveChangesAsync();
    }

    public async Task Update(LivroEntity livro)
    {
        await using Uow uow = await dataFactory.CreateUowAsync();
        var livroDb = await uow.LivroRepository.GetWithRelations(livro.Id);
        if(livroDb == null) return;
        LivroDto dto = new(livro);
        dto.ApplyToEntity(livroDb);
        await uow.SaveChangesAsync();
    }

    public async Task<bool> Delete(int id)
    {
        await using Uow uow = await dataFactory.CreateUowAsync();
        if(await uow.LivroRepository.FindAsync(id) is LivroEntity livro)
        {
            uow.LivroRepository.Remove(livro);
            await uow.SaveChangesAsync();
            return true;
        }
        return false;
    }
}
