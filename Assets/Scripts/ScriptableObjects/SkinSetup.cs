using pixelook;
using UnityEngine;

[CreateAssetMenu(fileName = "SkinSetup", menuName = "Assets/Skin Setup")]
public class SkinSetup : ScriptableObject
{
    [Header("Basic Setup")]
    public string skinName;

    [Header("Visual Setup")]
    public GameObject baseModel;
    public GameObject environmentModel;
    public GameObject rootModel;
}