using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public Quest[] quests;
    private string[] dialogue;
    private int line = 1;

    [SerializeField]
    private Lumberjack lumberjack;
    [SerializeField]
    private MapGenerator mapGenerator;

    public GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueBox;
    [SerializeField] private Button accept;
    [SerializeField] private Button decline;
    private bool waiting = false;

    public GameObject[] npcs;
    public string[] ids;

    [SerializeField]
    private GameObject questMarker, rewardMarker;

    public int QuestNo
    {
        get
        {
            return PlayerPrefs.GetInt("Quest");
        }
        set
        {
            PlayerPrefs.SetInt("Quest", PlayerPrefs.GetInt("Quest") + 1);
            SpawnRewardIcon(quests[PlayerPrefs.GetInt("Quest") - 1].givenBy);
        }
    }

    private void Start()
    {
        PlayerPrefs.SetInt("Quest",1);
        if (PlayerPrefs.GetInt("Quest") == 1)
        {
            // Set the lumberjack to head to the player.
            // Disable player movement.
            lumberjack.SetMovement(new Vector3(0, 1, 4));
            FindObjectOfType<PlayerController>().moveEnabled = false;
        }
        print(PlayerPrefs.GetInt("Quest"));
        SpawnQuestIcon(quests[PlayerPrefs.GetInt("Quest") - 1].givenBy);
    }

    private void Update()
    {
        // Waiting for player input
        if(waiting)
        {
            if(Input.GetMouseButtonDown(0))
            {
                StopAllCoroutines();
                if (dialogue[line].Contains("-"))
                {
                    line++;
                    StartCoroutine(DisplayDialogueLine());
                }
                else if (dialogue[line] == "***END OF INTERACTION***")
                {
                    line++;
                    waiting = false;
                    return;
                }
                else if (dialogue[line].Contains("***OPTION***"))
                {
                    if (PlayerPrefs.GetInt("Quest") == 1)
                    {
                        accept.gameObject.SetActive(true);
                    }
                    else
                    {
                        accept.gameObject.SetActive(true);
                        decline.gameObject.SetActive(true);
                    }
                }
                else if (dialogue[line].Contains("%ACCEPT"))
                {
                    waiting = false;
                    dialoguePanel.SetActive(false);
                    return;
                }
                else if (dialogue[line].Contains("%DECLINE"))
                {
                    print("decline");
                    waiting = false;
                    dialoguePanel.SetActive(false);
                    SpawnQuestIcon(quests[PlayerPrefs.GetInt("Quest") - 1].givenBy);
                    return;
                }
                else if (dialogue[line] == "***END OF FILE***")
                {
                    waiting = false;
                    dialoguePanel.SetActive(false);
                    quests[PlayerPrefs.GetInt("Quest") - 1].enabled = false;
                    PlayerPrefs.SetInt("Quest", PlayerPrefs.GetInt("Quest") + 1);
                    SpawnQuestIcon(quests[PlayerPrefs.GetInt("Quest") - 1].givenBy);
                }
                else
                {
                    StartCoroutine(DisplayDialogueLine());
                }
                print(dialogue[line]);
                waiting = false;
            }
        }
    }

    public void EnableQuest()
    {
        int quest = PlayerPrefs.GetInt("Quest") - 1;
        dialogue = quests[quest].GetDialogue();
        FindObjectOfType<PlayerController>().moveEnabled = false;

        StartCoroutine(DisplayDialogueLine());
    }
    public void Reward()
    {
        for(int i = 0; i < dialogue.Length; i++)
        {
            if(dialogue[i].Contains("Reward Phase"))
            {
                line = i + 2;
                dialoguePanel.SetActive(true);
                StartCoroutine(DisplayDialogueLine());
                break;
            }
        }
    }

    public void AcceptButton()
    {
        for(int i = 0; i < dialogue.Length; i++)
        {
            if(dialogue[i].Contains("%ACCEPT_"))
            {
                string[] x = dialogue[i].Split('_');
                quests[PlayerPrefs.GetInt("Quest") - 1].gameObject.SetActive(true);
                StartCoroutine(DisplayDialogueLine(x[1]));
            }
        }
        FindObjectOfType<PlayerController>().moveEnabled = true;
        accept.gameObject.SetActive(false);
        decline.gameObject.SetActive(false);
    }
    public void DeclineButton()
    {
        for (int i = 0; i < dialogue.Length; i++)
        {
            if (dialogue[i].Contains("%DECLINE_"))
            {
                string[] x = dialogue[i].Split('_');
                StartCoroutine(DisplayDialogueLine(x[1]));
            }
        }
        FindObjectOfType<PlayerController>().moveEnabled = true;
        accept.gameObject.SetActive(false);
        decline.gameObject.SetActive(false);
    }

    public void SpawnRewardIcon(string npc)
    {
        for(int i = 0; i < ids.Length; i++)
        {
            if(npc == ids[i])
            {
                Debug.Log("Spawning reward icon");
                Vector3 pos = new Vector3(npcs[i].transform.position.x, npcs[i].transform.position.y + 6f, npcs[i].transform.position.z);
                GameObject g = Instantiate(rewardMarker, pos, Quaternion.identity);
                g.transform.parent = npcs[i].transform;
            }
        }
    }
    public void SpawnQuestIcon(string npc)
    {
        for (int i = 0; i < ids.Length; i++)
        {
            if (npc == ids[i])
            {
                Debug.Log("Spawning quest icon");
                Vector3 pos = new Vector3(npcs[i].transform.position.x, npcs[i].transform.position.y + 6f, npcs[i].transform.position.z);
                GameObject g = Instantiate(questMarker, pos, Quaternion.identity);
                g.transform.parent = npcs[i].transform;
            }
        }
    }

    private IEnumerator DisplayDialogueLine(string op = null)
    {
        if (op == null)
        {
            dialogueBox.text = "";
            foreach (char c in dialogue[line])
            {
                yield return new WaitForSeconds(0.05f);
                dialogueBox.text += c;
            }
        }
        else
        {
            dialogueBox.text = "";
            foreach (char c in op)
            {
                yield return new WaitForSeconds(0.05f);
                dialogueBox.text += c;
            }
        }
        line++;
        waiting = true;
    }
}
