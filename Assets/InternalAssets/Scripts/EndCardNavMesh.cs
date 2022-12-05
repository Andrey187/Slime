using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EndCardNavMesh : MonoBehaviour
{
    public List<NavMeshAgent> agents;
    [SerializeField] private PlayerTail playerTail;
    [SerializeField] private List<Transform> TargetPoses;
    [SerializeField] private Transform testpoint;

    public bool isDestroyed;
    public bool Added = false;
    public bool isSwitchedPoint = false;

    void Start()
    {
        CheckForAgents();
    }
    
    void Update()
    {
        GoToPoint(isDestroyed, isSwitchedPoint);
        foo(isDestroyed,isSwitchedPoint);
    }

    public void CheckForAgents()
    {
        if (!Added)
        {
            foreach (var bodyPart in playerTail._bodyParts)
            {
                NavMeshAgent Part = bodyPart.GetComponent<NavMeshAgent>();
                agents.Add(Part);
            }
        }
    }

    //ToDO refactoring 2 methonds in one
    private void GoToPoint(bool isDestroyed, bool isSwithed)
    {
        if (isDestroyed && isSwithed == false)
        {
            playerTail.GetComponent<PlayerTail>().enabled = false;
            for (int i = 0; i < agents.Count; i++)
            {
                agents[i].SetDestination(TargetPoses[i].position);
            }
        }
    }

    private void foo(bool isDestroyed,bool isSwithed)
    {
        if (isDestroyed == false && isSwithed == true)
        {
            playerTail.GetComponent<PlayerTail>().enabled = false;
            for (int i = 0; i < agents.Count; i++)
            {
                agents[i].SetDestination(testpoint.position);
            }
        }
    }
}
