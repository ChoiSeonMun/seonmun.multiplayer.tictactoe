using UnityEngine;

/// <summary>
/// 틱택토 게임을 진행한다. => 비즈니스 로직 => 핵심 모듈
/// - 애플리케이션을 여러 계층(수준)으로 나눈다.
/// ㄴ 입출력과 가까울수록 저수준, 입출력과 멀어질수록 고수준
/// ㄴ 저수준이 고수준에 의존하게 만들어야 한다.
/// </summary>

// 칸의 상태는 3가지이므로 열거형으로 나타낸다.
public enum SquareState
{
    None,
    Cross,
    Circle
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private SquareState[,] _board = new SquareState[3, 3];

    public void PlayMarker(int x, int y)
    {
        _board[y, x] = SquareState.Cross;

        Logger.Info("보드의 상태");
        for (int line = 0; line < 3; ++line)
        {
            Logger.Info($"{_board[line, 0]}, {_board[line, 1]}, {_board[line, 2]}");
        }
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
