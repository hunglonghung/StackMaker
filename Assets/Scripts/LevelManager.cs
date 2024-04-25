using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static GameManager;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject levelManagerCanva;
    [SerializeField] private List<GameObject> levels;
    public static int CurrentGameLevel = 0;
    public static GameObject level;
    private bool isMuted = false; 
    void Awake()
    {
        GameManager.OnGameStateChanged += GameManagerOnOnGameStateChanged;
    }

    private void GameManagerOnOnGameStateChanged(GameState state)
    {
        levelManagerCanva.SetActive(state == GameState.Level);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SelectLevel(int levelNumber)
    {
        GameManager.Instance.UpdateGameState(GameState.Gameplay);
        switch (levelNumber)
        {
            case 1:
                level = Instantiate(levels[0]);
                CurrentGameLevel = 0;
                break;
            case 2:
                level = Instantiate(levels[1]);
                CurrentGameLevel = 1;
                break;
            case 3:
                level = Instantiate(levels[2]);
                CurrentGameLevel = 2;
                break;
            default:
                Debug.LogError("Undefined level number: " + levelNumber);
                break;
        }
    }
    public void MoveToSetting()
    {
        GameManager.Instance.UpdateGameState(GameState.Settings);
    }
    public void ChangeVolume()
    {
        isMuted = !isMuted;  
        AudioListener.volume = isMuted ? 0 : 1;  
    }
}
