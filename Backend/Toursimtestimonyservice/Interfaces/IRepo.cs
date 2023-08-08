namespace Toursimtestimonyservice.Interfaces
{
    public interface IRepo<k,T>
    {
        public Task<T> Add(T item);
        public Task<T> Delete(k key);

        public Task<ICollection<T>> GetAll();

    }
}
