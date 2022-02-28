using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float speed = 0;
    [SerializeField] private float moveDirection = 0;
    [SerializeField] private bool parentOnTrigger = true;
    [SerializeField] private bool hitBoxOnTrigger = false;
    [SerializeField] private GameObject moverObject=null;

    private Renderer renderer=null;
    private bool isVisible = false;

    public float Speed
    {
        get => speed;
        set => speed = value;
    }

    private void Start()
    {
        renderer = moverObject.GetComponent<Renderer>();
    }

    private void Update()
    {
        transform.Translate(    Vector3.right * speed  * Time.deltaTime);

        IsVisible();
    }

    private void IsVisible()
    {
        if (renderer.isVisible) isVisible = true;

        if (!renderer.isVisible && isVisible)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (parentOnTrigger)
            {
                other.transform.parent = transform;
                other.GetComponent<PlayerController>().ParentedToObject = true;
            }
            if(hitBoxOnTrigger)      other.gameObject.GetComponent<PlayerController>().GotHit();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (parentOnTrigger)
            {
                other.transform.parent = null;
                other.GetComponent<PlayerController>().ParentedToObject = false;
            }
        }
    }
}
