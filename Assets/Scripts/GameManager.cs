using System;
using UnityEngine;

// 칸의 상태는 3가지이므로 열거형으로 나타낸다.
public enum SquareState
{
    None,
    Cross,
    Circle
}

/// <summary>
/// 틱택토 게임을 진행한다. => 비즈니스 로직 => 핵심 모듈
/// - 애플리케이션을 여러 계층(수준)으로 나눈다.
/// ㄴ 입출력과 가까울수록 저수준, 입출력과 멀어질수록 고수준
/// ㄴ 저수준이 고수준에 의존하게 만들어야 한다.
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private SquareState[,] _board = new SquareState[3, 3];

    // 현재 턴
    // ㄴ 입력에 대해 이 데이터를 가지고 출력을 처리해야 한다.
    private SquareState _currentTurnState = SquareState.Cross;

    // 출력을 위해서는 보드의 상태(칸의 좌표, 칸의 상태)가 변경됐다는 것을 알려야 한다.
    public event Action<int, int, SquareState> OnBoardChanged;

    
    public void PlayMarker(int x, int y)
    {
        if (_board[y, x] != SquareState.None)
        {
            return;
        }

        _board[y, x] = _currentTurnState;

        Logger.Info("보드의 상태");
        for (int line = 0; line < 3; ++line)
        {
            Logger.Info($"{_board[line, 0]}, {_board[line, 1]}, {_board[line, 2]}");
        }

        // 구독한 객체에게 보드의 상태가 바뀌었다는 것을 통지한다.
        OnBoardChanged?.Invoke(x, y, _currentTurnState);

        if (_currentTurnState == SquareState.Cross)
        {
            _currentTurnState = SquareState.Circle;
        }
        else if (_currentTurnState == SquareState.Circle)
        {
            _currentTurnState = SquareState.Cross;
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
