using System;

namespace ClassLibrary1.Collections.Entity
{
    /// <summary>
    /// record when an attempt was made to collect a debt
    /// constraint on debtid and attempt prevent two processes trying to process the same debt
    /// </summary>
    public class CollectionAttempt
    {
        public Guid DebtId { get; set; }
        //public DateTime Created { get; set; }

        public int Attempt { get; set; }

        public string Reference { get; set; }
    }
}
