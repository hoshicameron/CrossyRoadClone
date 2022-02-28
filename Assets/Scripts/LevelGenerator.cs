using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private List<GameObject> platform=new List<GameObject>();
    [SerializeField] private List<float> height = new List<float>();

    private int rndRange = 0;
    private float lastPosition=0;
    private float lastScale = 0;

    public void RandomGenerator()
    {
        rndRange = Random.Range(0, platform.Count);
        for (int i = 0; i < platform.Count; i++)
        {

            CreateLevelObject(platform[i],height[i],i);
        }
    }

    public void CreateLevelObject(GameObject newGameObject, float height, int value)
    {
        if (rndRange == value)
        {
            GameObject go = Instantiate(newGameObject) as GameObject;

            float offset = lastPosition + (lastScale * 0.5f);
            offset += go.transform.localScale.z * 0.5f;
            Vector3 pos=new Vector3(0,height,offset);

            go.transform.position = pos;

            lastPosition = go.transform.position.z;
            lastScale = go.transform.localScale.z;

            go.transform.parent = transform;
        }
    }

}
