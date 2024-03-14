using RefitWebApiCore.RestServices;

namespace RefitWebApiClient
{
    public interface IRefiRestServiceFactory
    {
        public const string ClientName = "RefitHttpApiClient";

        /// <summary>
        /// 建立 Rest Service
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <returns></returns>
        T Create<T>(RefitHttpApiHeaderContext? context = null) where T : IRefitRestService;
    }
}
