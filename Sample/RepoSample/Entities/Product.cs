using System;
using System.Collections.Generic;
using System.Text;

namespace RepoSample.Entities
{
    public class Product : EntityBase
    {
        public long ProductId { get; set; }

        public string ProductCode { get; set; }

        public string Name { get; set; }

        public decimal UnitPrice { get; set; }

    }
}
