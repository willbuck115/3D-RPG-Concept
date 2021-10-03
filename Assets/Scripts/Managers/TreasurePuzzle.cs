using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasurePuzzle : MonoBehaviour
{

    public GameObject treasurePuzzle;
    public GameObject pivotPoint;
    public GameObject innterTarget, middleTarget, outerTarget;
    private GameObject selectedX;
    private GameObject targetX;
    
    private float radiusOfBand;
    public float rotationSpeed;

    private void Update()
    {
        if(Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                if(hit.collider.name == "X")
                {
                    selectedX = hit.collider.gameObject;
                    if(selectedX.transform.parent.name == "Inner Circle")
                    {
                        targetX = innterTarget;
                    }
                    else if(selectedX.transform.parent.name == "Middle Circle")
                    {
                        targetX = middleTarget;
                    }
                    else
                    {
                        targetX = outerTarget;
                    }
                    hit.transform.RotateAround(pivotPoint.transform.position, Vector3.up, rotationSpeed * Time.deltaTime);
                }
                else if(selectedX != null)
                {
                    selectedX.transform.RotateAround(pivotPoint.transform.position, Vector3.up, rotationSpeed * Time.deltaTime);
                }
            }
        }
        else
        {
            selectedX = null;
        }
    }

}
