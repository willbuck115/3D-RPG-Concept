using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    private PlayerController player;
    
    // Have access to all the NPC classes here.
    private Lumberjack lumberJack;

    private void Start()
    {
        Application.targetFrameRate = 50;

        // Find all the classes
        player = FindObjectOfType<PlayerController>();
        lumberJack = FindObjectOfType<Lumberjack>();

        // Start Quest 1
        lumberJack.targetPosition = new Vector3(player.transform.position.x - 4, 1, 0);
        lumberJack.moving = true;


    }
}
