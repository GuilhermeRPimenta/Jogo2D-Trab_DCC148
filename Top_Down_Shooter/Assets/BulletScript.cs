using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private float speed = 15f;
    private float cameraHalfHeight;
    private float cameraHalfWidth;
    // Start is called before the first frame update
    void Start()
    {
        cameraHalfHeight = Camera.main.orthographicSize;
        cameraHalfWidth = cameraHalfHeight * Camera.main.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 v = Vector3.up * Time.deltaTime * speed;
        transform.Translate(v);
        Vector3 pos = transform.position;
        if(pos.x > Camera.main.transform.position.x + cameraHalfWidth + 0.5f  || pos.x < Camera.main.transform.position.x - cameraHalfWidth - 0.5f
        || pos.y > Camera.main.transform.position.y + cameraHalfHeight + 0.5f  || pos.y < Camera.main.transform.position.y - cameraHalfHeight - 0.5f){
            gameObject.SetActive(false);
        }
    }
}
