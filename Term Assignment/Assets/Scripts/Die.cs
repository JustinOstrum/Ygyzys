using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour
{
    public Collectible attachedCollectible;
    void Update()
    {
        if (attachedCollectible == null)
        
            this.gameObject.SetActive(false);
    }
}
