using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneEnum
{
    OpeningScene,
    MainScene,
    MazeScene,
    BossScene,
    LoadingScene,
}

public static class SceneLoader 
{

    private class SceneLoaderMonoBehaviour: MonoBehaviour { }

    private static Action onLoaderCallback;
    private static AsyncOperation loadingAsyncOperation;

    private static DontDestroy[] dontDestroyObjs;

    public static void Load(SceneEnum scene)
    {
        dontDestroyObjs = GameObject.FindObjectsOfType<DontDestroy>();

        onLoaderCallback = () =>
        {
            GameObject loadingGameObject = new GameObject("Loading Game Object");

            loadingGameObject.AddComponent<SceneLoaderMonoBehaviour>().StartCoroutine(LoadSceneAsync(scene));

        };

        SceneManager.LoadScene(SceneEnum.LoadingScene.ToString());
    }

    private static IEnumerator LoadSceneAsync(SceneEnum scene)
    {
        yield return null;

        loadingAsyncOperation = SceneManager.LoadSceneAsync(scene.ToString());

        while (!loadingAsyncOperation.isDone)
        {

            yield return null;
        }


    }

    public static float GetLoadingProgress()
    {
        if(loadingAsyncOperation != null)
        {
            return loadingAsyncOperation.progress;
        } else
        {
            return 1f;
        }
    }

    public static void LoaderCallback()
    {
        if(onLoaderCallback != null)
        {

            onLoaderCallback();

            onLoaderCallback = null;
        }
    }

    public static void DisableDontDestroyObjects()
    {

        foreach (DontDestroy obj in SceneLoader.dontDestroyObjs)
        {
            obj.gameObject.SetActive(false);
        }
    }

    public static void EnableDontDestroyObjects()
    {
        Debug.Log("objs:" + dontDestroyObjs);

        foreach (DontDestroy obj in SceneLoader.dontDestroyObjs)
        {
            obj.gameObject.SetActive(true);
        }
    }
}
