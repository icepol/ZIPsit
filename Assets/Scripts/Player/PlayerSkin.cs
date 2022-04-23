using System;
using pixelook;
using UnityEngine;

public class PlayerSkin : MonoBehaviour
{
    [SerializeField] private GameObject selectedBaseSkin;
    [SerializeField] private GameObject selectedEnvironmentSkin;

    private void OnEnable()
    {
        EventManager.AddListener(Events.PLAYER_SKIN_CHANGED, OnPlayerSkinChanged);
    }

    private void Start()
    {
        ChangeSkin();
    }

    private void OnDisable()
    {
        EventManager.RemoveListener(Events.PLAYER_SKIN_CHANGED, OnPlayerSkinChanged);
    }

    private void OnPlayerSkinChanged()
    {
        ChangeSkin();
    }

    private void ChangeSkin()
    {
        if (selectedBaseSkin != null)
            Destroy(selectedBaseSkin);

        selectedBaseSkin =
             Instantiate(GameManager.Instance.GameSetup.skins[GameManager.Instance.GameSetup.SelectedSkinIndex].baseModel,
                 transform, false);
        
        if (selectedEnvironmentSkin != null)
            Destroy(selectedEnvironmentSkin);
        
        selectedEnvironmentSkin =
            Instantiate(GameManager.Instance.GameSetup.skins[GameManager.Instance.GameSetup.SelectedSkinIndex].environmentModel,
                transform, false);
    }
}
