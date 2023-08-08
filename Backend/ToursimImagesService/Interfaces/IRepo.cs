namespace ToursimImagesService.Interfaces
{
    public interface IRepo<K,T>
    {
        public Task<T?> Get(K key);

        public Task<T?> Add(T item);

        public Task<ICollection<T?>> GetAll();

    }
}
