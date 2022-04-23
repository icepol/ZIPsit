using System.Collections;
using GameAnalyticsSDK;
using pixelook;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameSetup gameSetup;
    
    public static GameManager Instance { get; private set; }
    public GameSetup GameSetup => gameSetup;

    private void Awake()
    {
        Instance = this;

        GameState.OnApplicationStarted();
    }

    private void OnEnable()
    {
        EventManager.AddListener(Events.GAME_STARTED, OnGameStarted);
        EventManager.AddListener(Events.GAME_FINISHED, OnGameFinished);
    }

    private void Start()
    {
        GameAnalytics.Initialize();
        GameServices.Initialize();
    }

    // Update is called once per frame
    private void OnDisable()
    {
        EventManager.RemoveListener(Events.GAME_STARTED, OnGameStarted);
        EventManager.RemoveListener(Events.PLAYER_DIED, OnGameFinished);
    }

    private void OnGameStarted()
    {
        GameState.OnGameStarted();
    }

    private void OnGameFinished()
    {
        GameAnalytics.NewProgressionEvent(
            GAProgressionStatus.Fail, 
            "World_1", 
            //$"Level_{GameState.Level}",
            GameState.Score);
        
        GameServices.ReportScore(Constants.TopScoreLeaderBoardId, GameState.Score);
        GameServices.ReportScore(Constants.TopDistanceReachedId, GameState.Distance);

        StartCoroutine(WaitAndRestart());
    }

    IEnumerator WaitAndRestart()
    {
        yield return new WaitForSeconds(2.5f);
        
        Restart();
    }

    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }
}
