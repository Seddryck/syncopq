using System;
using System.Collections.Generic;
using System.Text;

namespace Syncopq
{
    public class Query
    {
        public string Name { get; }
        public string Code { get; }

        public Query(string name, string code)
            => (Name, Code) = (name, code);
    }
}
