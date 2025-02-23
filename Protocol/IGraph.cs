using System.Collections.Generic;

namespace Editor.Protocol
{
    public interface IGraph
    {
        List<INode> Nodes { get; }
        List<IEdge> Edges { get; }

        void AddNode(INode node);
        void RemoveNode(INode node);
        void AddEdge(INode startNode, INode endNode);
        void RemoveEdge(IEdge edge);
    }
}