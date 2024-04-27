using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState State;
    public static int coins = 0;
    public static event Action<GameState> OnGameStateChanged;
    [SerializeField] public TextMeshProUGUI coinText;
    void Awake()
{
    if (Instance == null)
    {
        Instance = this;
        DontDestroyOnLoad(this.gameObject); // Đảm bảo đối tượng không bị hủy khi tải cảnh mới
    }
    else
    {
        Destroy(gameObject); // Hủy đối tượng nếu đã có một thể hiện khác tồn tại
    }
}

    // Start is called before the first frame update
    void Start()
    {
        UpdateGameState(GameState.MainMenu);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateGameState(GameState newState)
    {
        State = newState;
        switch (newState)
        {
            case GameState.MainMenu:
                Time.timeScale = 0;
                break;
            case GameState.Win:
                Time.timeScale = 0;
                break;
            case GameState.Settings:
                Time.timeScale = 0;
                break;
            case GameState.Gameplay:
            {
                Time.timeScale = 1;
                handleGameplayState();
                break;

            }
            case GameState.Level:
                Time.timeScale = 0;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState),newState,null);
        }
        OnGameStateChanged?.Invoke(newState);
    }


    private void handleGameplayState()
    {
        coinText.text = coins.ToString();
    }

    public enum GameState{
        Gameplay, 
        Win,
        Settings,
        MainMenu,
        Level
        

    }
}
