using UnityEngine;

public class TreeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject treePrefab;
    
    void Start()
    {
        treePrefab.SetActive(Random.Range(0, 1f) > 0.5f);

        float scale = Random.Range(0.5f, 1f);
        treePrefab.transform.localScale = new Vector3(scale, scale, scale);

        treePrefab.transform.localPosition = new Vector3(
            Random.Range(-0.1f, 0.1f), 
            0 ,
            Random.Range(-0.1f, 0.1f));
    }
}
