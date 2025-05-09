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
    private GameOverState _gameOverState = GameOverState.NotOver;

    // 출력을 위해서는 보드의 상태(칸의 좌표, 칸의 상태)가 변경됐다는 것을 알려야 한다.
    public event Action<int, int, SquareState> OnBoardChanged;

    
    public void PlayMarker(int x, int y)
    {
        if (_gameOverState != GameOverState.NotOver)
        {
            return;
        }

        if (_board[y, x] != SquareState.None)
        {
            return;
        }

        _board[y, x] = _currentTurnState;

        // 구독한 객체에게 보드의 상태가 바뀌었다는 것을 통지한다.
        OnBoardChanged?.Invoke(x, y, _currentTurnState);

        _gameOverState = TestGameOver();
        if (_gameOverState != GameOverState.NotOver)
        {
            Logger.Info($"{_gameOverState} is winner.");
            return;
        }
        
        if (_currentTurnState == SquareState.Cross)
        {
            _currentTurnState = SquareState.Circle;
        }
        else if (_currentTurnState == SquareState.Circle)
        {
            _currentTurnState = SquareState.Cross;
        }
    }

    // O가 승자, X가 승자, 무승부, 아직 게임이 끝나지 않음
    enum GameOverState
    {
        NotOver,
        Cross,
        Circle,
        Tie
    }

    GameOverState TestGameOver()
    {
        // 가로 검사
        for (int y = 0; y < 3; ++y)
        {
            if (_board[y, 0] != SquareState.None &&
            _board[y, 0] == _board[y, 1] && _board[y, 1] == _board[y, 2])
            {
                if (_board[y, 0] == SquareState.Cross)
                {
                    return GameOverState.Cross;
                }
                else if (_board[y, 0] == SquareState.Circle)
                {
                    return GameOverState.Circle;
                }
            }
        }

        // 세로 검사
        for (int x = 0; x < 3; ++x)
        {
            if (_board[0, x] != SquareState.None &&
            _board[0, x] == _board[1, x] && _board[1, x] == _board[2, x])
            {
                if (_board[0, x] == SquareState.Cross)
                {
                    return GameOverState.Cross;
                }
                else if (_board[0, x] == SquareState.Circle)
                {
                    return GameOverState.Circle;
                }
            }
        }

        // 대각선 검사
        if (_board[0, 0] != SquareState.None &&
            _board[0, 0] == _board[1, 1] && _board[1, 1] == _board[2, 2])
        {
            if (_board[0, 0] == SquareState.Cross)
            {
                return GameOverState.Cross;
            }
            else if (_board[0, 0] == SquareState.Circle)
            {
                return GameOverState.Circle;
            }
        }
        if (_board[2, 0] != SquareState.None &&
            _board[2, 0] == _board[1, 1] && _board[1, 1] == _board[0, 2])
        {
            if (_board[2, 0] == SquareState.Cross)
            {
                return GameOverState.Cross;
            }
            else if (_board[2, 0] == SquareState.Circle)
            {
                return GameOverState.Circle;
            }
        }

        // 무승부 : 보드의 모든 칸이 채워졌음에도 한 쪽이 승리가 나지 않은 경우
        // ㄴ 모든 칸이 None이 아닌 것으로 판별할 수 있다.
        for (int y = 0; y < 3; ++y)
        {
            for (int x = 0; x < 3; ++x)
            {
                if (_board[y, x] == SquareState.None)
                {
                    return GameOverState.NotOver;
                }
            }
        }

        return GameOverState.Tie;
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
