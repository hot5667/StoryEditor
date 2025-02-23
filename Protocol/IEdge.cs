namespace Editor.Protocol
{
    public interface IEdge
    {
        INode StartNode { get; }
        INode EndNode { get; }
        void Draw();
    }
}