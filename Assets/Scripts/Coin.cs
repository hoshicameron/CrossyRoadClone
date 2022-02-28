using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int coinValue = 1;
    [SerializeField] private AudioClip coinClip;
    //private AudioSource audioSource;

    private void Start()
    {
        //audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Todo recycle coin with pool

            GameManager.Instance.UpdateCoinCount(coinValue);

            AudioSource.PlayClipAtPoint(coinClip,Camera.main.transform.position);
            //audioSource.PlayOneShot(coinClip);

            Destroy(gameObject);
        }
    }
}
