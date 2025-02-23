using UnityEngine;

namespace Editor.Protocol
{
    public interface IGraphEditor
    {
        IGraph Graph { get; }
        Node SelectedNode { get; set; }

        void HandleEvent(Event currentEvent);
        void Draw();
    }
}