using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    [SerializeField] private GameObject camera = null;

    public bool CanPlay { get; private set; } = false;

    private int currentCoins = 0;
    private int currentDistance = 0;

    private void Start()
    {
        // Todo Level generator startup
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

        // Todo generate new level piece here
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
        print("Start Play");
        CanPlay = true;
    }

}
