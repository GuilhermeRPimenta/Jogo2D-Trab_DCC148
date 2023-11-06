using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScript : MonoBehaviour
{
    [SerializeField] Sprite rightDiagDownSprite;
    [SerializeField] Sprite rightDiagUpSprite;
    [SerializeField] Sprite downSprite;
    [SerializeField] Sprite rightSprite;
    [SerializeField] Sprite upSprite;

    private GameObject player;
    private SpriteRenderer spriteR;
    private bool playerOnLeft;
    private string direction;

    private float cameraHalfHeight;
    private float cameraHalfWidth;

    private float timer = 2f;
    private int health = 15;

    public GameObject bulletPrefab;
    private ObjectPool pool;

    private AudioSource audioSource;
    [SerializeField] AudioClip dmgSound;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip shotSound;
    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GameObject.Find("EnemyDeathSoundPlayer").GetComponent<AudioSource>();
        player = GameObject.Find("Player");
        spriteR = GetComponent<SpriteRenderer>();
        cameraHalfHeight = Camera.main.orthographicSize;
        cameraHalfWidth = cameraHalfHeight * Camera.main.aspect;
        pool = new ObjectPool(bulletPrefab, 10);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAnimation();

        CheckShoot();
    }

    void UpdateAnimation(){
        Vector3 vectorTowerPlayer = player.transform.position - transform.position;
        float angle = Vector2.Angle(Vector3.up, vectorTowerPlayer);

        if(angle >= 0 && angle < 30)  {spriteR.sprite = upSprite; direction = "Up";}
        else if(angle >=30 && angle < 60)  {spriteR.sprite = rightDiagUpSprite; direction = "DiagUp";}
        else if(angle >= 60 && angle < 120)  {spriteR.sprite = rightSprite; direction = "Side";}
        else if(angle >=120 && angle < 150)  {spriteR.sprite =  rightDiagDownSprite; direction = "DiagDown";}
        else  {spriteR.sprite = downSprite; direction = "Down";}

        playerOnLeft = false;
        if(player.transform.position.x < transform.position.x) playerOnLeft = true;
        Mirror();
    }

    void Mirror(){
        Quaternion targetRotation;
        if(playerOnLeft){
            targetRotation = Quaternion.Euler(0, 180, 0);
        }
        else{
            targetRotation = Quaternion.Euler(0, 0, 0);
        }
        transform.rotation = targetRotation;
    }

    void CheckShoot(){
        timer -= Time.deltaTime;
        if(timer <= 0){
            timer = 2;

            Vector3 pos = transform.position;
            if(pos.x > Camera.main.transform.position.x + cameraHalfWidth + 5.5f  || pos.x < Camera.main.transform.position.x - cameraHalfWidth - 5.5f
            || pos.y > Camera.main.transform.position.y + cameraHalfHeight + 5.5f  || pos.y < Camera.main.transform.position.y - cameraHalfHeight - 5.5f) return;

            Shoot();
            
        }
    }

    void Shoot(){
        audioSource.PlayOneShot(shotSound, 1);
        GameObject bullet = pool.GetFromPool();
        int signalX = 1;
        if(playerOnLeft) signalX = -1;

        if(direction == "Up") bullet.transform.position = transform.position + new Vector3(0f, 2.6f, 0f);
        else if(direction == "DiagUp") bullet.transform.position = transform.position + new Vector3(signalX * 1.3f, 2.5f, 0f);
        else if(direction == "Side") bullet.transform.position = transform.position + new Vector3(signalX * 1f, 1.6f, 0f);
        else if(direction == "DiagDown") bullet.transform.position = transform.position + new Vector3(signalX * 1f , 1.3f, 0);
        else if(direction == "Down") bullet.transform.position = transform.position + new Vector3(0f, 1f, 0f);

        Vector3 vectorBulletAim = player.transform.position - bullet.transform.position;
        float angle = Vector2.Angle(Vector3.up, vectorBulletAim);
        Quaternion targetRotation;
        targetRotation = Quaternion.Euler(0, 0, - signalX * angle);
        bullet.transform.rotation = targetRotation;
        
        bullet.SetActive(true);
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

    void OnCollisionEnter2D(Collision2D collision){
        Collider2D target = collision.collider;
        if(target.gameObject.tag == "PlayerBullet"){
            Destroy(target.gameObject);
            audioSource.PlayOneShot(dmgSound, 1);
            health -= 1;
            if(health <=0){
                audioSource.PlayOneShot(deathSound, 1);
                Destroy(gameObject);
            }
        }
        
    }
}
