using UnityEngine;

namespace pixelook
{
    public class CameraShake : MonoBehaviour
    {
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        void OnEnable()
        {
            EventManager.AddListener(Events.GAME_FINISHED, OnGameFinished);
        }

        private void OnDisable()
        {
            EventManager.RemoveListener(Events.GAME_FINISHED, OnGameFinished);
        }

        private void OnGameFinished()
        {
            _animator.SetTrigger("ShakeBig");
        }
    }
}