using System;
using System.Collections.Generic;
using System.Text;

namespace RepoSample.Entities
{
    public class EntityBase
    {
        public DateTime CreatedDateUTC { get; set; }

        public string CreatedBy { get; set; }

        public DateTime ModifiedDateUTC { get; set; }

        public string ModifiedBy { get; set; }
    }
}
