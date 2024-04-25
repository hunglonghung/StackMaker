using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState State;
    public static event Action<GameState> OnGameStateChanged;
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
                break;
            case GameState.Win:
                break;
            case GameState.Settings:
                break;
            case GameState.Gameplay:
                break;
            case GameState.Level:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState),newState,null);
        }
        OnGameStateChanged?.Invoke(newState);
    }

    public enum GameState{
        Gameplay, 
        Win,
        Settings,
        MainMenu,
        Level
        

    }
}
