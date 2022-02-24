using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : SingletonMonoBehaviour<UIManager>
{
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI distanceText;
    [SerializeField] private GameObject gameOverUI;

    protected override void Awake()
    {
        base.Awake();

        gameOverUI.SetActive(false);
    }

    public void UpdateCoinText(string text)
    {
        coinText.SetText(text);
    }

    public void UpdateDistanceText(string text)
    {
        distanceText.SetText(text);
    }

    public void ShowGameOverUI()
    {
        gameOverUI.SetActive(true);
    }


    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        print("Quitting the game");
        Application.Quit();
    }
}
