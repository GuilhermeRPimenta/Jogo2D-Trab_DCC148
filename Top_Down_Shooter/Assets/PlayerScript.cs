using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //public Hashtable animationDict = new Hashtable();
    private string currentAnimaton;
    private Animator animatorController;
    private float timer = 0f;
    private GameObject aim;
    [SerializeField] private float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
        animatorController = GetComponent<Animator>();
        
        aim = GameObject.Find("Aim");

        /*animationDict.Add("01", "UpAnim");
        animationDict.Add("0-1", "DownAnim");
        animationDict.Add("10", "SideAnim");
        animationDict.Add("11", "DiagUpAnim");
        animationDict.Add("1-1", "DiagDownAnim");
        animationDict.Add("-10", "SideAnim");
        animationDict.Add("-11", "DiagUpAnim");
        animationDict.Add("-1-1", "DiagDownAnim");*/
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move(){
        float dx = Input.GetAxisRaw("Horizontal");
        float dy = Input.GetAxisRaw("Vertical");
        if(dx != 0f && dy != 0f){
            dx = dx * 0.707107f;
            dy = dy * 0.707107f;
        }
        
        float movX = dx * speed * Time.deltaTime;
        float movY = dy * speed * Time.deltaTime;

        Vector3 pos = transform.position;
        pos.x += movX;
        pos.y += movY;
        Debug.Log(pos);
        transform.position = pos;


        

        
        //float angle = Mathf.Atan2(transform.position.x - aim.transform.position.x, transform.position.y - aim.transform.position.y) * Mathf.Rad2Deg;

        //Debug.Log(angle);

        
        
        UpdateAnimation(dx, dy);
    }

    void UpdateAnimation(float dx, float dy){
        
        Vector3 vectorPlayerAim = aim.transform.position - transform.position;
        float angle = Vector2.Angle(Vector3.up, vectorPlayerAim);

        string newAnim;
        if(angle >= 0 && angle < 15)  newAnim = "UpAnim";
        else if(angle >=15 && angle < 75)  newAnim = "DiagUpAnim";
        else if(angle >= 75 && angle < 105)  newAnim = "SideAnim";
        else if(angle >=105 && angle < 165)  newAnim =  "DiagDownAnim";
        else  newAnim = "DownAnim";

        

        if(dx == 0f && dy == 0f){
        if(currentAnimaton != newAnim){
            animatorController.enabled = true;
            animatorController.Play(newAnim);
            currentAnimaton = newAnim;
            Mirror();
            
        }
        timer += Time.deltaTime;
        if(timer < 0.2f) {return;}
        timer = 0f;
        animatorController.enabled = false;
        return;
        }
        
        animatorController.enabled = true;
        
        Mirror();
        
        if(currentAnimaton == newAnim) return;
        animatorController.Play(newAnim);
        currentAnimaton = newAnim;
    }

    void Mirror(){
        if(aim.transform.position.x < transform.position.x){
            Quaternion targetRotation = Quaternion.Euler(0, 180, 0);
            transform.rotation = targetRotation;
        }
        else{
            Quaternion targetRotation = Quaternion.Euler(0, 0, 0);
            transform.rotation = targetRotation;
        }
        
    }
}
