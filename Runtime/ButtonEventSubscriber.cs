using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum LoadType
{
    SCENE_NAME = 0,
    SCENE_BUILD_INDEX = 1,
    SCENE_OFFSET = 2
}

[RequireComponent(typeof(Button))]
public class ButtonEventSubscriber : MonoBehaviour
{
    //public LoadType loadType = LoadType.SCENE_BUILD_INDEX;
    [SerializeField] LoadType loadType;
    [SerializeField] string scenePath;
    [SerializeField] int sceneBuildIndex;
    [SerializeField] int sceneOffset;
    [SerializeField] bool wait;
    [SerializeField] float waitTime;
    [SerializeField] bool animate;
    [SerializeField] Animator animator;
    [SerializeField] string triggerName;

    // Start is called before the first frame update
    void Start()
    {
        switch(loadType)
        {
            case LoadType.SCENE_NAME:
                if (animate)
                {
                    GetComponent<Button>().onClick.AddListener(() => { GameObject.Find("SceneManager").GetComponent<SceneManager>().LoadSceneBySceneName(scenePath, waitTime, animator, triggerName); });
                    return;
                }

                if (wait)
                {
                    GetComponent<Button>().onClick.AddListener(() => { GameObject.Find("SceneManager").GetComponent<SceneManager>().LoadSceneBySceneName(scenePath, waitTime); });
                    return;
                }

                GetComponent<Button>().onClick.AddListener(() => { GameObject.Find("SceneManager").GetComponent<SceneManager>().LoadSceneBySceneName(scenePath); });
                return;


            case LoadType.SCENE_BUILD_INDEX:
                if (animate)
                {
                    GetComponent<Button>().onClick.AddListener(() => { GameObject.Find("SceneManager").GetComponent<SceneManager>().LoadSceneBySceneBuildIndex(sceneBuildIndex, waitTime, animator, triggerName); });
                    return;
                }

                if (wait)
                {
                    GetComponent<Button>().onClick.AddListener(() => { GameObject.Find("SceneManager").GetComponent<SceneManager>().LoadSceneBySceneBuildIndex(sceneBuildIndex, waitTime); });
                    return;
                }

                GetComponent<Button>().onClick.AddListener(() => { GameObject.Find("SceneManager").GetComponent<SceneManager>().LoadSceneBySceneBuildIndex(sceneBuildIndex); });
                return;


            case LoadType.SCENE_OFFSET:
                if (animate)
                {
                    GetComponent<Button>().onClick.AddListener(() => { GameObject.Find("SceneManager").GetComponent<SceneManager>().LoadSceneWithOffset(sceneOffset, waitTime, animator, triggerName); });
                    return;
                }

                if (wait)
                {
                    GetComponent<Button>().onClick.AddListener(() => { GameObject.Find("SceneManager").GetComponent<SceneManager>().LoadSceneWithOffset(sceneOffset, waitTime); });
                    return;
                }

                GetComponent<Button>().onClick.AddListener(() => { GameObject.Find("SceneManager").GetComponent<SceneManager>().LoadSceneWithOffset(sceneOffset); });
                return;
        }
    }
}
