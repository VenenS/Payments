using System;

namespace Payments.Domain.Common
{
    /// <summary>
    /// Базовая сущность
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// Время создания сущности
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Время редактирования сущности
        /// </summary>
        public DateTime UpdatedDate { get; set; }
    }
}
