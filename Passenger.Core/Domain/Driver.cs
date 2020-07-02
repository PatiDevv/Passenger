using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Passenger.Core.Domain
{
    public class Driver
    {
        private ISet<Route> _routes = new HashSet<Route>();
        private ISet<DailyRoute> _dailyRoutes = new HashSet<DailyRoute>();

        public Guid UserId { get; protected set; }
        public string Name { get; protected set; }
        public Vehicle Vehicle { get; protected set; }
        public IEnumerable<Route> Routes
        {
            get { return _routes; }
            set { _routes = new HashSet<Route>(value); }
        }
        public IEnumerable<DailyRoute> DailyRoutes
        {
            get { return _dailyRoutes; }
            set { _dailyRoutes = new HashSet<DailyRoute>(value); }
        }
        public DateTime UpdateAt { get; protected set; }

        protected Driver() 
        { }

        public Driver(User user)
        {
            UserId = user.Id;
            Name = user.UserName;
        }

        public Driver(Guid userId, Vehicle vehicle, IEnumerable<Route> routes, IEnumerable<DailyRoute> dailyRoutes)
        {
            UserId = userId;
            Vehicle = vehicle;
            Routes = routes;
            DailyRoutes = dailyRoutes;
        }

        public void SetVehicle(Vehicle vehicle)
        {
            Vehicle = vehicle;
            UpdateAt = DateTime.UtcNow;
        }

        public void AddRoute(string name, Node start, Node end)
        {
            var route = Routes.SingleOrDefault(x => x.Name == name);
            if(route != null)
            {
                throw new Exception($"Route with '{name}' already exissts for driver: {Name}.");
            }
            _routes.Add(Route.Create(name, start, end));
            UpdateAt = DateTime.UtcNow;
        }

        public void DeleteRoute(string name)
        {
            var route = Routes.SingleOrDefault(x => x.Name == name);
            if (route == null)
            {
                throw new Exception($"Route with '{name}' for driver: {Name} was not found.");
            }
            _routes.Remove(route);
            UpdateAt = DateTime.UtcNow;
        }
    }
}
