using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Circle _circle;
    [SerializeField] private ScoreController _scoreController;
    [SerializeField] private MainMenuController _mainMenuController;
    [SerializeField] private GameOverController _gameOverController;

    public delegate void GameStart(int difficulty);
    public event GameStart OnGameStart;

    private void Start()
    {
        _circle.OnThornCollapsed += Circle_OnThornCollapsed;
        _scoreController.OnWin += ScoreController_OnWin;
    }

    private int _rememberDifficulty;
    public void StartGame(int difficulty)
    {
        _rememberDifficulty = difficulty;
        OnGameStart?.Invoke(difficulty);
    }

    public void QuickRestart()
    {
        OnGameStart?.Invoke(_rememberDifficulty);
    }

    public void ToMenu()
    {
        _mainMenuController.Show();
    }

    private void ScoreController_OnWin()
    {
        _gameOverController.Win();
    }

    private void Circle_OnThornCollapsed()
    {
        _gameOverController.Lose();
    }
}
