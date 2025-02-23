using Editor;
using UnityEngine;
using UnityEditor;

public class RightPanel : BasePanel<Node>
{
    private int activeTab;

    public RightPanel(Rect rect) : base(rect) { }

    public override void Draw()
    {
        // BasePanel의 Rect 프로퍼티를 사용해 UI 구성을 진행
        GUILayout.BeginArea(Rect, GUI.skin.box);

        // Toolbar 스타일의 탭 메뉴
        GUILayout.BeginHorizontal(EditorStyles.toolbar);
        if (GUILayout.Toggle(activeTab == 0, "Node Editor", EditorStyles.toolbarButton)) activeTab = 0;
        if (GUILayout.Toggle(activeTab == 1, "Node List", EditorStyles.toolbarButton)) activeTab = 1;
        GUILayout.EndHorizontal();

        // 현재 활성화된 탭 내용 표시
        if (activeTab == 0)
        {
            if (data != null)
            {
                DrawNodeEditor(data);
            }
            else
            {
                GUILayout.Label("No Node Selected. Please select a node from the left panel.", EditorStyles.helpBox);
            }
        }
        else if (activeTab == 1)
        {
            DrawNodeListSummary();
        }

        GUILayout.EndArea();
    }

    private void DrawNodeEditor(Node node)
    {
        GUILayout.Label($"Node ID: {node.Id}");
    }

    private void DrawNodeListSummary()
    {
        GUILayout.Label("Summary of All Nodes");
    }
}