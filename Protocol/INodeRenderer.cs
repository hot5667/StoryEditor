using UnityEngine;

namespace Editor.Protocol
{
    public interface INodeRenderer
    {
        void DrawNode(INode node, Event currentEvent, ref Node selectedNode);
    }
}