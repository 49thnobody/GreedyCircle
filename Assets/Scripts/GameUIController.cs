using TMPro;
using UnityEngine;

public class GameUIController : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private Circle _circle;

    [SerializeField] private GameObject _scorePanel;
    [SerializeField] private GameObject _loadingPanel;
    [SerializeField] private TextMeshProUGUI _scoreText;
    private int _score;
    private int _targetedScore;

    public delegate void Win();
    public event Win OnWin;

    public void Show(int difficulty)
    {
        _score = 0;
        _targetedScore = difficulty;
        _scorePanel.SetActive(true);
        _loadingPanel.SetActive(true);
        UpdateUI();
    }

    public void LoadingDone()
    {
        _loadingPanel.SetActive(false);
    }

    private void Start()
    {
        _circle.OnCoinCollected += Circle_OnCoinCollected;
        _gameManager.OnGameStart += Show;
    }

    private void Circle_OnCoinCollected(MyGameObject coin)
    {
        _score++;
        UpdateUI();
        if (_score == _targetedScore)
        {
            OnWin?.Invoke();
            _scorePanel.SetActive(false);
        }
    }

    private void UpdateUI()
    {
        _scoreText.text = _score.ToString();
    }
}
