using Editor.Protocol;
using UnityEngine;

namespace Editor.Graph
{
    public class GraphEditor : IGraphEditor
    {
        public IGraph Graph { get; private set; } = new Graph();
        public Node SelectedNode { get; set; }

        public void HandleEvent(Event currentEvent)
        {
            // 클릭해서 노드 선택
            if (currentEvent.type == EventType.MouseDown)
            {
                foreach (var node in Graph.Nodes)
                {
                    if (Vector2.Distance(node.Position, currentEvent.mousePosition) < 20f)
                    {
                        SelectedNode = (Node)node;
                        break;
                    }
                }
            }
        }

        public void Draw()
        {
            // 노드 그리기
            foreach (var node in Graph.Nodes)
            {
                var selectedNode = SelectedNode;
                node.Draw(Event.current, ref selectedNode);
            }

            // 엣지 그리기
            foreach (var edge in Graph.Edges)
            {
                edge.Draw();
            }
        }
    }

}