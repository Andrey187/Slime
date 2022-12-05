using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance
    {
        get
        {
            if (instance != null)
                return instance;

            instance = FindObjectOfType<SceneController>();

            if (instance != null)
                return instance;

            Create();

            return instance;
        }
    }
    public static bool Transitioning
    {
        get { return Instance.m_Transitioning; }
    }

    protected static SceneController instance;

    public static SceneController Create()
    {
        GameObject sceneControllerGameObject = new GameObject("SceneController");
        instance = sceneControllerGameObject.AddComponent<SceneController>();

        return instance;
    }

    public SceneTransitionDestination initialSceneTransitionDestination;

    private Scene m_CurrentZoneScene;
    protected SceneTransitionDestination.DestinationTag m_ZoneRestartDestinationTag;
    protected bool m_Transitioning;

    private void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    public void GetTransitionDestination()
    {
        initialSceneTransitionDestination = FindObjectOfType<SceneTransitionDestination>();

        if (initialSceneTransitionDestination != null)
        {
            SetEnteringGameObjectLocation(initialSceneTransitionDestination);
            initialSceneTransitionDestination.OnReachDestination.Invoke();
        }
        else
        {
            m_ZoneRestartDestinationTag = SceneTransitionDestination.DestinationTag.A;
        }
    }

    public void GetSceneName()
    {
        m_CurrentZoneScene = SceneManager.GetActiveScene();
        Debug.Log("sceneNameAwake: " + Instance.m_CurrentZoneScene.name);
    }

    public static void RestartZone()
    {
        Instance.StartCoroutine(Instance.Transition(Instance.m_CurrentZoneScene.name, 
            Instance.m_ZoneRestartDestinationTag, TransitionPoint.TransitionType.DifferentZone));
        Debug.Log("sceneNameRestart: " + Instance.m_CurrentZoneScene.name);
    }

    public static void TransitionToScene(TransitionPoint transitionPoint)
    {
        Instance.StartCoroutine(Instance.Transition(transitionPoint.newSceneName, 
            transitionPoint.transitionDestinationTag, transitionPoint.transitionType));
    }

    public static SceneTransitionDestination GetDestinationFromTag(SceneTransitionDestination.DestinationTag destinationTag)
    {
        return Instance.GetDestination(destinationTag);
    }

    protected IEnumerator Transition(string newSceneName, 
        SceneTransitionDestination.DestinationTag destinationTag, 
        TransitionPoint.TransitionType transitionType = TransitionPoint.TransitionType.DifferentZone)
    {
        m_Transitioning = true;
        yield return SceneManager.LoadSceneAsync(newSceneName);
        SceneTransitionDestination entrance = GetDestination(destinationTag);
        SetEnteringGameObjectLocation(entrance);
        SetupNewScene(transitionType,entrance);
        if (entrance != null)
            entrance.OnReachDestination.Invoke();
        m_Transitioning = false;
    }

    protected SceneTransitionDestination GetDestination(SceneTransitionDestination.DestinationTag destinationTag)
    {
        SceneTransitionDestination[] entrances = FindObjectsOfType<SceneTransitionDestination>();
        for (int i = 0; i < entrances.Length; i++)
        {
            if (entrances[i].destinationTag == destinationTag)
                return entrances[i];
        }
        Debug.LogWarning("No entrance was found with the " + destinationTag + " tag.");
        return null;
    }

    protected void SetEnteringGameObjectLocation(SceneTransitionDestination entrance)
    {
        if (entrance == null)
        {
            Debug.LogWarning("Entering Transform's location has not been set.");
            return;
        }
        Transform entranceLocation = entrance.transform;
        Transform enteringTransform = entrance.transitioningGameObject.transform;
        enteringTransform.position = entranceLocation.position;
        enteringTransform.rotation = entranceLocation.rotation;
    }

    protected void SetupNewScene(TransitionPoint.TransitionType transitionType, SceneTransitionDestination entrance)
    {
        if (entrance == null)
        {
            Debug.LogWarning("Restart information has not been set.");
            return;
        }

        if (transitionType == TransitionPoint.TransitionType.DifferentZone)
            SetZoneStart(entrance);
    }


    protected void SetZoneStart(SceneTransitionDestination entrance)
    {
        m_CurrentZoneScene = entrance.gameObject.scene;
        m_ZoneRestartDestinationTag = entrance.destinationTag;
    }

}

