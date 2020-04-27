using System;
using System.Collections.Generic;
using System.Text;

namespace Passenger.Core.Domain
{
    public class Driver
    {
        public Guid Id { get; protected set; }
        public Guid UserId { get; protected set; }
        public Vehicle Vehicle { get; protected set; }
        public IEnumerable<Route> Routes { get; protected set; }
        public IEnumerable<DailyRoute> DailyRoutes { get; protected set; }

        protected Driver() 
        { }

        public Driver(Guid id, Guid userId, Vehicle vehicle, IEnumerable<Route> routes, IEnumerable<DailyRoute> dailyRoutes)
        {
            Id = id;
            UserId = userId;
            Vehicle = vehicle;
            Routes = routes;
            DailyRoutes = dailyRoutes;
        }
    }
}
