using UnityEditor;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public QuestManager manager;

    private string[] dialogue;
    public TextAsset dialogueFile;
    
    public int questID;
    public string type;
    public string givenBy;
    public string objective;
    public string description;
    public int numberOf;

    // Optional
    public GameObject collectablePrefab;
    public GameObject chestPrefab;

    public int currentNumber = 0;

    private void Start()
    {
        currentNumber = 0;
        SpawnAtRandom();
    }

    public string[] GetDialogue()
    {
        // Deserialize the dialogue file
        dialogue = LoadDialogue.GetDialogue(dialogueFile);

        return dialogue;
    }

    private void GoalEvaluation()
    {
        if (type.Contains("Collection"))
        {
            if (currentNumber >= numberOf)
            {
                print("completed");
                manager.SpawnRewardIcon(givenBy);
            }
        }
        if(type.Contains("Find"))
        {
            if (currentNumber >= numberOf)
            {
                print("completed");
                manager.SpawnRewardIcon(givenBy);
            }
        }
    }

    public void CollisionEvent(Collision collision)
    {
        if (collision.gameObject.name.Contains(collectablePrefab.name))
        {
            currentNumber = currentNumber + 1;
            print(currentNumber);
            GoalEvaluation();
        }
    }

    public void SpawnAtRandom()
    {
        if (chestPrefab != null)
        {
            float posX = Random.Range(30, -30f);
            float posZ = Random.Range(30, -30f);

            Vector3 pos = new Vector3(posX, 9f, posZ);

            Collider[] c = Physics.OverlapBox(pos, new Vector3(5f, 10, 5f));
            foreach(Collider col in c)
            {
                if(col.CompareTag("Tree"))
                {
                    SpawnAtRandom();
                    return;
                }
            }

            Instantiate(chestPrefab, pos, Quaternion.identity);
        }
    }
}
