namespace Core.Utilities.Chaching.Redis.Abstract
{
    public interface ICacheManager
    {
        void Set<T>(string key, T model);
        Task<bool> Clear();
        T Get<T>(string key);
    }
}
