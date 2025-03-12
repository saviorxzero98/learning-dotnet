namespace DistributedCachingSample.Services
{
    public class CacheDataResult<TEntity>
    {
        public string Key { get; set; }

        public TEntity? Data { get; set; }

        public bool IsHit { get; set; } = false;


        public CacheDataResult(string key) 
        {
            Key = key;
        }
        public CacheDataResult(string key, TEntity? data)
        {
            Key = key;
            Data = data;
            IsHit = (data != null);
        }
        public CacheDataResult(string key, TEntity? data, bool isHit = true)
        {
            Key = key;
            Data = data;
            IsHit = isHit;
        }
    }
}
