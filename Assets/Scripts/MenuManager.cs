using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuCanva;
    [SerializeField] private List<GameObject> soundButton;
    private bool isSound = true; 
    void Awake()
    {
        GameManager.OnGameStateChanged += GameManagerOnOnGameStateChanged;
    }

    private void GameManagerOnOnGameStateChanged(GameState state)
    {
        mainMenuCanva.SetActive(state == GameState.MainMenu);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartGame()
    {
        GameManager.Instance.UpdateGameState(GameState.Level);
    }
    public void MoveToSetting()
    {
        GameManager.Instance.UpdateGameState(GameState.Settings);
    }
    public void ChangeVolume()
    {
        isSound = !isSound;  
        AudioListener.volume = isSound ? 1 : 0;  
        for(int i = 0 ; i <= soundButton.Count - 1; i++)
        {
            soundButton[i].SetActive(isSound);
        }
        
        
    }

}
