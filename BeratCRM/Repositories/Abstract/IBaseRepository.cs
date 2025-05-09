namespace BeratCRM.Repositories.Abstract;

public interface IBaseRepository <T>
{
    public Task<T> Create(T entity);
    public Task<T> Update(T entity);
    public Task<T> Delete(Guid id);
}