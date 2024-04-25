using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using static GameManager;
public class SettingsManager : MonoBehaviour
{
    [SerializeField] private GameObject settingsCanva;
    [SerializeField] private List<GameObject> levels;
    private bool isMuted = false; 
    void Awake()
    {
        GameManager.OnGameStateChanged += GameManagerOnOnGameStateChanged;
    }

    private void GameManagerOnOnGameStateChanged(GameState state)
    {
        settingsCanva.SetActive(state == GameState.Settings);
    }
    void Start()
    {

    }
    void Update()
    {

    }
    public void adjustVolume()
    {

    }
    public void adjustMusic()
    {

    }
    public void StartGame()
    {
        GameManager.Instance.UpdateGameState(GameState.Level);
    }
    public void BackToMainMenu()
    {
        GameManager.Instance.UpdateGameState(GameState.MainMenu);
    }
    public void Restart()
    {
        Time.timeScale = 1;
        Destroy(LevelManager.level);
        Instantiate(levels[LevelManager.CurrentGameLevel]);
    }
}