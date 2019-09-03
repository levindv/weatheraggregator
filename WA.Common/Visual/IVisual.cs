using System;

namespace WA.Common.Visual
{
    public interface IVisual
    {
        /// <summary>
        /// Получить контрол для отрисовки
        /// </summary>
        /// <returns></returns>
        IVisualComponent GetVisualComponent();
    }
}