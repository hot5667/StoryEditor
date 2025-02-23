using Editor.Protocol;
using System;
using UnityEngine;

namespace Editor.Core.Data.SerializableData
{
    [Serializable]
    public class NodeContentData : INodeContent
    {
        [SerializeField]
        private string title;
    
        [SerializeField]
        private string description;
    
        [SerializeField]
        private Color nodeColor;
    
        [SerializeField]
        private int value;
    
        public string Title
        {
            get => title; 
            set => title = value;
        }
        public string Description
        {
            get => description;
            set => description = value;
        }
    
        public Color NodeColor  
        {
            get => nodeColor;
            set => nodeColor = value;
        }
    
        public int Value 
        {
            get => value;
            set => this.value = value;
        }
    }
}