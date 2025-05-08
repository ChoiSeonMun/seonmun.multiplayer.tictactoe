using UnityEngine;

/// <summary>
/// 틱택토 게임을 진행한다. => 비즈니스 로직 => 핵심 모듈
/// - 애플리케이션을 여러 계층(수준)으로 나눈다.
/// ㄴ 입출력과 가까울수록 저수준, 입출력과 멀어질수록 고수준
/// ㄴ 저수준이 고수준에 의존하게 만들어야 한다.
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public void ProcessInput(int x, int y)
    {
        // 처리
        Logger.Info("ProcessInput() Called");
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Destroy(Instance);
        }
    }
}
