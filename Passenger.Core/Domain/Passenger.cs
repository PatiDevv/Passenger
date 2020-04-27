using System;
using System.Collections.Generic;
using System.Text;

namespace Passenger.Core.Domain
{
    public class Passenger
    {
        public Guid Id { get; protected set; }

        public Guid UserId { get; protected set; }

        public Node Adress { get; protected set; }

    }
}
