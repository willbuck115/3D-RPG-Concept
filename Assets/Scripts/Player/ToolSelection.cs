using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolSelection : MonoBehaviour
{

    private int tool;    // Shovel - 1

    private Digging dig;

    private void Awake()
    {
        dig = GetComponent<Digging>();
    }

    public void ShovelButton()
    {
        tool = 1;
    }

    public void DetectorButton()
    {
        tool = 2;

        InvokeRepeating("Pulse", 1f, 1f);
    }

    private void GetShovel()
    {
        // Activate the shovel model.
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (tool == 1)
            {
                dig.StartDig(transform.position + transform.forward * 3);
            }
        }
    }

    private void Pulse()
    {
        // Checks for a chest within the area.
        Collider[] col = Physics.OverlapBox(transform.position, new Vector3(2.5f, 2.5f, 2.5f));

        foreach(Collider c in col)
        {
            if(c.CompareTag("Treasure"))
            {
                // Chest has been found start the puzzle

                CancelInvoke();
                return;
            }
        }


    }

}
