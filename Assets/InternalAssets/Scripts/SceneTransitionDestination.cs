using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneTransitionDestination : MonoBehaviour
{
    public enum DestinationTag
    {
        A, B, C, D,
    }

    public DestinationTag destinationTag;    // This matches the tag chosen on the TransitionPoint that this is the destination for.
    [Tooltip("This is the gameobject that has transitioned.  For example, the player.")]
    public GameObject transitioningGameObject;
    public UnityEvent OnReachDestination;

    private void Awake()
    {
        SceneController.Instance.GetTransitionDestination();
    }
}
