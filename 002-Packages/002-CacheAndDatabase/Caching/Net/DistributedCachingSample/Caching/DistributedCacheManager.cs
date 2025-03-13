using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;

namespace DistributedCachingSample.Caching
{
    public class DistributedCacheManager : ICacheManager
    {
        private readonly IDistributedCache _cache;
        private readonly DistributedCacheEntryOptions _options;

        public DistributedCacheManager(
            IDistributedCache cache,
            IOptions<DistributedCacheEntryOptions> options)
        {
            _cache = cache;
            _options = options.Value;
        }

        public virtual async ValueTask<T> GetOrCreateAsync<T>(
            string key,
            Func<CancellationToken, ValueTask<T>> factory,
            IEnumerable<string>? tags = null,
            CancellationToken cancellationToken = default)
        {
            var valueBytes = await _cache.GetAsync(key, cancellationToken);
            T? value = default(T);
            
            if (valueBytes != null)
            {
                var valueJson = Encoding.UTF8.GetString(valueBytes, 0, valueBytes.Length);
                value = JsonConvert.DeserializeObject<T>(valueJson);
            }
            
            if (value == null)
            {
                value = await factory.Invoke(cancellationToken);
            }
            return value;
        }

        public virtual async ValueTask SetAsync<T>(
            string key,
            T value,
            IEnumerable<string>? tags = null,
            CancellationToken cancellationToken = default)
        {
            // Set Cache Value
            var valueJson = JsonConvert.SerializeObject(value);
            var valueBytes = Encoding.UTF8.GetBytes(valueJson);
            await _cache.SetAsync(key, valueBytes, _options, cancellationToken);

            // Set Cache Tag
            await SetCacheTagsAsync(key, tags, cancellationToken);
        }

        public virtual async ValueTask RemoveAsync(
            string key,
            CancellationToken cancellationToken = default)
        {
            // Remove Cache Value
            await _cache.RemoveAsync(key, cancellationToken);

            // Remove Cache Tag
            await RemoveCacheTagsByKeyAsync(key, cancellationToken);
        }

        public virtual async ValueTask RemoveAsync(
            IEnumerable<string> keys,
            CancellationToken cancellationToken = default)
        {
            foreach (var key in keys)
            {
                await RemoveAsync(key, cancellationToken);
            }
        }

        public virtual async ValueTask RemoveByTagAsync(
            IEnumerable<string> tags,
            CancellationToken cancellationToken = default)
        {
            var removeKeys = await RemoveCacheTagsAsync(tags, cancellationToken);

            foreach (var key in removeKeys)
            {
                await _cache.RemoveAsync(key, cancellationToken);
            }
        }

        public virtual async ValueTask RemoveByTagAsync(
            string tag,
            CancellationToken cancellationToken = default)
        {
            var removeKeys = await RemoveCacheTagAsync(tag, cancellationToken);

            foreach (var key in removeKeys)
            {
                await _cache.RemoveAsync(key, cancellationToken);
            }
        }


        #region Cache Tags

        protected virtual async ValueTask SetCacheTagsAsync(
            CacheTagContext context,
            CancellationToken cancellationToken = default)
        {
            var valueJson = JsonConvert.SerializeObject(context);
            var valueBytes = Encoding.UTF8.GetBytes(valueJson);
            await _cache.SetAsync(CacheTagContext.CacheKey, valueBytes, _options, cancellationToken);
        }

        protected virtual async ValueTask SetCacheTagsAsync(
            string key,
            IEnumerable<string>? tags = null,
            CancellationToken cancellationToken = default)
        {
            var tagContext = await GetCacheTagContextAsync(cancellationToken);

            if (tags == null)
            {
                tagContext = tagContext.AddOrUpdateTags(key);
            }
            else
            {
                tagContext = tagContext.AddOrUpdateTags(key, tags.ToArray());
            }
            await SetCacheTagsAsync(tagContext, cancellationToken);
        }

        protected virtual async ValueTask RemoveCacheTagsByKeyAsync(
            string key,
            CancellationToken cancellationToken = default)
        {
            var tagContext = await GetCacheTagContextAsync(cancellationToken);

            tagContext.PopKeyTags(key);

            await SetCacheTagsAsync(tagContext, cancellationToken);
        }

        protected virtual async ValueTask<List<string>> RemoveCacheTagAsync(
            string tag,
            CancellationToken cancellationToken = default)
        {
            var tagContext = await GetCacheTagContextAsync(cancellationToken);

            var keys = tagContext.PopKeyTags(tag);

            await SetCacheTagsAsync(tagContext, cancellationToken);

            return keys;
        }
        protected virtual async ValueTask<List<string>> RemoveCacheTagsAsync(
           IEnumerable<string> tags,
           CancellationToken cancellationToken = default)
        {
            var keys = new List<string>();
            var tagContext = await GetCacheTagContextAsync(cancellationToken);

            foreach (var tag in tags)
            {
                keys.AddRange(tagContext.PopTagKeys(tag));
            }

            await SetCacheTagsAsync(tagContext, cancellationToken);

            return keys.Distinct().ToList();
        }

        protected virtual async ValueTask<CacheTagContext> GetCacheTagContextAsync(
            CancellationToken cancellationToken = default)
        {
            var tagContext = await GetOrCreateAsync(CacheTagContext.CacheKey,
                                                   (token) => ValueTask.FromResult(new CacheTagContext()),
                                                   cancellationToken: cancellationToken);
            return tagContext;
        }

        #endregion
    }

    public class CacheTagContext
    {
        public const string CacheKey = "__TaggableCache:Tags";

        /// <summary>
        /// Tag 對應的 Cache Keys
        /// </summary>
        public Dictionary<string, List<string>> TagKeys { get; set; }

        /// <summary>
        /// Cache Key 擁有的 Tags
        /// </summary>
        public Dictionary<string, List<string>> KeyTags { get; set; }


        public CacheTagContext()
        {
            TagKeys = new Dictionary<string, List<string>>();
            KeyTags = new Dictionary<string, List<string>>();
        }


        public CacheTagContext AddOrUpdateTags(string key, params string[] tags)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return this;
            }

            var keyTags = tags.ToList();

            if (KeyTags.ContainsKey(key))
            {
                if (KeyTags[key] == null)
                {
                    KeyTags[key] = new List<string>();
                }

                // Cache Key 擁有的 Tags
                var addTags = KeyTags[key].Where(i => !keyTags.Contains(i));
                var removeTags = keyTags.Where(i => !KeyTags[key].Contains(i));
                KeyTags[key] = keyTags;

                // Tag 對應的 Cache Keys
                foreach (var addTag in addTags)
                {
                    AddTagKeys(addTag, key);
                }
                foreach (var removeTag in removeTags)
                {
                    RemoveTagKeys(removeTag, key);
                }
            }
            else
            {
                if (keyTags.Count != 0)
                {
                    // Cache Key 擁有的 Tags
                    KeyTags.Add(key, keyTags);

                    // Tag 對應的 Cache Keys
                    foreach (var tag in keyTags)
                    {
                        AddTagKeys(tag, key);
                    }
                }               
            }
            return this;
        }

        protected void AddTagKeys(string tag, string key)
        {
            if (TagKeys.ContainsKey(tag))
            {
                if (TagKeys[tag] == null)
                {
                    TagKeys[tag] = new List<string>();
                }

                if (!TagKeys[tag].Contains(key))
                {
                    TagKeys[tag].Add(key);
                }
            }
            else
            {
                TagKeys.Add(tag, new List<string> { key });
            }
        }

        protected void RemoveTagKeys(string tag, string key)
        {
            if (TagKeys.ContainsKey(tag) &&
                TagKeys[tag] != null &&
                TagKeys[tag].Contains(key))
            {
                TagKeys[tag].Remove(key);
            }
        }

        protected void RemoveKeyTags(string key, string tag)
        {
            if (KeyTags.ContainsKey(key) &&
                KeyTags[key] != null &&
                KeyTags[key].Contains(tag))
            {
                KeyTags[key].Remove(tag);
            }
        }


        public List<string> PopTagKeys(string tag)
        {
            if (string.IsNullOrEmpty(tag) ||
               !TagKeys.ContainsKey(tag))
            {
                return new List<string>();
            }

            var keys = TagKeys[tag] ?? new List<string>();
            TagKeys.Remove(tag);

            foreach (var key in keys)
            {
                RemoveKeyTags(key, tag);
            }
            return keys;
        }

        public List<string> PopKeyTags(string key)
        {
            if (string.IsNullOrEmpty(key) ||
                !KeyTags.ContainsKey(key))
            {
                return new List<string>();
            }
            
            var tags = KeyTags[key] ?? new List<string>();
            KeyTags.Remove(key);

            foreach (var tag in tags)
            {
                RemoveTagKeys(tag, key);
            }
            return tags;
        }
    }
}
