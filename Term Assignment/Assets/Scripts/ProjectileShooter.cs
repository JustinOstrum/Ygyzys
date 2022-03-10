using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    public float MaxTime;

    public float currentTime;

    public ProjectileController projectilePrefab;
            
    void Start()
    {
        currentTime = MaxTime;
    }

    void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime > MaxTime)
        {
            ProjectileController temp = GameObject.Instantiate<ProjectileController>(projectilePrefab);

            temp.transform.position = 
                transform.position + Vector3.up * 0.4f * Mathf.Sign(transform.localScale.y);
            
            currentTime = 0;
        }

    }
    
}
