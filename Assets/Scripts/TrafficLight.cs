using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLight : MonoBehaviour
{
   [SerializeField] private GameObject light=null;

   private void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag("train"))
      {
         light.SetActive(true);
      }
   }

   private void OnTriggerExit(Collider other)
   {
      if (other.CompareTag("train"))
      {
         light.SetActive(false);
      }
   }
}
