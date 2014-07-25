using System;

namespace ClassLibrary1.Collections.Entity
{
    /// <summary>
    /// record when an attempt was made to collect a debt
    /// </summary>
    public class CollectionAttempt
    {
        public Guid Id { get; set; }
        public Guid DebtId { get; set; }
        public DateTime Created { get; set; }
    }
}
