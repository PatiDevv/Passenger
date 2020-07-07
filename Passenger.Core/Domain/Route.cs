namespace Passenger.Core.Domain
{
    public class Route
    {
        public string Name { get; protected set; }                                                                                                 
        public Node StartNode { get; protected set; }
        public Node EndNode { get; protected set; }

        public double Distance { get; protected set; }

        protected Route()
        {
        }

        protected Route(string name, Node startNode, Node endNode, double distance)
        {
            Name = name;
            StartNode = startNode;
            EndNode = endNode;
            Distance = distance;
        }

        public static Route Create(string name, Node start, Node end, double distance)
            => new Route(name, start, end, distance);
    }
}