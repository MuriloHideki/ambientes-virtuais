using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cenoura : MonoBehaviour
{
    public List<GameObject> cenouras;
    public GameObject ferradura;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("BATEU!!!!!");
        if(other.name == "Faca")
        {
            ferradura.SetActive(false);

            foreach(GameObject iten in cenouras) 
            {
                iten.SetActive(true);
            }
        }
    }
}