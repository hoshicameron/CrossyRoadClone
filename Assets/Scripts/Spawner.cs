using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform startPosition = null;

    [SerializeField] private float delayMin = 1.5f;
    [SerializeField] private float delayMax = 5f;
    [SerializeField] private float speedMin = 1.0f;
    [SerializeField] private float speedMax = 4f;

    [SerializeField] private bool useSpawnPlacement = false;
    [SerializeField] private int spawnCountMin = 4;
    [SerializeField] private int spawnCountMax = 20;

    private float delayTime = 0;
    private float speed = 0;

    [HideInInspector] public GameObject item = null;
    [HideInInspector] public bool goLeft = false;
    [HideInInspector] public float spawnLeftPosition=0;
    [HideInInspector] public float spawnRightPosition=0;

    private void OnEnable()
    {
        if (useSpawnPlacement)
        {
            int spawnCount = Random.Range(spawnCountMin,spawnCountMax);

            for (int i = 0; i < spawnCount; i++)
            {
                SpawnItem();
            }
        } else
        {
            speed = Random.Range(speedMin, speedMax);

            StartCoroutine(SpawnCoroutine());
        }

    }

    private void OnDisable()
    {
        StopCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            // Spawn Mover
            SpawnItem();

            // Create random delay
            delayTime = Random.Range(delayMin, delayMax);

            // Wait for delay time ends
            yield return new WaitForSeconds(delayTime);
        }
    }

    private void SpawnItem()
    {
        GameObject newGameObject=Instantiate(item) as GameObject;

        newGameObject.transform.position = GetSpawnPosition();

        float direction = 0f;
        if (goLeft) direction = 180f;
        if (!useSpawnPlacement)
        {
            newGameObject.GetComponent<Mover>().Speed = speed;
            newGameObject.transform.rotation = newGameObject.transform.rotation * Quaternion.Euler(0, direction, 0);
        }
    }

    private Vector3 GetSpawnPosition()
    {
        if (useSpawnPlacement)
        {
            Vector3 position= new Vector3((int)Random.Range(spawnLeftPosition,spawnCountMax),startPosition.position.y,
                startPosition.position.z);

            return position;
        } else
        {
            return startPosition.position;
        }
    }
}
