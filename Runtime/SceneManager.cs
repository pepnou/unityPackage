using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    //[SerializeField] private Animator transition;

    private int sceneCount, sceneIndex;

    private void Start()
    {
        sceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        sceneCount = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings;
    }



    IEnumerator LoadScene(int buildIndex)
    {
        yield return new WaitForSeconds(0);
        UnityEngine.SceneManagement.SceneManager.LoadScene(buildIndex);
    }
    IEnumerator LoadScene(int buildIndex, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        UnityEngine.SceneManagement.SceneManager.LoadScene(buildIndex);
    }
    IEnumerator LoadScene(int buildIndex, float waitTime, Animator transition, string triggerName)
    {
        transition.SetTrigger(triggerName);
        yield return new WaitForSeconds(waitTime);
        UnityEngine.SceneManagement.SceneManager.LoadScene(buildIndex);
    }




    public void LoadSceneBySceneName(string scenePath)
    {
        StartCoroutine(LoadScene(SceneUtility.GetBuildIndexByScenePath("Assets/Scenes/" + scenePath + ".unity")));
    }
    public void LoadSceneBySceneName(string scenePath, float waitTime)
    {
        StartCoroutine(LoadScene(SceneUtility.GetBuildIndexByScenePath("Assets/Scenes/" + scenePath + ".unity"), waitTime));
    }
    public void LoadSceneBySceneName(string scenePath, float waitTime, Animator transition, string triggerName)
    {
        StartCoroutine(LoadScene(SceneUtility.GetBuildIndexByScenePath("Assets/Scenes/" + scenePath + ".unity"), waitTime, transition, triggerName));
    }



    public void LoadSceneBySceneBuildIndex(int buildIndex)
    {
        StartCoroutine(LoadScene(buildIndex));
    }
    public void LoadSceneBySceneBuildIndex(int buildIndex, float waitTime)
    {
        StartCoroutine(LoadScene(buildIndex, waitTime));
    }
    public void LoadSceneBySceneBuildIndex(int buildIndex, float waitTime, Animator transition, string triggerName)
    {
        StartCoroutine(LoadScene(buildIndex, waitTime, transition, triggerName));
    }



    public void LoadSceneWithOffset(int offset)
    {
        StartCoroutine(LoadScene((sceneIndex + sceneCount + (int)Mathf.Sign(offset) * (Mathf.Abs(offset) % sceneCount)) % sceneCount));
    }
    public void LoadSceneWithOffset(int offset, float waitTime)
    {
        StartCoroutine(LoadScene((sceneIndex + sceneCount + (int)Mathf.Sign(offset) * (Mathf.Abs(offset) % sceneCount)) % sceneCount, waitTime));
    }
    public void LoadSceneWithOffset(int offset, float waitTime, Animator transition, string triggerName)
    {
        StartCoroutine(LoadScene((sceneIndex + sceneCount + (int)Mathf.Sign(offset) * (Mathf.Abs(offset) % sceneCount)) % sceneCount, waitTime, transition, triggerName));
    }
}
