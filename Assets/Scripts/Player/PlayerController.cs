using pixelook;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float referenceWidth = 1242f;
    [SerializeField] private float mouseSensitivity = 3f;
 
    private Vector3 _startRocketPosition;
    private Vector3 _startMousePosition;
    private float _controllerHeight;
    private float _mouseSensitivity;

    private bool _isEnabled = true;
    private bool _isGameRunning;
    private bool _isMoving;
    private bool _isReturning;
    

    private void Awake()
    {

    }

    private void OnEnable()
    {
        EventManager.AddListener(Events.PANEL_SHOW, OnPanelShow);
        EventManager.AddListener(Events.PANEL_HIDE, OnPanelHide);
    }

    private void Start()
    {
        _controllerHeight = Screen.height / 3f;
        _mouseSensitivity = (Screen.width / referenceWidth) * mouseSensitivity;
    }

    void Update()
    {
        MovePlayerRocker();
    }

    private void OnDisable()
    {
        EventManager.RemoveListener(Events.PANEL_SHOW, OnPanelShow);
        EventManager.RemoveListener(Events.PANEL_HIDE, OnPanelHide);
    }

    private void OnPanelShow()
    {
        _isEnabled = false;
    }

    private void OnPanelHide()
    {
        _isEnabled = true;
    }

    private void MovePlayerRocker()
    {
        if (!_isEnabled) return;
        
    }
}
