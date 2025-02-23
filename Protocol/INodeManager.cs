using System;
using UnityEngine;

namespace Editor.Protocol
{
    public interface INodeManager
    {
        event Action<Node> OnNodeAdded;
        event Action<Node> OnNodeRemoved;
        event Action OnNodesChanged;
        Node CreateNode(Vector2 position, INodeContent content, INodeRenderer renderer);
        Node CreateNodeWithSize(Vector2 position, INodeContent content, INodeRenderer renderer, Vector2 size);
        void SelectNode(Node node);
        void RemoveNode(Node node);
        Node GetNodeById(int id);
        void SaveNodes(string path);
        void LoadNodes(string path, INodeRenderer renderer);
        void Clear();
    }
}