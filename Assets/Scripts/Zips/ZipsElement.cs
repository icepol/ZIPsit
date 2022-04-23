using System.Collections;
using pixelook;
using UnityEngine;
using Random = UnityEngine.Random;

public enum ZipsOrientation
{
    Up,
    Down,
}

public class ZipsElement : MonoBehaviour
{
    [SerializeField] private GameObject selectedBaseSkin;
    [SerializeField] private GameObject selectedEnvironmentSkin;

    [SerializeField] private GameObject baseSkinParent;
    [SerializeField] private GameObject environmentSkinParent;

    [SerializeField] private GameObject orientationWrapper;

    [SerializeField] private float startSwitchTime = 0.05f;
    [SerializeField] private float endSwitchTime = 0.8f;
    [SerializeField] private float increaseSwitchTimeBy = 0.01f;

    [SerializeField] private ParticleSystem explosion;

    public ZipsOrientation Orientation
    {
        get;
        private set;
    }

    public void RemoveEnvironment()
    {
        Destroy(selectedEnvironmentSkin);
    }
    
    public bool isActive;

    private bool isOrientationSelected;
    private float _nextSwitchTime;
    private float _currentSwitchTime;
    
    void Start()
    {
        Orientation = (ZipsOrientation) Random.Range(0, (int) ZipsOrientation.Down + 1);

        SetSkin();
        SetOrientation();

        _nextSwitchTime = startSwitchTime;
        _currentSwitchTime = _nextSwitchTime;
    }

    private void OnEnable()
    {
        EventManager.AddListener(Events.ZIPS_ORIENTATION_SELECTED, OnZipsOrientationSelected);
        EventManager.AddListener(Events.GAME_FINISHED, OnGameFinished);
        EventManager.AddListener(Events.PLAYER_SKIN_CHANGED, OnSkinChanged);
    }

    void Update()
    {
        if (isActive)
            SwitchOrientation();
    }

    private void OnDisable()
    {
        EventManager.RemoveListener(Events.ZIPS_ORIENTATION_SELECTED, OnZipsOrientationSelected);
        EventManager.RemoveListener(Events.GAME_FINISHED, OnGameFinished);
        EventManager.RemoveListener(Events.PLAYER_SKIN_CHANGED, OnSkinChanged);
    }
    
    void OnZipsOrientationSelected()
    {
        if (!isActive) return;
        if (isOrientationSelected) return;
        
        isOrientationSelected = true;
    }

    void OnGameFinished()
    {
        if (!isActive) return;

        isActive = false;

        StartCoroutine(WaitAndExplode());
    }

    void OnSkinChanged()
    {
        SetSkin();
    }

    void SetSkin()
    {
        if (selectedBaseSkin != null)
            Destroy(selectedBaseSkin.gameObject);

        selectedBaseSkin =
            Instantiate(GameManager.Instance.GameSetup.skins[GameManager.Instance.GameSetup.SelectedSkinIndex].baseModel,
                baseSkinParent.transform, false);
        
        if (selectedEnvironmentSkin != null)
            Destroy(selectedEnvironmentSkin.gameObject);
        
        if (GameManager.Instance.GameSetup.skins[GameManager.Instance.GameSetup.SelectedSkinIndex].environmentModel == null)
            return;

        selectedEnvironmentSkin =
            Instantiate(GameManager.Instance.GameSetup.skins[GameManager.Instance.GameSetup.SelectedSkinIndex].environmentModel,
                environmentSkinParent.transform, false);
    }

    void SetOrientation()
    {
        float xAngle = Orientation == ZipsOrientation.Down ? 180 : 0;
        orientationWrapper.transform.localRotation = new Quaternion(xAngle, 0, 0, 0);
    }

    void SwitchOrientation()
    {
        if (isOrientationSelected) return;
        
        _currentSwitchTime -= Time.deltaTime;

        if (_currentSwitchTime > 0)
            return;

        _currentSwitchTime = _nextSwitchTime;
        
        _nextSwitchTime += increaseSwitchTimeBy;
        if (_nextSwitchTime > endSwitchTime)
            _nextSwitchTime = endSwitchTime;

        Orientation = Orientation == ZipsOrientation.Down ? ZipsOrientation.Up : ZipsOrientation.Down;
        
        SetOrientation();
    }

    IEnumerator WaitAndExplode()
    {
        yield return new WaitForSeconds(0.5f);
        
        orientationWrapper.SetActive(false);
        environmentSkinParent.SetActive(false);
        
        explosion.Play();
        
        EventManager.TriggerEvent(Events.ZIPS_EXPLODED);
    }
}
