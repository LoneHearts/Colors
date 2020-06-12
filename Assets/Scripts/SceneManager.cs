using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour {
    public PlayerBehaviour player;
    public static SceneManager Instance { get; private set; } // static singleton
    void Awake() 
    {
        if (Instance == null) 
        { 
            Instance = this;  
        }
        else
        { 
            Destroy(gameObject); 
        }
        
        player = FindObjectOfType<PlayerBehaviour>();
        
    }
}