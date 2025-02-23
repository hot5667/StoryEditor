using Editor.Protocol;
using UnityEditor;

namespace Editor.Graph
{
    public class Edge : IEdge
    {
        public INode StartNode { get; private set; }
        public INode EndNode { get; private set; }

        public Edge(INode startNode, INode endNode)
        {
            StartNode = startNode;
            EndNode = endNode;
        }

        public void Draw()
        {
            Handles.DrawLine(StartNode.Position, EndNode.Position);
        }
    }
}