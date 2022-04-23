using pixelook;
using UnityEngine;

public class GameOverPanel : MonoBehaviour
{
    private Vector2 _position;
    
    void Start()
    {
        // move off the screen
        _position = transform.position;
        
        transform.position = new Vector3(10000, 10000, 0);
    }

    private void OnEnable()
    {
        EventManager.AddListener(Events.GAME_FINISHED, OnGameFinished);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener(Events.GAME_FINISHED, OnGameFinished);
    }

    private void OnGameFinished()
    {
        transform.position = _position;
    }
}
