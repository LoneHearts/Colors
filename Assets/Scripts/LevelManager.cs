using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour {

    private float m_timeScale;
    private bool m_gamePaused = false;
    
    [HideInInspector]
    public bool m_levelFinished;
    [HideInInspector]
    public PlayerBehaviour player;
    private int numberOfEnemies;

    public static LevelManager Instance { get; private set; } // static singleton
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
    void FixedUpdate()
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

    void Update()
    {
        if(Input.GetButtonDown("Pause"))
        {
            PauseLevel();
        }
    }

    public void RestartLevel()
    {
        Scene scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene(); 
        UnityEngine.SceneManagement.SceneManager.LoadScene("DevRoom");
    }

    private void PauseLevel()
    {
        if(!m_levelFinished)
        {
            if(m_gamePaused)
            {
                m_gamePaused = false;
                Time.timeScale = m_timeScale;
                FindObjectOfType<UIPause>().Disable();
            }
            else
            {
                m_gamePaused = true;
                m_timeScale = Time.timeScale;
                Time.timeScale = 0f;
                FindObjectOfType<UIPause>().Enable();
            }
        }
    }

    public void EndLevel()
    {
        if(!m_levelFinished)
        {
            m_levelFinished = true;
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