using Base.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Domain.Entities
{
    [BsonCollection("Person")]
    public class PersonEntity : Document
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
