using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolScript : MonoBehaviour
{
    private PlayerScript playerScr;
    private SpriteRenderer spriteRenderer;
    private SpriteRenderer playerSpriteRenderer;
    private GameObject weapon;
    private WeaponScript weaponScript;
    private GameObject aim;

    public GameObject bulletPrefab;
    private ObjectPool pool;

    [SerializeField] Sprite diagDownSprite;
    [SerializeField] Sprite diagUpSprite;
    [SerializeField] Sprite downSprite;
    [SerializeField] Sprite sideSprite;
    [SerializeField] Sprite upSprite;
    // Start is called before the first frame update
    void Start()
    {
        playerScr = GetComponentInParent<PlayerScript>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerSpriteRenderer = GetComponentInParent<SpriteRenderer>();
        weapon = GameObject.Find("Weapon");
        weaponScript = weapon.GetComponent<WeaponScript>();
        aim = GameObject.Find("Aim");
        pool = new ObjectPool(bulletPrefab, 1000);

    }

    // Update is called once per frame
    public void UpdatePistolSprite()
    {
        string newAnim = playerScr.newAnim;
        
        if(newAnim == "UpAnim") {spriteRenderer.sprite = upSprite;
            spriteRenderer.sortingOrder = 9;
        }
        else if(newAnim == "DiagUpAnim") {spriteRenderer.sprite = diagUpSprite;
            spriteRenderer.sortingOrder = 9;
        }
        else if(newAnim == "SideAnim") {spriteRenderer.sprite = sideSprite;
            spriteRenderer.sortingOrder = 9;
        }
        else if(newAnim == "DiagDownAnim") {spriteRenderer.sprite = diagDownSprite;
            spriteRenderer.sortingOrder = 11;
        }
        else if(newAnim == "DownAnim") {spriteRenderer.sprite = downSprite;
            spriteRenderer.sortingOrder = 11;
        }
        
    }

    public void Shoot(){
        GameObject bullet = pool.GetFromPool();
        string newAnim = weaponScript.newAnim;
        int signalX = 1;
        if(playerScr.aimOnLeft) signalX = -1;
        if(newAnim == "UpAnim") bullet.transform.position = weapon.transform.position + new Vector3(0f, 0.21f, 0f);
        else if(newAnim == "DiagUpAnim") bullet.transform.position = weapon.transform.position + new Vector3(signalX * 0.16f, 0.05f, 0f);
        else if(newAnim == "SideAnim") bullet.transform.position = weapon.transform.position + new Vector3(signalX * 0.16f, 0f, 0f);
        else if(newAnim == "DiagDownAnim") bullet.transform.position = weapon.transform.position + new Vector3(signalX * 0.12f , -0.08f, 0);
        else if(newAnim == "DownAnim") bullet.transform.position = weapon.transform.position + new Vector3(0f, -0.15f, 0f);

        Vector3 vectorBulletAim = aim.transform.position - bullet.transform.position;
        float angle = Vector2.Angle(Vector3.up, vectorBulletAim);
        Quaternion targetRotation;
        targetRotation = Quaternion.Euler(0, 0, - signalX * angle);
        bullet.transform.rotation = targetRotation;
        
        bullet.SetActive(true);
    }
}
