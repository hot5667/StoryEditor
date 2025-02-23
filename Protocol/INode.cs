using UnityEngine;

namespace Editor.Protocol
{
    public interface INode
    {
        int Id { get; }
        Vector2 Position { get; set; }
        Vector2 Size { get; set; }
        INodeContent Content { get; }

        void Draw(Event current, ref Node selectedNode);
    }
}