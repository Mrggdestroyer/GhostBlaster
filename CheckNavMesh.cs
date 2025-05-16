using System;
using UnityEngine;
using UnityEngine.AI;
public class CheckNavMesh : MonoBehaviour
{
    public MeshRenderer meshRenderer;

    private bool navMeshAvailable = false;
    void Start()
    {

    }

    public void Update()
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(Vector3.zero, out hit, 1000.0f, NavMesh.AllAreas))
        {
            Vector3 result = hit.position;
            navMeshAvailable = true;
        }
        else
        {
            navMeshAvailable = false;
        }

        if (navMeshAvailable) meshRenderer.enabled = false;
    }

}