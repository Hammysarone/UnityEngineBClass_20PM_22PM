using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverUI;

    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void GameOver()
    {
        PopUpGameOverUI();
        PauseGame();
    }

    public void ContinueGame()
    {
        ReleasePauseGame();
        HideGameOverUI();
        Player.instance.RecoverHP();
    }

    private void PopUpGameOverUI()
    {
        _gameOverUI.SetActive(true);
    }

    private void HideGameOverUI()
    {
        _gameOverUI.SetActive(false);
    }

    private void PauseGame()
    {
        Time.timeScale = 0.0f;
    }

    private void ReleasePauseGame()
    {
        Time.timeScale = 1.0f;
    }
}
