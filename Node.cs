using Editor.Protocol;
using UnityEngine;

namespace Editor
{
    public class Node : ValidatableObject, INode
    {
        private INode _nodeImplementation;
        private INode _nodeImplementation1;
        public int Id { get; private set; }
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public INodeContent Content { get; set; }
        public void Draw(Event current, ref Node selectedNode)
        {
            _nodeImplementation1.Draw(current, ref selectedNode);
        }

        private INodeRenderer Renderer { get; set; }

        public Node(
            int id, 
            Vector2 position, 
            Vector2 size, 
            INodeContent content, 
            INodeRenderer renderer)
        {
            Id = id;
            Position = position;
            Size = size;

            Content = content;
            Renderer = renderer;

            Validate();
        }

        // public void Draw(Event currentEvent, ref Node selectedNode)
        // {
        //     Renderer.DrawNode(this, currentEvent, ref selectedNode);
        // }

        public sealed override void Validate()
        {
            CheckNull(Content, nameof(Content));
            CheckNull(Renderer, nameof(Renderer));
        }
    }
}