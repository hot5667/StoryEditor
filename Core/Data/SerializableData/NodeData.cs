using Editor.Protocol;
using UnityEngine;
using System;

namespace Editor.Core.Data.SerializableData
{
    [Serializable]
    public class NodeData
    {
        public int Id { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public INodeContent Content { get; set; }

        public NodeData()
        {
            Id = 0;
            Position = Vector2.zero;
            Size = Vector2.zero;
            Content = null;
        }
    
        public NodeData(int id, Vector2 position, Vector2 size, INodeContent content)
        {
            this.Id = id;
            this.Position = position;
            this.Size = size;
            this.Content = content;
        }
    }
}