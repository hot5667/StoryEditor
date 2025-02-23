using Editor.Protocol;
using UnityEngine;

namespace Editor
{
    public class NodeContent : INodeContent
    {
        public string Title { get; set; } //제목
        public string Description { get; set; } //설명
        public Color NodeColor { get; set; } //중요도 //3개 ~ 5개 / 빨강 - 매우 중요 / 주황 - 중요 / 노랑 - 주의 / 연두 - 고려  / 회색 - 일반 
        public int Value { get; set; }  //역활 분류 굳이? StoryIndex 순서도? - > 01 02 03 04 05
    
        public NodeContent(
            string title = "Untitled", 
            string description = "", 
            Color? color = null, 
            int value = 0)
        {
            Title = title;
            Description = description;
            NodeColor = color ?? Color.gray; // 기본 색상은 회색.
            Value = value;
        }
    }
}
