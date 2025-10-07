using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private GameObject cam;

    [SerializeField] private float parallaxEffect;
    private float xPosition;
    void Start()
    {
        cam=GameObject.Find("Main Camera");

        xPosition = transform.position.x;
    }

    
    void Update()
    {
        float dist = (cam.transform.position.x * parallaxEffect);

        transform.position = new Vector3(xPosition + dist, transform.position.y);
    }
}
