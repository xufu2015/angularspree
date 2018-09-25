using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Infrastructure
{
    public class SortTerm
    {
        public string Name { get; set; }

        public string EntityName { get; set; }

        public bool Descending { get; set; }

        public bool Default { get; set; }
    }
}
