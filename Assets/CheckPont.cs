using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CheckPont : MonoBehaviour
{
    int pontos = 0;
    public TextMeshProUGUI text;

    private void Start()
    {
        text.text = "Pontos: 0";
    }

    private void OnTriggerEnter(Collider other)
    {        
        pontos ++;
        text.text = "Pontos: " +pontos.ToString();

    }
}
