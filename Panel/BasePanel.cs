using UnityEngine;

public abstract class BasePanel<T>
{
    private Rect rect; // 필드를 private으로 유지
    protected T data;  // 상속 클래스에서 접근 가능한 데이터 필드

    // rect Getter와 Setter 정의
    public Rect Rect 
    { 
        get => rect;
        set => rect = value; 
    }

    public BasePanel(Rect rect)
    {
        this.rect = rect;
    }

    public void SetData(T data)
    {
        this.data = data;
    }

    public abstract void Draw();
}