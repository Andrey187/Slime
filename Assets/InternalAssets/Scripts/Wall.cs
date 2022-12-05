using System;
using DestroyIt;
using UnityEngine;
using UnityEngine.AI;

public class Wall : MonoBehaviour
{
    [SerializeField]private EndCardNavMesh EndCardNavMesh;

    [SerializeField] private PlayerTail PlayerTail;
    [SerializeField] private ParticleSystem _particle;
    private NavMeshObstacle _nmo;
    public Destructible objectToDestroy;
    public int damagePerPress = 20; // The amount of damage to apply to all destructible objects per keypress.

    private void Start()
    {
        _nmo = FindObjectOfType<NavMeshObstacle>();
    }

    private void OnDestroy()
    {
        EndCardNavMesh.isDestroyed = true;
        EndCardNavMesh.isSwitchedPoint = false;
        _nmo.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        EndCardSlime p = collision.transform.GetComponent<EndCardSlime>();
        Transform ParticlePoint = collision.transform;

        if (p != null)
        {
            if (objectToDestroy != null)
                objectToDestroy.ApplyDamage(damagePerPress);
            else
            {
                Destructible[] destObjs = FindObjectsOfType<Destructible>();
                foreach (Destructible destObj in destObjs)
                    destObj.ApplyDamage(damagePerPress);
            }
            EndCardNavMesh.agents.Remove(collision.gameObject.GetComponent<NavMeshAgent>());

            var SpawnedParticle = Instantiate(_particle, this.transform);
            SpawnedParticle.transform.position = ParticlePoint.position;
            //collision.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            //collision.gameObject.transform.GetChild(1).gameObject.SetActive(false);
            //_particle.Play();

            PlayerTail.RemoveFirstBodyPart();
            EndCardNavMesh.CheckForAgents();
            EndCardNavMesh.Added = true;
        }
    }
}
