using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class ZombieTeleport : MonoBehaviour
{
    private Renderer[] renderers;
    private Collider[] colliders;
    public Transform[] spawnLocations;
    private int lastSpawnIndex = -1;
    private UnityEngine.AI.NavMeshAgent agent;
    public AudioSource DYING;

    private void Awake()
    {
        renderers = GetComponentsInChildren<Renderer>();
        colliders = GetComponentsInChildren<Collider>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }
    public void Die()
    {
        DYING.Play();
        SetVisible(false);

        int randomIndex;
        do
        {
            randomIndex = Random.Range(0, spawnLocations.Length);
        }
        while (randomIndex == lastSpawnIndex && spawnLocations.Length > 1);
        lastSpawnIndex = randomIndex;
        if (agent != null)
        {
            agent.Warp(spawnLocations[randomIndex].position);
        }
        else
        {
            transform.position = spawnLocations[randomIndex].position;
        }

        SetVisible(true);
    }

    private void SetVisible(bool visible)
    {
        foreach (var renderer in renderers) renderer.enabled = visible;
        foreach (var collider in colliders) collider.enabled = visible;
        
    }


}
