using UnityEngine;

public class SceneTransitioner : MonoBehaviour
{
    public string spawnPointID;

    [SerializeField] private string targetSceneID;
    [SerializeField] private string targetSpawnPointID;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !SceneTransitionManager.SceneTransitionDebounce)
        {
            SceneTransitionManager.Instance.ChangeScene(targetSceneID, targetSpawnPointID);
        }
    }
}
