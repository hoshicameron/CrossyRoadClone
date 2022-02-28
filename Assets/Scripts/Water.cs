using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    private bool hitWater = false;
    private void OnTriggerStay(Collider other)
    {
        if(hitWater)    return;

        if (other.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();

            if (!playerController.ParentedToObject && !playerController.IsJumping)
            {
                playerController.GotSoaked();
            }
        }
    }
}
