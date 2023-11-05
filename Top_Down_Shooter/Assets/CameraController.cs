using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform followTransform;

    void FixedUpdate()
    {
        if (followTransform.position.x > -11 && followTransform.position.x < 1100)
        {
            this.transform.position = new Vector3(followTransform.position.x, this.transform.position.y, this.transform.position.z);
        }
        if (followTransform.position.y > -100.8 && followTransform.position.y < 10.8)
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
