using TMPro;
using UnityEngine;

public class GameOverController : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private GameObject _panel;
    [SerializeField] private TextMeshProUGUI _resultText;

    public void Lose()
    {
        _panel.SetActive(true);
        _resultText.text = "Вы проиграли!";
    }
    public void Win()
    {
        _panel.SetActive(true);
        _resultText.text = "Вы выиграли!";
    }

    public void Restart()
    {
        _gameManager.QuickRestart();
        _panel.SetActive(false);
    }

    public void ToMenu()
    {
        _gameManager.ToMenu();
        _panel.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }
}