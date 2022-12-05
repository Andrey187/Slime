using System.Collections;
using UnityEngine;

public class EndCardSlime : MonoBehaviour
{
    [SerializeField] private EndCardNavMesh EndCardNavMesh;
    private Animator animator;
    private PlayerTail PlayerTail;

    private void Start()
    {
        animator = GetComponent<Animator>();
        EndCardNavMesh = FindObjectOfType<EndCardNavMesh>();
        PlayerTail = GameObject.FindWithTag("Player").GetComponent<PlayerTail>();
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.name == "EndCard")
    //    {
    //        PlayerController p = transform.GetComponent<PlayerController>();
    //        if (p != null)
    //        {
    //            p.moveEnable = false;
    //        }
    //        else
    //        {
    //            PlayerTail.INDEX += 1;
    //            GetComponent<Rigidbody>().useGravity = false;
    //        }
    //        animator.SetBool("EndCard", true);
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "EndCard")
        {
            //PlayerController p = transform.GetComponent<PlayerController>();
            //if (p != null)
            //{
            //    p.moveEnable = false;
            //}
            //else
            {
                PlayerTail.INDEX += 1;
                //GetComponent<Rigidbody>().useGravity = false;
                EndCardNavMesh.isSwitchedPoint = true;
            }
            //animator.SetBool("EndCard", true);
        }
    }
}
