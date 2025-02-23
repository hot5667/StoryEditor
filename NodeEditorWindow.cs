using System.Linq;
using UnityEditor;
using UnityEngine;
using Editor.Core.Managers;

namespace Editor
{
    public class NodeEditorWindow : EditorWindow
    {
        private readonly NodeManager _nodeManager = new NodeManager(); // NodeManager를 사용한 관리
        private Node _selectedNode; // 현재 선택된 노드 추적

        private Vector2 _leftPanelScroll; // 스크롤뷰 위치 저장
        private int _activeTab; // 현재 활성화된 탭 인덱스

        // 테스트
        private string _storyText = "";

        [MenuItem("Window/Node Editor")]
        public static void OpenWindow()
        {
            GetWindow<NodeEditorWindow>("Node Editor");
        }

        private void OnEnable()
        {
            _nodeManager.Initialize(); // 노드 매니저 초기화
            _nodeManager.OnNodeAdded += OnNodeChanged; // 노드 변경 이벤트 등록
            _nodeManager.OnNodeRemoved += OnNodeChanged;
            _nodeManager.OnNodesChanged += Repaint; // 노드가 변경되면 창 다시 그리기
        }

        private void OnDisable()
        {
            _nodeManager.OnNodeAdded -= OnNodeChanged; // 이벤트 등록 제거
            _nodeManager.OnNodeRemoved -= OnNodeChanged;
            _nodeManager.OnNodesChanged -= Repaint;
        }

        private void OnGUI()
        {
            DrawToolbar();

            GUILayout.BeginHorizontal();

            // 왼쪽: 노드 목록
            DrawLeftPanel();

            // 오른쪽: 선택된 탭 UI
            DrawRightPanel();

            GUILayout.EndHorizontal();

            _storyText = EditorGUILayout.TextField("스토리 내용", _storyText, GUILayout.Height(70));
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private void DrawToolbar()
        {
            GUILayout.BeginHorizontal(EditorStyles.toolbar);
            GUILayout.Label("Node Editor", EditorStyles.boldLabel);
            GUILayout.FlexibleSpace();

            if (GUILayout.Button("Add Node", EditorStyles.toolbarButton))
            {
                _nodeManager.AddNode(new Vector2(100, 100));
            }

            if (_selectedNode != null && GUILayout.Button("Remove Node", EditorStyles.toolbarButton))
            {
                var confirm = EditorUtility.DisplayDialog("Remove Node", "Are you sure you want to remove this node?",
                    "Yes", "No");
                if (confirm)
                {
                    _nodeManager.RemoveNode(_selectedNode);
                    _selectedNode = null;
                }
                else
                {
                    Debug.Log("Node removal cancelled.");
                }
            }

            GUILayout.EndHorizontal();
        }

        private void DrawLeftPanel()
        {
            GUILayout.BeginVertical("box", GUILayout.Width(position.width * 0.3f), GUILayout.ExpandHeight(true));

            GUILayout.Label("Nodes", EditorStyles.boldLabel, GUILayout.Height(20));

            // 스크롤뷰로 노드 목록 표시
            _leftPanelScroll = GUILayout.BeginScrollView(_leftPanelScroll);
            if (_nodeManager.Nodes != null)
                foreach (var node in from node in _nodeManager.Nodes let nodeLabel = $"ID: {node.Id} - {node.Content.Title}" where GUILayout.Button(nodeLabel, EditorStyles.miniButton) select node)
                {
                    _selectedNode = node;
                    _nodeManager.SelectNode(node); // 노드 선택 처리
                }

            GUILayout.EndScrollView();

            GUILayout.EndVertical();
        }

        private void DrawRightPanel()
        {
            GUILayout.BeginVertical("box", GUILayout.Width(position.width * 0.7f), GUILayout.ExpandHeight(true));

            // 탭 그리기
            DrawTabs();

            switch (_activeTab)
            {
                // 현재 선택된 탭에 따른 내용 표시
                case 0:
                    DrawNodeEditorPanel();
                    break;
                case 1:
                    DrawNodeSummaryPanel();
                    break;
                case 2:
                    DrawGraphEditorPanel();
                    break;
                    
            }
            
            GUILayout.EndVertical();
        }

      

        private void DrawTabs()
        {
            GUILayout.BeginHorizontal(EditorStyles.toolbar);
            if (GUILayout.Toggle(_activeTab == 0, "Node Editor", EditorStyles.toolbarButton))
            {
                _activeTab = 0;
            }

            if (GUILayout.Toggle(_activeTab == 1, "Node Summary", EditorStyles.toolbarButton))
            {
                _activeTab = 1;
            }

            if (GUILayout.Toggle(_activeTab == 2, "Graph Node", EditorStyles.toolbarButton))
            {
                _activeTab = 2;
            }

            GUILayout.EndHorizontal();
        }
        private void DrawGraphEditorPanel()
        {
            GUILayout.Label("Graph Editor", EditorStyles.boldLabel);

            if (_nodeManager.Nodes != null)
            {
                GUILayout.Label("There are no Node, Please Add a Node", EditorStyles.helpBox);
            }
            
            var currentEvent = Event.current;

            if (_nodeManager.Nodes == null) return;
            foreach (var node in _nodeManager.Nodes)
            {
                node.Draw(currentEvent, ref _selectedNode);
            }
        }
         
        private void DrawNodeEditorPanel()
        {
            if (_selectedNode == null)
            {
                GUILayout.Label("No Node Selected. Please select a node from the left panel.", EditorStyles.helpBox);
                return;
            }

            // 선택된 노드 편집 UI
            GUILayout.Label($"Editing Node {_selectedNode.Id}", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            EditorGUI.BeginChangeCheck(); // 변경 감지 시작
            _selectedNode.Content.Title = EditorGUILayout.TextField("Title", _selectedNode.Content.Title);
            _selectedNode.Content.Description =
                EditorGUILayout.TextField("Description", _selectedNode.Content.Description);
            _selectedNode.Content.NodeColor = EditorGUILayout.ColorField("Node Color", _selectedNode.Content.NodeColor);
            _selectedNode.Content.Value = EditorGUILayout.IntField("Value", _selectedNode.Content.Value);
            // if (EditorGUI.EndChangeCheck())
            // {
            //     nodeManager.NotifyNodeChanged();
            // }

            // 버튼으로 노드 선택 해제
            GUILayout.Space(10);
            if (GUILayout.Button("Deselect Node", GUILayout.Height(30)))
            {
                _selectedNode = null;
            }
        }

        private void DrawNodeSummaryPanel()
        {
            GUILayout.Label("Node Summary", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            foreach (var node in _nodeManager.Nodes)
            {
                EditorGUILayout.LabelField(
                    $"ID: {node.Id} | Title: {node.Content.Title} | Value: {node.Content.Value}");
            }
        }

        private static void OnNodeChanged(Node node)
        {
            // 노드 변경에 따른 후처리 (예: 디버그 로그 출력)
            Debug.Log($"Node {node.Id} updated.");
        }

        // 테스트 - 노드 내용 스토리 입력 - content 로 뺄지 따로 내용이라는 클래스로 뺄지 고민...
        // 기능적 으로 필요한 부분 내용 저장 및 수정 + load 기능 우선 순위로 분별....
        // 작성, 
        public void SaveStory()
        {
            if (!string.IsNullOrEmpty(_storyText)) return;
            
            EditorUtility.DisplayDialog("저장 실패", "스토리를 입력하세요!", "확인");
        }
    }
}