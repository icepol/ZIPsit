using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwistoBase : MonoBehaviour
{
    [SerializeField] private GameObject baseGreen;
    [SerializeField] private GameObject baseMagenta;
    
    void Start()
    {
        var randomness = Random.Range(0, 1f);
        
        baseGreen.SetActive(randomness > 0.5f);
        baseMagenta.SetActive(randomness <= 0.5f);
    }
}
