using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private GameObject _selectDifficultyPanel;

    public void Show()
    {
        _panel.SetActive(true);
    }

    public void Play()
    {
        _selectDifficultyPanel.SetActive(true);
    }

    public void Play(int difficulty)
    {
        _gameManager.StartGame(difficulty);
        _selectDifficultyPanel.SetActive(false);
        _panel.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
