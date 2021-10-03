using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{

    [SerializeField] private QuestManager questManager;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                // Checks what the player clicked
                if (hit.collider.name.Contains("QuestMarker"))
                {
                    questManager.EnableQuest();
                    questManager.dialoguePanel.SetActive(true);
                    DestroyImmediate(hit.collider.gameObject);
                }
                else if (hit.collider.name.Contains("RewardMarker"))
                {
                    questManager.Reward();
                    questManager.dialoguePanel.SetActive(true);
                    DestroyImmediate(hit.collider.gameObject);
                }
                else if(hit.collider.name.Contains("ApplePrefab"))
                {
                    hit.collider.GetComponent<Rigidbody>().isKinematic = false;
                }
            }
        }
    }
}

