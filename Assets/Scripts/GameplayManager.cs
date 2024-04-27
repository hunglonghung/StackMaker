using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using static GameManager;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] private GameObject gameplayManagerCanva;
    [SerializeField] private List<GameObject> levels;
    [SerializeField] private TextMeshProUGUI coinText;  
    [SerializeField] private TextMeshProUGUI levelText;
    private bool isMuted = false; 
    void Awake()
    {
        GameManager.OnGameStateChanged += GameManagerOnOnGameStateChanged;
    }

    private void GameManagerOnOnGameStateChanged(GameState state)
    {
        gameplayManagerCanva.SetActive(state == GameState.Gameplay);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Pause()
    {
        Time.timeScale = 0;  
        GameManager.Instance.UpdateGameState(GameState.Settings);  
    }
    public void Continue()
    {
        Time.timeScale = 1;  
        GameManager.Instance.UpdateGameState(GameState.Gameplay);  
    }
    

}
