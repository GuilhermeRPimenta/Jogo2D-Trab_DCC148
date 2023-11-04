using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private float speed = 15f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 v = Vector3.up * Time.deltaTime * speed;
        transform.Translate(v);
        /*if(false){
            gameObject.SetActive(false);
        }*/
    }
}
