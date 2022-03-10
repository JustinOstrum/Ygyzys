using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public int collectibleValue;

    public ParticleSystem collectibleParticles;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameStateManager.Instance.changeCollectibles(collectibleValue);

            if(collectibleParticles != null)
            {
                GameObject.Instantiate(collectibleParticles, transform.position, Quaternion.identity);
            }

            Destroy(this.gameObject);                
        }
    }
}
