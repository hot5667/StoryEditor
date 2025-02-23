using System.Collections.Generic;
using Editor;
using UnityEngine;
using UnityEditor;

public class LeftPanel : BasePanel<List<Node>>
{
    private Vector2 scrollPosition;
    private Node selectedNode;
    public Node SelectedNode => selectedNode;
    public LeftPanel(Rect rect, Node selectedNode) : base(rect)
    {
        this.selectedNode = selectedNode;
    }
    
    public override void Draw()
    {
        // BasePanel의 Rect 프로퍼티를 사용해 UI 렌더링
        GUILayout.BeginArea(Rect, GUI.skin.box);
        GUILayout.Label("Nodes", EditorStyles.boldLabel, GUILayout.Height(20));

        // Scroll View 생성
        scrollPosition = GUILayout.BeginScrollView(scrollPosition);
        foreach (var node in data)
        {
            var nodeContent = $"ID: {node.Id} - {node.Content.Title}";
            if (GUILayout.Button(nodeContent, EditorStyles.miniButton))
            {
                selectedNode = node;
            }
        }
        GUILayout.EndScrollView();

        GUILayout.EndArea();
    }
}