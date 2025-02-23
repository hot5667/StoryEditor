using Editor.Protocol;
using UnityEngine;

namespace Editor
{
    public class NodeRenderer : INodeRenderer
    {
        public void DrawNode(INode node, Event currentEvent, ref Node selectedNode)
        {
            // node의 필수 데이터 가져오기
            var title = node.Content.Title;
            var color = node.Content.NodeColor;

            // 기본 노드 스타일 정의
            var nodeStyle = new GUIStyle(GUI.skin.box)
            {
                normal = { background = MakeTex(1, 1, color) }
            };

            // 노드 위치와 크기 계산
            var nodeRect = new Rect(node.Position.x, node.Position.y, node.Size.x, node.Size.y);
            GUI.Box(nodeRect, title, nodeStyle);

            // 클릭 이벤트 처리
            if (currentEvent.type != EventType.MouseDown || !nodeRect.Contains(currentEvent.mousePosition)) return;
            Debug.Log($"Node '{title}' clicked!");
            selectedNode = node as Node;
        }

        private static Texture2D MakeTex(int width, int height, Color col)
        {
            var pix = new Color[width * height];
            for (var i = 0; i < pix.Length; ++i)
            {
                pix[i] = col;
            }

            var result = new Texture2D(width, height);
            result.SetPixels(pix);
            result.Apply();
            return result;
        }
    }
}