using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    private PlayerScript playerScr;
    public string newAnim;
    
    private GameObject pistol;
    private PistolScript pistolScript;
    // Start is called before the first frame update
    void Start()
    {
        playerScr = GetComponentInParent<PlayerScript>();
        
        pistol = GameObject.Find("Pistol");
        pistolScript = pistol.GetComponent<PistolScript>();
    }

    // Update is called once per frame
    public void UpdateWeaponPos()
    {
        newAnim = playerScr.newAnim;
        if(newAnim == "UpAnim") transform.localPosition = new Vector3(0f, 0.1f, 0f);
        else if(newAnim == "DiagUpAnim") transform.localPosition = new Vector3(0.06f, 0.06f, 0f);
        else if(newAnim == "SideAnim") transform.localPosition = new Vector3(0.05f, -0.05f, 0f);
        else if(newAnim == "DiagDownAnim") transform.localPosition = new Vector3(0.08f , -0.06f, 0);
        else if(newAnim == "DownAnim") transform.localPosition = new Vector3(0f, -0.05f, 0f);

        if(pistol.activeSelf) pistolScript.UpdatePistolSprite();
    }

    public void CheckWeapon(){
        if(pistol.activeSelf) pistolScript.Shoot();
    }
}
