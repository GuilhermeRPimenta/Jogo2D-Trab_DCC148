using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    private GameObject player;
    private Animator animatorController;
    private string currentAnimaton;
    private float cameraHalfHeight;
    private float cameraHalfWidth;
    private float speed = 1f;

    private float shootTimer = 4;
    private float shootChargeTimer = 2;
    private bool isShooting = false;

    private int health = 150;
    public GameObject bulletPrefab;

    public GameObject camera;
    private bool hasPlayedPresentation = false;
    private float presentationStartTimer = 2;
    private float presentationTimer = 4;
    private float presentationEndTimer = 2;

    private AudioSource audioSource;
    [SerializeField] AudioClip dmgSound;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip shotSound;

    private GameObject levelLoader;
    private LevelLoaderScript levelLoaderScript;

    void Start(){
        levelLoader = GameObject.Find("LevelLoader");
        audioSource = GameObject.Find("EnemyDeathSoundPlayer").GetComponent<AudioSource>();
        animatorController = GetComponent<Animator>();
        player = GameObject.Find("Player");

        cameraHalfHeight = Camera.main.orthographicSize;
        cameraHalfWidth = cameraHalfHeight * Camera.main.aspect;


    }

    // Update is called once per frame
    void Update()
    {
        // if(!hasPlayedPresentation){
        //     Presentation();
        //     return;
        // }

        if(shootTimer > 0){
            Move();
            shootTimer -= Time.deltaTime;
        } else {
            if(shootChargeTimer > 0){
                shootChargeTimer -= Time.deltaTime;
            } else {
                Shoot();
                shootTimer = 4;
                shootChargeTimer = 2;
            }
        }

        
        
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
            audioSource.PlayOneShot(dmgSound, 1);
            target.gameObject.SetActive(false);
            health -= 1;
            speed = 1f + 0.02f * (150-health);
            if(health <=0){
                Destroy(gameObject);
                audioSource.PlayOneShot(deathSound, 1);
                LevelLoader.LoadNextLevel();
            }
        }
        
    }

    void Shoot(){
        audioSource.PlayOneShot(shotSound, 1);
        GameObject bullet = Instantiate(bulletPrefab);
        int signalX = 1;

        bullet.transform.position = transform.position;
        bullet.transform.localScale = new Vector3(30, 30, 0);

        if(player.transform.position.x < transform.position.x) signalX = -1;
        Vector3 vectorBulletAim = player.transform.position - bullet.transform.position;
        float angle = Vector2.Angle(Vector3.up, vectorBulletAim);
        Quaternion targetRotation;
        targetRotation = Quaternion.Euler(0, 0, - signalX * angle);
        bullet.transform.rotation = targetRotation;
    }

    void Presentation(){
        // if(presentationStartTimer > 0){
        //     presentationStartTimer -= Time.deltaTime;
        //     return;
        // }

        // if(presentationTimer > 0){
        //     // UpdateAnimation("Presentation");
        //     camera.transform.position = Vector3.Lerp(player.transform.position, )
        //     presentationTimer -= Time.deltaTime;
        //     return;
        // }

        // if(presentationEndTimer > 0){
        //     presentationEndTimer -= Time.deltaTime;
        //     return;
        // }

        // hasPlayedPresentation = true;

    }
    
}
