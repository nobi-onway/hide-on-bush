using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Visisble : MonoBehaviour
{
    public UnityEvent detectedPlayer;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.gameObject.CompareTag("Player")) return;
               
        detectedPlayer.Invoke();
    }
}
