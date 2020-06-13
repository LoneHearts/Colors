using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManager : MonoBehaviour {

    public bool m_levelfinished;
    public PlayerBehaviour player;
    private int numberOfEnemies;

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

    void Start()
    {
        CountEnemies();
    }
    void Update()
    {
        if(Input.GetButtonDown("Restart"))
        {
            RestartLevel();
        }
        if(numberOfEnemies < 1)
        {
            EndLevel();
        }
    }

    public void RestartLevel()
    {
        Scene scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene(); 
        UnityEngine.SceneManagement.SceneManager.LoadScene("DevRoom");
    }

    public void EndLevel()
    {
        if(!m_levelfinished)
        {
            m_levelfinished = true;
            player.enabled = false;
            FindObjectOfType<UIWin>().Enable();
        }
    }
    private void CountEnemies()
    {
        numberOfEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    public void EnemyKilled()
    {
        numberOfEnemies--;
    }
}