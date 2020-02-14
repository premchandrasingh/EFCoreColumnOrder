using System;
using System.Collections.Generic;
using System.Text;

namespace RepoSample.Entities
{
    public class Order : EntityBase
    {
        public long OrderId { get; set; }

        public long ProductId { get; set; }

        public int Units { get; set; }

        public decimal UnitPrice { get; set; }

    }
}
