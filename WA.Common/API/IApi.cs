using WA.Common.DataLayer;

namespace WA.Common.API
{
    public interface IApi
    {
        /// <summary>
        /// Поднять апи на нужном адресе
        /// </summary>
        /// <param name="host">хост апи</param>
        /// <param name="port">порт апи</param>
        /// <param name="storage">хранилище с погодой</param>
        void Bind(string host, int port, IStorage storage);
        /// <summary>
        /// Свернуть апи
        /// </summary>
        void UnBind();
    }
}