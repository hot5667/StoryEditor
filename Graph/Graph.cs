using System.Collections.Generic;
using Editor.Protocol;

namespace Editor.Graph
{
    public class Graph : IGraph
    {
        public List<INode> Nodes { get; private set; } = new List<INode>();
        public List<IEdge> Edges { get; private set; } = new List<IEdge>();

        public void AddNode(INode node)
        {
            Nodes.Add(node);
        }

        public void RemoveNode(INode node)
        {
            Nodes.Remove(node);
            Edges.RemoveAll(edge => edge.StartNode == node || edge.EndNode == node);
        }

        public void AddEdge(INode startNode, INode endNode)
        {
            Edges.Add(new Edge(startNode, endNode));
        }

        public void RemoveEdge(IEdge edge)
        {
            Edges.Remove(edge);
        }
    }

}