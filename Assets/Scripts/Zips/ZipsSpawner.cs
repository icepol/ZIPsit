using System.Collections.Generic;
using pixelook;
using UnityEngine;

public enum ZipsPosition
{
    Left,
    Right,
}

public class ZipsSpawner : MonoBehaviour
{
    [SerializeField] private ZipsElement zipsElementPrefab;
    
    [SerializeField] private ZipsElementsContainer zipsElementsLeft;
    [SerializeField] private ZipsElementsContainer zipsElementsRight;

    [SerializeField] private GameObject selectedRootSkin;
    [SerializeField] private GameObject rootSkinParent;

    [SerializeField] private int expectedSize = 16;
    [SerializeField] private Vector3 zipsOffset = new Vector3(0, 0, 0.5f);

    private List<ZipsElement> _zipsElements = new();
    private ZipsPosition _nextZipsContainer;
    private bool _isGameRunning;
    private float _currentZipsItDuration;
    private int _movingZipsCount;
    private ZipsOrientation _zipsOrientation;
    private bool _isLastOrientationCorrect;
    
    private Dictionary<ZipsPosition, ZipsElementsContainer> _containersByPosition;

    void Start()
    {
        // which orientation will be the correct one
        _zipsOrientation = (ZipsOrientation) Random.Range(0, (int) ZipsOrientation.Down + 1);
        
        _nextZipsContainer = (ZipsPosition) Random.Range(0, (int) ZipsPosition.Right + 1);

        if (_nextZipsContainer == ZipsPosition.Left)
            zipsElementsRight.transform.localPosition = zipsOffset;
        else
            zipsElementsLeft.transform.localPosition = zipsOffset;

        _containersByPosition = new Dictionary<ZipsPosition, ZipsElementsContainer>
        {
            [ZipsPosition.Left] = zipsElementsLeft,
            [ZipsPosition.Right] = zipsElementsRight
        };

        SelectSkin();
        Spawn();
    }
    
    private void OnEnable()
    {
        EventManager.AddListener(Events.GAME_STARTED, OnGameStarted);
        EventManager.AddListener(Events.GAME_FINISHED, OnGameFinished);
        EventManager.AddListener(Events.ZIPS_ORIENTATION_SELECTED, OnZipsOrientationSelected);
        EventManager.AddListener(Events.ZIPS_MOVE_STARTED, OnZipsMoveStarted);
        EventManager.AddListener(Events.ZIPS_MOVE_FINISHED, OnZipsMoveFinished);
        EventManager.AddListener(Events.PLAYER_SKIN_CHANGED, OnPlayerSkinChanged);
        
    }

    private void OnDisable()
    {
        EventManager.RemoveListener(Events.GAME_STARTED, OnGameStarted);
        EventManager.RemoveListener(Events.GAME_FINISHED, OnGameFinished);
        EventManager.RemoveListener(Events.ZIPS_ORIENTATION_SELECTED, OnZipsOrientationSelected);
        EventManager.RemoveListener(Events.ZIPS_MOVE_STARTED, OnZipsMoveStarted);
        EventManager.RemoveListener(Events.ZIPS_MOVE_FINISHED, OnZipsMoveFinished);
        EventManager.RemoveListener(Events.PLAYER_SKIN_CHANGED, OnPlayerSkinChanged);
    }
    
    private void OnGameStarted()
    {
        _isGameRunning = true;
        
        ActivateElement();
    }

    private void OnGameFinished()
    {
        _isGameRunning = false;
    }

    void OnZipsOrientationSelected()
    {
        if (isOrientationCorrect())
        {
            Spawn();

            zipsElementsLeft.ZipsIt();
            zipsElementsRight.ZipsIt();
        }
        else
        {
            EventManager.TriggerEvent(Events.GAME_FINISHED);
        }
    }
    
    private void OnZipsMoveStarted()
    {
        _movingZipsCount++;
    }

    private void OnZipsMoveFinished()
    {
        _movingZipsCount--;
        
        if (_movingZipsCount > 0) return;
        
        RemoveFirstElement();
        ActivateElement();
        RemoveEnvironment();
    }

    private void OnPlayerSkinChanged()
    {
        SelectSkin();
    }

    void SelectSkin()
    {
        if (selectedRootSkin != null)
            Destroy(selectedRootSkin);

        selectedRootSkin =
            Instantiate(
                GameManager.Instance.GameSetup.skins[GameManager.Instance.GameSetup.SelectedSkinIndex].rootModel,
                rootSkinParent.transform, false);
    }

    void Spawn()
    {
        while (_zipsElements.Count < expectedSize)
        {
            var zipsElement = SpawnZipsElement();

            // get the next orientation
            ZipsOrientation orientation = _isLastOrientationCorrect 
                ? (ZipsOrientation) Random.Range(0, (int) ZipsOrientation.Down + 1) 
                : _zipsOrientation;

            _isLastOrientationCorrect = orientation == _zipsOrientation;
            
            // set the orientation
            zipsElement.Orientation = orientation;

            _zipsElements.Add(zipsElement);
        }
        
        RemoveEnvironment();
    }

    void ActivateElement()
    {
        _zipsElements[1].isActive = true;
    }

    void RemoveFirstElement()
    {
        Destroy(_zipsElements[0].gameObject);
        _zipsElements.RemoveAt(0);

        _zipsElements[0].isActive = false;
    }

    void RemoveEnvironment()
    {
        _zipsElements[0].RemoveEnvironment();
    }

    private ZipsElement SpawnZipsElement()
    {
        var zipsElement = Instantiate(zipsElementPrefab);
        
        _containersByPosition[_nextZipsContainer].AddElement(zipsElement);
        
        _nextZipsContainer = _nextZipsContainer == ZipsPosition.Left ? ZipsPosition.Right : ZipsPosition.Left;

        return zipsElement;
    }

    private bool isOrientationCorrect()
    {
        // check if the orientation of the first two elements is the same
        return _zipsElements[0].Orientation == _zipsElements[1].Orientation;
    }
}
