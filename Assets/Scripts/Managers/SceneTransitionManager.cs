using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance;

    public static bool SceneTransitionDebounce {get; private set;}


    private string targetSpawnID;

    void Awake()
    {
        SceneTransitionDebounce = false;
        DontDestroyOnLoad(gameObject);
        if(Instance == null)
        {
            Instance = this;
        }
    }

    public void ChangeScene(string targetScene, string targetSpawn)
    {
        targetSpawnID = targetSpawn;

        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(targetScene);
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

        SendPlayerToTargetSpawn();
        StartCoroutine(SceneTransitionDebouncer());
    }

    private void SendPlayerToTargetSpawn()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        if(player == null )return;
        var transitPoints = GameObject.FindGameObjectsWithTag("TransitionPoints");
        
        foreach(GameObject t in transitPoints)
        {
            if(t.GetComponent<SceneTransitioner>().spawnPointID == targetSpawnID)
            {
                player.transform.position = t.transform.position;
            }
        }
    }


    public IEnumerator SceneTransitionDebouncer()
    {
        SceneTransitionDebounce = true;
        yield return TimeCache.Wait(3f);
        SceneTransitionDebounce = false;
    }
}