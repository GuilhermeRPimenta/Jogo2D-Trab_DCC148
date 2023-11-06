using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform followTransform;
    public float left = -11f;
    public float right = 1100f;
    public float up = 10.8f;
    public float down = -100f;

    void FixedUpdate()
    {
        if (followTransform.position.x > left && followTransform.position.x < right)
        {
            this.transform.position = new Vector3(followTransform.position.x, this.transform.position.y, this.transform.position.z);
        }
        if (followTransform.position.y > down && followTransform.position.y < up)
        {
            this.transform.position = new Vector3(this.transform.position.x, followTransform.position.y, this.transform.position.z);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
