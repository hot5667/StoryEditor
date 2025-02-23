using UnityEngine;

namespace Editor.Protocol
{
    public interface INodeContent
    {
        string Title { get; set; }
        string Description { get; set; }
        Color NodeColor { get; set; }
        int Value { get; set; }
    }
}