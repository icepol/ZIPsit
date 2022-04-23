using UnityEngine;

namespace pixelook
{
    public class ScoreBalloon : MonoBehaviour
    {
        [SerializeField] private float positionOffset = 1;
        private void Start()
        {
            transform.position += Vector3.up * positionOffset;
        }

        public void SetScore(int score)
        {
            string text = $"<b>+{score}</b>";
            
            foreach (TextMesh textMesh in GetComponentsInChildren<TextMesh>())
                textMesh.text = text;
        }

        public void Destroy()
        {
            // called from the animation
            // ObjectPoolManager.Instance.ReturnToPool(gameObject);
            Destroy(gameObject);
        }
    }
}