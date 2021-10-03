using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleTree : MonoBehaviour
{
    [SerializeField] private List<GameObject> unmodifiedSpawns = new List<GameObject>();
    [SerializeField] private GameObject applePrefab;

    private List<GameObject> appleSpawn = new List<GameObject>();

    private void Start()
    {

        //StartCoroutine(GrowthTime());
        List<int> numbers = new List<int>();
        for(int x = 0; x < unmodifiedSpawns.Count; x++)
        {
            numbers.Add(x);
        }
        for(int i = 0; i < 5; i++)
        {
            int z = Random.Range(0, numbers.Count);
            numbers.Remove(z);
            GameObject a = Instantiate(applePrefab, unmodifiedSpawns[z].transform.position, Quaternion.identity);
            a.transform.SetParent(transform);
        }
    }

/*
    private void SpawnApple()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < 100)
        {
            appleSpawn.Clear();
            for (int i = 0; i < unmodifiedSpawns.Count; i++)
            {
                if (!unmodifiedSpawns[i].GetComponent<AppleSpawn>().AppleBoolean)
                {
                    appleSpawn.Add(unmodifiedSpawns[i]);
                }
            }
            if (appleSpawn.Count > 13)
            {
                int index = Random.Range(0, appleSpawn.Count);
                GameObject a = Instantiate(applePrefab, unmodifiedSpawns[index].transform.position, Quaternion.identity);
                unmodifiedSpawns[index].GetComponent<AppleSpawn>().AppleCreation(a);
                StartCoroutine(GrowthTime());
            }
        }
        else
        {
            InvokeRepeating("DistanceCheck", 10, 10);
        }
    }

    private IEnumerator GrowthTime()
    {
        yield return new WaitForSeconds(secondsTillGrow / growthMultipler);

        SpawnApple();
    }

    */
}
