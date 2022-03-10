using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFollow : MonoBehaviour
{
    Scene scene;
    
    public Transform target;

    public Vector2 minXandY;

    public Vector2 maxXandY;

    public Vector2 Smooth;

    public Vector2 Margin;

    private void Start()
    {
        scene = SceneManager.GetActiveScene();
    }

    private void FixedUpdate()
    {
        Track();
    }

    private void Track()
    {
        float targetX = transform.position.x;
        float targetY = transform.position.y;

        if (Mathf.Abs(transform.position.x - target.position.x) > Margin.x)
        {
            targetX = Mathf.Lerp(transform.position.x, transform.position.x + Mathf.Sign(target.position.x - transform.position.x) * Margin.x, Smooth.x * Time.deltaTime);
        }

        if (Mathf.Abs(transform.position.y - target.position.y) > Margin.y)
        {
            targetY = Mathf.Lerp(transform.position.y, target.position.y, Smooth.y * Time.deltaTime);
        }

        if (scene.name == "Level3")
        {
            if (transform.position.y > -11.2f && transform.position.x < 30f)
            {
                targetX = Mathf.Clamp(targetX, minXandY.x, 7f);
            }            
        }

        targetX = Mathf.Clamp(targetX, minXandY.x, maxXandY.x);
        targetY = Mathf.Clamp(targetY, minXandY.y, maxXandY.y);

        transform.position = new Vector3(targetX, targetY, transform.position.z);
    }
}
