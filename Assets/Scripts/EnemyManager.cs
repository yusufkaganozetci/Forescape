using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class EnemyManager : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] GameObject wolf;
    [SerializeField] GameObject bear;
    [SerializeField] GameObject bees;
    //[SerializeField] GameObject crocodile;
    [SerializeField] Transform enemiesFolderTransform;
    [SerializeField] GameObject[] leftSidePaths;
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] Transform[] crocodiles;
    private int lastGeneratedCrocodileIndex;
    private Transform[] topSpawnPoints;
    private Transform[] bottomSpawnPoints;
    private Transform[] leftSpawnPoints;
    private Transform[] rightSpawnPoints;
    private int randomNum;

    private Dictionary<int, bool> crocodileVisitedDict = new Dictionary<int, bool>();

    private EnvironmentScroller environmentScroller;
    
    void Start()
    {
        environmentScroller = FindObjectOfType<EnvironmentScroller>();
        lastGeneratedCrocodileIndex = 26;
        topSpawnPoints = spawnPoints[0..3];
        bottomSpawnPoints = spawnPoints[3..6];
        leftSpawnPoints = spawnPoints[6..7];
        rightSpawnPoints = spawnPoints[7..8];
        for(int i = 0; i < crocodiles.Length; i++)
        {
            crocodileVisitedDict.Add(i, false);
        }
        StartCoroutine(CreateEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        HandleCrocodileGeneration();
    }

    private IEnumerator CreateEnemy()
    {
        while (true)
        {
            randomNum = Random.Range(0, 100);
            if (randomNum < 40)
            {
                CreateWolf();
            }
            else if(randomNum>=40 && randomNum<70)
            {
                CreateBear();
            }
            else
            {
                CreateBees();
            }
            
            yield return new WaitForSeconds(4);
        }
        
    }

    private void CreateBear()
    {
        int spawnIndex = Random.Range(0, spawnPoints.Length - 1);
        GameObject g =Instantiate(bear, spawnPoints[spawnIndex].position, Quaternion.identity);
        g.GetComponent<AIDestinationSetter>().target = player;

    }

    private void CreateWolf()
    {
        Transform[] indexes = HandleWolfSpawnPoints();
        //int spawnIndex = Random.Range(0, spawnPoints.Length - 1);
        GameObject g = Instantiate(wolf, indexes[0].position, Quaternion.identity);
        g.GetComponent<Wolf>().targetTransform = indexes[1];
        g.GetComponent<AIDestinationSetter>().target = indexes[1];
        
    }

    private void CreateBees()
    {
        int spawnIndex = Random.Range(0, spawnPoints.Length - 1);
        GameObject g = Instantiate(bees, spawnPoints[spawnIndex].position, Quaternion.identity);
        g.GetComponent<AIDestinationSetter>().target = player;
    }

    

    private void HandleCrocodileGeneration()
    {
        for (int i = 0; i < crocodiles.Length; i++)
        {
            if(crocodiles[i].position.x>player.position.x && 
                crocodiles[i].position.x - player.position.x <= 4 && !crocodileVisitedDict[i])
            {
                lastGeneratedCrocodileIndex = i;
                crocodileVisitedDict[i] = true;
                crocodiles[i].gameObject.SetActive(true);
                StartCoroutine(crocodiles[i].gameObject.GetComponent<Crocodile>().HandleCrocodileAnimation());
                break;
            }
            if (crocodiles[i].position.x <= -25 && crocodileVisitedDict[i]==true)
            {
                crocodileVisitedDict[i] = false;
            }
        }
    }
    private void DisplayLakes()
    {

    }
    private Transform[] HandleWolfSpawnPoints()
    {
        Transform[] instantiateIndexes;
        Transform[] targetIndexes;
        int firstInstantiateIndex = Random.Range(0, 3);
        switch (firstInstantiateIndex)
        {
            case 0:
                instantiateIndexes = topSpawnPoints;
                targetIndexes = bottomSpawnPoints;
                break;
            case 1:
                instantiateIndexes = bottomSpawnPoints;
                targetIndexes = topSpawnPoints;
                break;
            case 2:
                instantiateIndexes = leftSpawnPoints;
                targetIndexes = rightSpawnPoints;
                break;
            case 3:
                instantiateIndexes = rightSpawnPoints;
                targetIndexes = leftSpawnPoints;
                break;
            default:
                instantiateIndexes = new Transform[0];
                targetIndexes = new Transform[0];
                break;
        }
        Transform instantiatePoint = instantiateIndexes[Random.Range(0, instantiateIndexes.Length - 1)];
        Transform targetPoint = targetIndexes[Random.Range(0, targetIndexes.Length - 1)];
        return  new Transform[] { instantiatePoint, targetPoint };

    }
    /**private Transform[] ConvertChildsToList(GameObject g)
    {
        Transform[] waypointsList = new Transform[g.transform.childCount];
        for(int i = 0; i < g.transform.childCount; i++)
        {
            waypointsList[i] = g.transform.GetChild(i).transform;
        }
        return waypointsList;
    }**/
}
