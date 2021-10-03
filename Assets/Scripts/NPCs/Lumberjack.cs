using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Lumberjack : NPCBase
{
    private float woodInventory;
    private bool findingTree;
    public GameObject targetTree;
    private GameObject targetObject;

    private void LateUpdate()
    {
        base.CheckMovement();
        if (findingTree)
        {
            if (transform.position == agent.destination)
            {
                animator.SetBool("Move", false);
                findingTree = false;
                ChopTree();
            }
        }
    }

    private void FindTree()
    {

        /*
        navMesh.BuildNavMesh();

        // Get all the trees within 80 units around
        Collider[] colliders = Physics.OverlapBox(transform.position, new Vector3(40, 3, 40));
        List<Collider> trees = new List<Collider>();

        foreach(Collider col in colliders)
        {
            if(col.tag == "Tree")
            {
                trees.Add(col);
            }
        }

        int randomTree = Random.Range(0, trees.Count);
        targetTree = trees[randomTree].gameObject;
        targetTree.GetComponent<NavMeshObstacle>().enabled = false;

        targetObject = new GameObject();
        targetObject.transform.position = targetTree.transform.position - new Vector3(1.3f, 0, 1.5f);
        targetObject.transform.LookAt(targetTree.transform);
        float randomRot = Random.Range(0, 360);
        targetObject.transform.RotateAround(targetTree.transform.position, Vector3.up, randomRot);

        agent.destination = targetObject.transform.position;

        findingTree = true;
        animator.SetBool("Move", true);

        */

        Collider[] colliders = Physics.OverlapBox(transform.position, new Vector3(40, 3, 4));
        List<Collider> trees = new List<Collider>();

        foreach(Collider c in colliders)
        {
            if(c.CompareTag("Tree"))
            {
                trees.Add(c);
            }
        }

        targetTree = trees[Random.Range(0, trees.Count)].gameObject;
        targetTree.GetComponent<NavMeshObstacle>().enabled = false;
    }
    private void ChopTree()
    {
        agent.enabled = false;
        transform.rotation = targetObject.transform.rotation; 
        animator.SetBool("ShouldChop", true);
    }
    public IEnumerator TreeCounter()
    {
        if(activeAI)
            woodInventory--;
        if (woodInventory < 1)
        {
            FindTree();
            yield return new WaitForEndOfFrame();
        }
        int r = Random.Range(1, 3);
        yield return new WaitForSeconds(r);
        TreeCounter();
    }
}
