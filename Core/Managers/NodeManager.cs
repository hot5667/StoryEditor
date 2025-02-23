using System;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Collections.Generic;
using System.Linq;
using Editor.Core.Data.SerializableData;
using Editor.Protocol;
using UnityEngine.Serialization;

namespace Editor.Core.Managers
{
    [Serializable]
    public class NodeCollection
    {
        [FormerlySerializedAs("Nodes")] public List<NodeData> nodes = new List<NodeData>(); // 명시적으로 초기화
    }

    public class NodeManager : INodeManager
    {
        public List<Node> Nodes { get; private set; } = new List<Node>();
        private int _nextNodeId;                 
        private Node _selectedNode;

        private INodeRenderer _nodeRenderer; 

        public event Action<Node> OnNodeSelected;
        public event Action<Node> OnNodeAdded;
        public event Action<Node> OnNodeRemoved;
        public event Action OnNodesChanged;

        /// <summary>
        /// NodeManager를 초기화합니다.
        /// </summary>
        public void Initialize()
        {
            _nodeRenderer = new NodeRenderer(); // 기본 렌더러를 초기화
        }

        /// <summary>
        /// 기본 크기의 노드를 생성합니다.
        /// </summary>
        public Node CreateNode(Vector2 position, INodeContent content, INodeRenderer renderer)
        {
            var newNode = new Node(_nextNodeId++, position, new Vector2(100, 100), content, renderer);
            Nodes.Add(newNode);
            OnNodeAdded?.Invoke(newNode);
            OnNodesChanged?.Invoke();
            return newNode;
        }

        /// <summary>
        /// 사용자 지정 크기의 노드를 생성합니다.
        /// </summary>
        public Node CreateNodeWithSize(Vector2 position, INodeContent content, INodeRenderer renderer, Vector2 size)
        {
            var newNode = new Node(_nextNodeId++, position, size, content, renderer);
            Nodes.Add(newNode);
            OnNodeAdded?.Invoke(newNode);
            OnNodesChanged?.Invoke();
            return newNode;
        }

        // 내부 메서드: 노드 생성 공통 로직
        private void CreateNodeInternal(int id, Vector2 position, Vector2 size, INodeContent content,
            INodeRenderer renderer)
        {
            var node = new Node(id, position, size, content, renderer);
            Nodes.Add(node);
            OnNodeAdded?.Invoke(node);
            OnNodesChanged?.Invoke();
        }

        /// <summary>
        /// 노드를 추가합니다.
        /// </summary>
        public void AddNode(Vector2 position)
        {
            var content = new NodeContent(
                $"Node {_nextNodeId}",
                "This is a test node.",
                Random.ColorHSV(),
                Random.Range(0, 100)
            );

            // 자동으로 ID 할당 및 기본 값으로 노드 생성
            CreateNodeInternal(_nextNodeId++, position, new Vector2(150, 100), content, _nodeRenderer);
        }

        /// <summary>
        /// 노드를 선택합니다.
        /// </summary>
        public void SelectNode(Node node)
        {
            _selectedNode = node;
            OnNodeSelected?.Invoke(node);
        }

        /// <summary>
        /// 노드를 제거합니다.
        /// </summary>
        public void RemoveNode(Node node)
        {
            if (node == null) return; // null 보호

            Nodes.Remove(node);
            OnNodeRemoved?.Invoke(node);
            OnNodesChanged?.Invoke();
        }

        /// <summary>
        /// ID로 노드를 검색합니다.
        /// </summary>
        public Node GetNodeById(int id)
        {
            // LINQ를 사용하여 노드 검색
            return Nodes.FirstOrDefault(n => n.Id == id);
        }

        /// <summary>
        /// 노드 데이터를 JSON 파일로 저장합니다.
        /// </summary>
        public void SaveNodes(string path)
        {
            string json = SerializeNodes();
            System.IO.File.WriteAllText(path, json);

            Debug.Log($"Nodes saved to {path}");
        }

        /// <summary>
        /// JSON 파일로부터 노드 데이터를 로드합니다.
        /// </summary>
        public void LoadNodes(string path, INodeRenderer renderer)
        {
            if (!System.IO.File.Exists(path))
            {
                Debug.LogWarning($"File at {path} does not exist.");
                return;
            }

            string json = System.IO.File.ReadAllText(path);
            try
            {
                DeserializeNodes(json);
                Debug.Log($"Nodes loaded from {path}");
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to load nodes: {ex.Message}");
            }
        }
        

        /// <summary>
        /// 모든 노드를 제거합니다.
        /// </summary>
        public void Clear()
        {
            Nodes.Clear();
            _selectedNode = null;
            OnNodesChanged?.Invoke();
        }

        // 직렬화를 통해 데이터 저장
        private string SerializeNodes()
        {
            var nodeData = Nodes.Select(n => new NodeData
            {
                Id = n.Id,
                Position = n.Position,
                Size = n.Size,
                Content = n.Content
            }).ToList();

            return JsonUtility.ToJson(new NodeCollection { nodes = nodeData });
        }

        // 역직렬화를 통해 데이터 로드
        private void DeserializeNodes(string json)
        {
            var collection = JsonUtility.FromJson<NodeCollection>(json);
            if (collection?.nodes == null)
                throw new Exception("Failed to parse node data");

            Clear();

            foreach (var node in collection.nodes.Select(data => new Node(data.Id, data.Position, data.Size, data.Content, _nodeRenderer)))
            {
                Nodes.Add(node);
            }

            _nextNodeId = Nodes.Max(n => n.Id) + 1;
            OnNodesChanged?.Invoke();
        }
    }
}