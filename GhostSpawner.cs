using UnityEngine;
using Meta.XR.MRUtilityKit;
using UnityEngine.SceneManagement;
using System.Collections;

public class GhostSpawner : MonoBehaviour
{
    [Header("Time Between Spawning")]
    public float spawnTimer = 1f;
    public float bruteSpawnTimer = 3f;


    [Header("Prefabs")]
    public GameObject regGhost;
    public GameObject bruteGhost;

    [Header("Number To Spawn")]
    public int regGhostToSpawn = 100;
    public int bruteGhostToSpawn = 100;
      
    //timers
    private float timer; //reg ghost
    private float bruteTimer;
    private float decreaseTimer;


    [Header("Time Reduct. Values")]
    public float timeBeforeReduction = 60f;
    public float reduceTimeBy = 0.3f;
    public float stopReductionAt = 0.5f;

    [Header("Others")]
    public float minEdgeToDistance = 0.3f;
    public MRUKAnchor.SceneLabels spawnLabels;
    public float normalOffset = 0.1f;

    public bool reduceTime = false;
    public bool isRegularLevel = true;
    public bool isTCLevel = false;

    public GhostKillCounter GhostKillCounter;
    public Stopwatch stopwatch;

    private int spawnedGhost = 0;
    private int spawnedBruteGhost = 0;


    public void Update()
    {
        timer += Time.deltaTime;
        bruteTimer += Time.deltaTime;
        decreaseTimer += Time.deltaTime;

        if (timer > spawnTimer)
        {
            timer -= spawnTimer;
            SpawnGhost();
        }

        if (bruteTimer > bruteSpawnTimer)
        {
            bruteTimer -= bruteSpawnTimer;
            SpawnBruteGhost();
        }

        if (reduceTime && (decreaseTimer >= timeBeforeReduction) && (spawnTimer > stopReductionAt))
        {
            spawnTimer -= reduceTimeBy;
            bruteSpawnTimer -= reduceTimeBy;
            decreaseTimer = 0;
        }

        if ((GhostKillCounter.ghostKilled == (regGhostToSpawn + bruteGhostToSpawn)) && isRegularLevel)
        {
            GhostKillCounter.ResetGhostsKilled();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        if ((GhostKillCounter.ghostKilled >= (regGhostToSpawn + bruteGhostToSpawn)) && isTCLevel)
        {
            StartCoroutine(TCLevelCleared());
        }
    }


    public void SpawnGhost()
     {
         MRUKRoom room = MRUK.Instance.GetCurrentRoom();

         while (spawnedGhost <= regGhostToSpawn - 1)
         {
            bool hasFoundPosition = room.GenerateRandomPositionOnSurface(MRUK.SurfaceType.VERTICAL, minEdgeToDistance, LabelFilter.Included(spawnLabels), out Vector3 pos, out Vector3 norm);

            if (hasFoundPosition)
            {
                 Vector3 randomPositionNormalOffset = pos + norm * normalOffset;
                 randomPositionNormalOffset.y = 0;

                 Instantiate(regGhost, randomPositionNormalOffset, Quaternion.identity);

                 spawnedGhost++;

                 return;
            }
         }
     }

    public void SpawnBruteGhost()
    {
        MRUKRoom room = MRUK.Instance.GetCurrentRoom();

        while (spawnedBruteGhost <= bruteGhostToSpawn - 1)
        {
            bool hasFoundPosition = room.GenerateRandomPositionOnSurface(MRUK.SurfaceType.VERTICAL, minEdgeToDistance, LabelFilter.Included(spawnLabels), out Vector3 pos, out Vector3 norm);

            if (hasFoundPosition)
            {
                Vector3 randomPositionNormalOffset = pos + norm * normalOffset;
                randomPositionNormalOffset.y = 0;

                Instantiate(bruteGhost, randomPositionNormalOffset, Quaternion.identity);

                spawnedBruteGhost++;

                return;
            }
        }
    }

    IEnumerator TCLevelCleared()
    {
        stopwatch.StopStopwatch();
        yield return new WaitForSeconds(5);

        GhostKillCounter.ResetGhostsKilled();
        SceneManager.LoadScene(0); //back to start menu
    }

}
