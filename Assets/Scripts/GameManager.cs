using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    [SerializeField] private GameObject camera = null;
    [SerializeField] private int levelCount = 50;
    [SerializeField] private LevelGenerator levelGenerator;

    public bool CanPlay { get; private set; } = false;

    private int currentCoins = 0;
    private int currentDistance = 0;

    private void Start()
    {
        for (int i = 0; i < levelCount; i++)
        {
            levelGenerator.RandomGenerator();
        }
    }

    public void UpdateCoinCount(int value)
    {
        currentCoins += value;
        UIManager.Instance.UpdateCoinText(currentCoins.ToString());
    }

    public void UpdateDistanceCount()
    {
        currentDistance += 1;
        UIManager.Instance.UpdateDistanceText(currentDistance.ToString());

        levelGenerator.RandomGenerator();
    }

    public void GameOver()
    {
        //CanPlay= false;
        camera.GetComponent<CameraShake>().Shake();
        camera.GetComponent<CameraFollow>().enabled = false;
        UIManager.Instance.ShowGameOverUI();
    }

    public void StartPlay()
    {
        CanPlay = true;
    }

}
