using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelScript : MonoBehaviour{

    private GameObject levelLoader;
    private LevelLoaderScript levelLoaderScript;

    void Start(){
        levelLoader = GameObject.Find("LevelLoader");
        levelLoaderScript = levelLoader.GetComponent<LevelLoaderScript>();
    }

    void OnTriggerEnter2D(Collider2D target){
        Debug.Log("asd");
        if(target.gameObject.tag == "Player"){
            levelLoaderScript.LoadNextLevel();
        }
        
    }
}
