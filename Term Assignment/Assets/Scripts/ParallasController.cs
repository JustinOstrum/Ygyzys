using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallasController : MonoBehaviour
{
    public float parallaxFactor = 1;
    public bool ignoreY = true;
    public bool ignoreX = false;

    Vector3 cameraStartPosition;
    Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;

        cameraStartPosition = Camera.main.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = startPosition + (Camera.main.transform.position - cameraStartPosition) * parallaxFactor;
    
        if (ignoreY)
        {
            transform.position = new Vector3(transform.position.x, startPosition.y);               
        }

        if (ignoreX)
        {
            transform.position = new Vector3(startPosition.x, transform.position.y);
        }
    
    }
}
