using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider))]
public class TransitionPoint : MonoBehaviour
{
    public enum TransitionType
    {
        DifferentZone,
    }

    public enum TransitionWhen
    {
        OnTriggerEnter, ExternalCall,
    }

    [Tooltip("This is the gameobject that will transition.  For example, the player.")]
    public GameObject transitioningGameObject;
    [Tooltip("Whether the transition will be within this scene, to a different zone or a non-gameplay scene.")]
    public TransitionType transitionType;
    [SceneName]
    public string newSceneName;
    [Tooltip("The tag of the SceneTransitionDestination script in the scene being transitioned to.")]
    public SceneTransitionDestination.DestinationTag transitionDestinationTag;
    [Tooltip("What should trigger the transition to start.")]
    [SerializeField] private TransitionWhen transitionWhen;

    bool m_TransitioningGameObjectPresent;
    private int _nextSceneLoad;

    private void Start()
    {
        _nextSceneLoad = SceneManager.GetActiveScene().buildIndex + 1;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == transitioningGameObject)
        {
            m_TransitioningGameObjectPresent = true;

            if (SceneController.Transitioning)
                return;

            if (transitionWhen == TransitionWhen.OnTriggerEnter)
                TransitionInternal();

            if (_nextSceneLoad > PlayerPrefs.GetInt("levelAt"))
            {
                PlayerPrefs.SetInt("levelAt", _nextSceneLoad);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == transitioningGameObject)
        {
            m_TransitioningGameObjectPresent = false;
        }
    }

    private void Update()
    {
        if (SceneController.Transitioning)
            return;
        if (!m_TransitioningGameObjectPresent)
            return;
    }

    protected void TransitionInternal()
    {
        SceneController.TransitionToScene(this);
    }

    public void Transition()
    {
        if (!m_TransitioningGameObjectPresent)
            return;
    }
}
