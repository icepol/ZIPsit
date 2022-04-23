using UnityEngine;

namespace pixelook
{
    public class SoundsManager : MonoBehaviour
    {
        [SerializeField] private Transform targetTransform;
        
        [SerializeField] private AudioClip zipsMoveStarted;
        [SerializeField] private AudioClip zipsExploded;

        private void OnEnable()
        {
            EventManager.AddListener(Events.ZIPS_MOVE_STARTED, OnZipsMoveStarted);
            EventManager.AddListener(Events.ZIPS_EXPLODED, OnZipsExploded);
        }

        private void OnDisable()
        {
            EventManager.RemoveListener(Events.ZIPS_MOVE_STARTED, OnZipsMoveStarted);
            EventManager.RemoveListener(Events.ZIPS_EXPLODED, OnZipsExploded);
        }

        private void OnZipsMoveStarted()
        {
            if (zipsMoveStarted && Settings.IsSfxEnabled)
                AudioSource.PlayClipAtPoint(zipsMoveStarted, targetTransform.position);
        }
        
        private void OnZipsExploded()
        {
            if (zipsExploded && Settings.IsSfxEnabled)
                AudioSource.PlayClipAtPoint(zipsExploded, targetTransform.position);
        }
    }
}