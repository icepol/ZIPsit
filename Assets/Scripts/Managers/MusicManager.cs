using UnityEngine;

namespace pixelook
{
    [RequireComponent(typeof(AudioSource))]
    public class MusicManager : MonoBehaviour
    {
        private float _currentFadeOutDuration;
        private bool _isFadingOut;
            
        private AudioSource audioSource;
        private static MusicManager _instance;

        private void Awake()
        {
            if (_instance != null)
                Destroy(gameObject);
            else
            {
                audioSource = GetComponent<AudioSource>();

                _instance = this;
                
                DontDestroyOnLoad(gameObject);                
            }
            
        }

        private void OnEnable()
        {
            EventManager.AddListener(Events.MUSIC_SETTINGS_CHANGED, OnMusicSettingsChanged);
        }

        private void Start()
        {
            if (!Settings.IsMusicEnabled)
                audioSource.Stop();
        }
        
        private void OnDisable()
        {
            EventManager.RemoveListener(Events.MUSIC_SETTINGS_CHANGED, OnMusicSettingsChanged);
        }

        private void OnMusicSettingsChanged()
        {
            if (Settings.IsMusicEnabled)
                audioSource.Play();
            else
                audioSource.Stop();
        }
    }
}