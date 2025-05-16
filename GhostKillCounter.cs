using UnityEngine;

public class GhostKillCounter : MonoBehaviour
{
    public int ghostKilled = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ResetGhostsKilled();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetGhostsKilled()
    {
        ghostKilled = 0;
    }

}
