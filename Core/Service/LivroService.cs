using Blazor.Core.DataAccess;
using Blazor.Core.Domain.Dto;
using Blazor.Core.Domain.Entity;

namespace Blazor.Core.Service;

public class LivroService(DataFactory dataFactory)
{
    public Task<List<LivroEntity>> ToListAsync() =>
        dataFactory.ExecuteAsync(uow => uow.LivroRepository.ToListAsync());

    public Task<LivroEntity?> FindAsync(int id) =>
        dataFactory.ExecuteAsync(uow => uow.LivroRepository.FindAsync(id));

    public async Task Create(LivroEntity entity)
    {
        await using Uow uow = await dataFactory.CreateUowAsync();
        uow.LivroRepository.Add(entity);
        await uow.SaveChangesAsync();
    }

    public async Task<bool> Update(LivroEntity entity)
    {
        await using Uow uow = await dataFactory.CreateUowAsync();
        var livroDb = await uow.LivroRepository.FindAsync(entity.Id);
        if(livroDb == null) return false;
        LivroDto dto = new(entity);
        dto.ApplyToEntity(livroDb);
        await uow.SaveChangesAsync();
        return true;
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
