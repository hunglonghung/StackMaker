using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEditor;
using UnityEngine;
using static GameManager;

public class LevelManager : MonoBehaviour
{
    [SerializeField] public CinemachineVirtualCamera followCamera;
    [SerializeField] private GameObject levelManagerCanva;
    [SerializeField] public List<GameObject> levels;
    [SerializeField] public TextMeshProUGUI levelText;
    public static int CurrentGameLevel = 0;
    public static GameObject level;
    public static GameObject ObjectToDestroy;
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
                ObjectToDestroy = level;
                CurrentGameLevel = 0;
                followCamera.Follow = FindObjectOfType<PlayerMovementFixed>().transform;
                levelText.text = "Level " + (CurrentGameLevel + 1).ToString();
                break;
            case 2:
                level = Instantiate(levels[1]);
                ObjectToDestroy = level;
                CurrentGameLevel = 1;
                followCamera.Follow = FindObjectOfType<PlayerMovementFixed>().transform;
                levelText.text = "Level " + (CurrentGameLevel + 1).ToString();
                break;
            case 3:
                level = Instantiate(levels[2]);
                ObjectToDestroy = level;
                CurrentGameLevel = 2;
                followCamera.Follow = FindObjectOfType<PlayerMovementFixed>().transform;
                levelText.text = "Level " + (CurrentGameLevel + 1).ToString();
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
    public void NextLevel()
    {
        Time.timeScale = 1;
        Destroy(ObjectToDestroy);
        level = Instantiate(levels[LevelManager.CurrentGameLevel + 1]);
        ObjectToDestroy = level;
        LevelManager.CurrentGameLevel += 1;
        followCamera.Follow = FindObjectOfType<PlayerMovementFixed>().transform;
        levelText.text = "Level " + (CurrentGameLevel + 1).ToString();
        GameManager.Instance.UpdateGameState(GameState.Gameplay);
    }
    public void Restart()
    {
        Time.timeScale = 1;
        Destroy(ObjectToDestroy);
        level = Instantiate(levels[LevelManager.CurrentGameLevel]);
        ObjectToDestroy = level;
        followCamera.Follow = FindObjectOfType<PlayerMovementFixed>().transform;
        levelText.text = "Level " + (CurrentGameLevel + 1).ToString();
        GameManager.Instance.UpdateGameState(GameState.Gameplay);
    }

}
