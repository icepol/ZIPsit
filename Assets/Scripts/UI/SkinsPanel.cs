using System;
using System.Collections;
using System.Collections.Generic;
using pixelook;
using UnityEngine;
using UnityEngine.UI;

public class SkinsPanel : MonoBehaviour
{
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;
    
    [SerializeField] private Text selectedSkinText;

    private void Start()
    {
        UpdateUI();
    }

    public void OnLeftButtonClick()
    {
        GameManager.Instance.GameSetup.SelectedSkinIndex--;
        
        UpdateUI();
        
        EventManager.TriggerEvent(Events.PLAYER_SKIN_CHANGED);
    }

    public void OnRightButtonClick()
    {
        GameManager.Instance.GameSetup.SelectedSkinIndex++;
        
        UpdateUI();
        
        EventManager.TriggerEvent(Events.PLAYER_SKIN_CHANGED);
    }

    void UpdateUI()
    {
        leftButton.gameObject.SetActive(GameManager.Instance.GameSetup.SelectedSkinIndex > 0);
        rightButton.gameObject.SetActive(GameManager.Instance.GameSetup.SelectedSkinIndex < GameManager.Instance.GameSetup.skins.Length - 1);

        selectedSkinText.text =
            GameManager.Instance.GameSetup.skins[GameManager.Instance.GameSetup.SelectedSkinIndex].skinName;
    }
}
