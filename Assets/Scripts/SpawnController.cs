using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private bool goLeft = false;
    [SerializeField] private bool goRight = false;
    [SerializeField] private List<GameObject> items=new List<GameObject>();
    [SerializeField] private List<Spawner> SpawnersLeft=new List<Spawner>();
    [SerializeField] private List<Spawner> SpawnersRight=new List<Spawner>();

    private void Start()
    {
        GameObject item = items[Random.Range(0, items.Count)];

        int direction = Random.Range(0, 2);
        if (direction > 0)
        {
            goLeft = false;
            goRight = true;
        } else
        {
            goLeft = true;
            goRight = false;
        }

        for (int i = 0; i < SpawnersLeft.Count; i++)
        {
            SpawnersLeft[i].item = item;
            SpawnersLeft[i].goLeft = goLeft;
            SpawnersLeft[i].gameObject.SetActive(goRight);
            SpawnersLeft[i].spawnLeftPosition = SpawnersLeft[i].transform.position.x;
        }

        for (int i = 0; i < SpawnersRight.Count; i++)
        {
            SpawnersRight[i].item = item;
            SpawnersRight[i].goLeft = goLeft;
            SpawnersRight[i].gameObject.SetActive(goLeft);
            SpawnersRight[i].spawnRightPosition = SpawnersRight[i].transform.position.x;
        }
    }
}
