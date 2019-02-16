using System;
using System.Collections.Generic;
using Cet.Core.Entities;

namespace Cet.Entities.Concrete
{
    public partial class Administrator : IRegistrable
    {
        public int Id { get; set; }

        public virtual User User { get; set; }
    }
}