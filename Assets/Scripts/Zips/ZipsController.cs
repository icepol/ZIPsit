using System.Collections;
using System.Collections.Generic;
using pixelook;
using UnityEngine;

public class ZipsController : MonoBehaviour
{
    private bool _isGameRunning;
    private bool _isZipMoving;
    
    private void OnEnable()
    {
        EventManager.AddListener(Events.GAME_STARTED, OnGameStarted);
        EventManager.AddListener(Events.GAME_FINISHED, OnGameFinished);
        EventManager.AddListener(Events.ZIPS_MOVE_STARTED, OnZipsMoveStarted);
        EventManager.AddListener(Events.ZIPS_MOVE_FINISHED, OnZipsMoveFinished);
    }

    void Update()
    {
        if (!_isGameRunning) return;
        if (_isZipMoving) return;

        if (Input.GetKeyDown(KeyCode.Space))
            EventManager.TriggerEvent(Events.ZIPS_ORIENTATION_SELECTED);
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
        StartCoroutine(GameStarted());
    }

    IEnumerator GameStarted()
    {
        yield return null;
        
        _isGameRunning = true;
    }

    private void OnGameFinished()
    {
        _isGameRunning = false;
    }

    private void OnZipsMoveStarted()
    {
        _isZipMoving = true;
    }

    private void OnZipsMoveFinished()
    {
        _isZipMoving = false;
    }
}
