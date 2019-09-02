namespace WA.Common.API
{
    public interface IApi
    {
        /// <summary>
        /// Поднять апи на нужном адресе
        /// </summary>
        /// <param name="host">хост апи</param>
        /// <param name="port">порт апи</param>
        void Bind(string host, int port);
        /// <summary>
        /// Свернуть апи
        /// </summary>
        void UnBind();
    }
}