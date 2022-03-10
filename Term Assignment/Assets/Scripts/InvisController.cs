using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisController : MonoBehaviour
{
    public ParticleSystem secret;

    private void Start()
    {
       secret.Pause(); 
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            secret.Play();
        }
    }
}
