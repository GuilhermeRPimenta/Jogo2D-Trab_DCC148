using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeScript : MonoBehaviour
{
    private GameObject player;
    private Animator animatorController;
    private string currentAnimaton;
    private float cameraHalfHeight;
    private float cameraHalfWidth;
    private float speed = 1f;

    private int health = 5;

    // Start is called before the first frame update
    void Start()
    {
        animatorController = GetComponent<Animator>();
        player = GameObject.Find("Player");

        cameraHalfHeight = Camera.main.orthographicSize;
        cameraHalfWidth = cameraHalfHeight * Camera.main.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        
        Move();

        
        
    }

    void Move(){
        float dx;
        float dy;
        Vector3 pos = transform.position;
        if(pos.x > Camera.main.transform.position.x + cameraHalfWidth + 5.5f  || pos.x < Camera.main.transform.position.x - cameraHalfWidth - 5.5f) return;
        if(pos.y > Camera.main.transform.position.y + cameraHalfHeight + 5.5f  || pos.y < Camera.main.transform.position.y - cameraHalfHeight - 5.5f) return;

        if(pos.x < player.transform.position.x) dx = 1f;
        else dx = -1f;
        if(pos.y < player.transform.position.y) dy = 1f;
        else dy = -1f;
        
        float xStrength = Mathf.Abs(pos.x - player.transform.position.x);
        float yStrength = Mathf.Abs(pos.y - player.transform.position.y);

        //float xPercentage = xStrength / Mathf.Sqrt(Mathf.Pow(xStrength, 2) + Mathf.Pow(yStrength, 2));
        //float yPercentage = yStrength / Mathf.Sqrt(Mathf.Pow(xStrength, 2) + Mathf.Pow(yStrength, 2));

        //float xPercentage = xStrength/ (xStrength + yStrength);
        //float yPercentage = xStrength/ (xStrength + yStrength);

        string newAnim;
        if(xStrength > yStrength) newAnim = "SlimeSideAnim";
        else if(player.transform.position.y > transform.position.y) newAnim = "SlimeBackAnim";
        else newAnim = "SlimeFrontAnim";

        pos.x += dx * speed * Time.deltaTime + xStrength/4000f * dx;
        pos.y += dy * speed * Time.deltaTime + yStrength/4000f *dy;

        transform.position = pos;

        UpdateAnimation(newAnim);
    }
    void UpdateAnimation(string newAnim){

        Mirror();
        if(currentAnimaton == newAnim) return;
        animatorController.Play(newAnim);
        currentAnimaton = newAnim;
    }
    void Mirror(){
        Quaternion targetRotation;
        if(player.transform.position.x > transform.position.x) targetRotation = Quaternion.Euler(0, 180, 0);
        else targetRotation = Quaternion.Euler(0, 0, 0);
        transform.rotation = targetRotation;
    }

    void OnTriggerEnter2D(Collider2D target){
        if(target.gameObject.tag == "PlayerBullet"){
            Destroy(target.gameObject);
            health -= 1;
            if(health <=0){
                Destroy(gameObject);
            }
        }
        
    }
    
}
