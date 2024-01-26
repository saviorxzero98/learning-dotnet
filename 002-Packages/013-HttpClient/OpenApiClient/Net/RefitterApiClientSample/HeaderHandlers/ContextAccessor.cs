namespace RefitterApiClientSample.HeaderHandlers
{
    public class ContextAccessor<T> : IContextGetter<T>, IContextSetter<T> where T : class
    {
        private T? _context;

        /// <summary>
        /// 取值
        /// </summary>
        /// <returns></returns>
        public T? GetValue()
        {
            return _context;
        }

        /// <summary>
        /// 設值
        /// </summary>
        /// <param name="context"></param>
        public void SetValue(T context)
        {
            _context = context;
        }
    }

    public interface IContextGetter<T> where T : class
    {
        /// <summary>
        /// 取值
        /// </summary>
        /// <returns></returns>
        T? GetValue();
    }

    public interface IContextSetter<T> where T : class
    {
        /// <summary>
        /// 設值
        /// </summary>
        /// <param name="context"></param>
        void SetValue(T context);
    }
}
