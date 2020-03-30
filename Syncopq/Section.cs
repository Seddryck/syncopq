using System;
using System.Collections.Generic;
using System.Text;

namespace Syncopq
{
    public class Section
    {
        public string Name { get; }
        public IEnumerable<Query> Queries { get; }

        public Section(string name, IEnumerable<Query> queries)
            => (Name, Queries) = (name, queries);
    }
}
