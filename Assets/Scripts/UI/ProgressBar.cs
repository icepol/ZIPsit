using System;
using pixelook;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private float timeLimit = 3f;

    private RectTransform _rectTransform;
    
    private bool _isTimerRunning;
    private float _currentTime;
    
    private void OnEnable()
    {
        EventManager.AddListener(Events.GAME_STARTED, OnGameStarted);
        EventManager.AddListener(Events.GAME_FINISHED, OnGameFinished);
        EventManager.AddListener(Events.ZIPS_MOVE_STARTED, OnZipsMoveStarted);
        EventManager.AddListener(Events.ZIPS_MOVE_FINISHED, OnZipsMoveFinished);
    }

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();

        HideTimer();
    }

    private void OnDisable()
    {
        EventManager.RemoveListener(Events.GAME_STARTED, OnGameStarted);
        EventManager.RemoveListener(Events.GAME_FINISHED, OnGameFinished);
        EventManager.RemoveListener(Events.ZIPS_MOVE_STARTED, OnZipsMoveStarted);
        EventManager.RemoveListener(Events.ZIPS_MOVE_FINISHED, OnZipsMoveFinished);
    }

    private void OnGameStarted()
    {
        ShowTimer();
        StartTimer();
    }
    
    private void OnGameFinished()
    {
        StopTimer();
    }

    private void OnZipsMoveStarted()
    {
        StopTimer();

        GameState.Score += (int) ((_currentTime / timeLimit) * 10) + 1;
    }

    private void OnZipsMoveFinished()
    {
        StartTimer();
    }

    void Update()
    {
        if (!_isTimerRunning) return;

        _currentTime -= Time.deltaTime;

        Vector3 scale = _rectTransform.localScale;

        scale.x = _currentTime / timeLimit;

        _rectTransform.localScale = scale;
        
        if (_currentTime <= 0)
            EventManager.TriggerEvent(Events.GAME_FINISHED);
    }

    private void HideTimer()
    {
        Vector3 scale = _rectTransform.localScale;

        scale.x = 0;

        _rectTransform.localScale = scale;
    }

    private void ShowTimer()
    {
        Vector3 scale = _rectTransform.localScale;

        scale.x = 1;

        _rectTransform.localScale = scale;
    }

    private void StartTimer()
    {
        _currentTime = timeLimit;
        _isTimerRunning = true;
    }

    private void StopTimer()
    {
        _isTimerRunning = false;
    }
}
