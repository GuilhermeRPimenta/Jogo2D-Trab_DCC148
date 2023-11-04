using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private string currentAnimaton;
    private Animator animatorController;
    private float timer = 0f;
    private GameObject aim;
    [SerializeField] private float speed = 5f;
    public string newAnim;

    
    private WeaponScript[] weaponScript;
    public bool aimOnLeft;
    public bool aimBelow;

    // Start is called before the first frame update
    void Start()
    {
        animatorController = GetComponent<Animator>();
        aim = GameObject.Find("Aim");
        weaponScript = GetComponentsInChildren<WeaponScript>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        if(Input.GetMouseButtonDown(0)) weaponScript[0].CheckWeapon();
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
        transform.position = pos;


        


        
        UpdateAnimation(dx, dy);
    }

    void UpdateAnimation(float dx, float dy){
        
        Vector3 vectorPlayerAim = aim.transform.position - transform.position;
        float angle = Vector2.Angle(Vector3.up, vectorPlayerAim);

        
        if(angle >= 0 && angle < 30)  newAnim = "UpAnim";
        else if(angle >=30 && angle < 60)  newAnim = "DiagUpAnim";
        else if(angle >= 60 && angle < 120)  newAnim = "SideAnim";
        else if(angle >=120 && angle < 150)  newAnim =  "DiagDownAnim";
        else  newAnim = "DownAnim";

        aimOnLeft = false;
        aimBelow = false;
        if(aim.transform.position.x < transform.position.x) aimOnLeft = true;
        if(aim.transform.position.y < transform.position.y) aimBelow = true;

        bool playInReverse = false;
        if(aimOnLeft && dx > 0) playInReverse = true;
        else if(!aimOnLeft && dx < 0) playInReverse = true;
        else if(aimBelow && dy > 0) playInReverse = true;
        else if(!aimBelow && dy < 0) playInReverse = true;
        animatorController.SetBool("playInReverse", playInReverse);

        if(dx == 0f && dy == 0f){
        if(currentAnimaton != newAnim){
            animatorController.enabled = true;
            animatorController.Play(newAnim);
            currentAnimaton = newAnim;
            weaponScript[0].UpdateWeaponPos();
            Mirror(aimOnLeft);
            
        }
        timer += Time.deltaTime;
        if(timer < 0.2f) {return;}
        timer = 0f;
        animatorController.enabled = false;
        return;
        }
        
        animatorController.enabled = true;
        weaponScript[0].UpdateWeaponPos();
        Mirror(aimOnLeft);
        
        if(currentAnimaton == newAnim) return;
        animatorController.Play(newAnim);
        currentAnimaton = newAnim;
    }

    void Mirror(bool aimOnLeft){
        Quaternion targetRotation;
        if(aimOnLeft){
            targetRotation = Quaternion.Euler(0, 180, 0);
        }
        else{
            targetRotation = Quaternion.Euler(0, 0, 0);
        }
        transform.rotation = targetRotation;
        
    }
}
