using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int coinValue;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            print("Coin Collected");



            // Todo recycle coin with pool

            // Todo update game manager

            Destroy(gameObject);
        }
    }
}
