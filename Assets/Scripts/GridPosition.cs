using UnityEngine;

// 기능 : 마우스 버튼을 Unity 애플리케이션에 전달한다.
public class GridPosition : MonoBehaviour
{

    private void OnMouseDown()
    {
        Debug.Log($"Clicked!");
    }

}
