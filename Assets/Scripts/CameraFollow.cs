using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private bool autoMove = true;
    [SerializeField] private GameObject player = null;
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private Vector3 offset;

    private Vector3 depth = Vector3.zero;
    private Vector3 position=Vector3.zero;

    private void LateUpdate()
    {
        if (!GameManager.Instance.CanPlay) return;

        if (autoMove)
        {
            depth = transform.position += new Vector3(0, 0, speed*Time.deltaTime);
            position = Vector3.Lerp(transform.position, player.transform.position+offset, Time.deltaTime);
            transform.position=new Vector3(position.x,offset.y,depth.z);
        } else
        {
            position = Vector3.Lerp(transform.position, player.transform.position+offset, Time.deltaTime);
            transform.position=new Vector3(position.x,offset.y,position.z);
        }
    }
}
