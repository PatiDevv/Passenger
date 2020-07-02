namespace Passenger.Core.Domain
{
    public class Route
    {
        public string Name { get; protected set; }                                                                                                 
        public Node StartNode { get; protected set; }
        public Node EndNode { get; protected set; }

        protected Route()
        {
        }

        protected Route(string name, Node startNode, Node endNode)
        {
            Name = name;
            StartNode = startNode;
            EndNode = endNode;
        }

        public static Route Create(string name, Node start, Node end)
            => new Route(name, start, end);
    }
}