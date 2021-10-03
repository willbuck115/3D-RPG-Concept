using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Digging : MonoBehaviour
{

    private Vector3 currentPosition;
    public float overlapBox;

    private GameObject chest;
    public GameObject dirtPrefab;

    private const float increaseY = .45f;

    public void StartDig(Vector3 pos)
    {
        currentPosition = pos;
        Collider[] collisions = Physics.OverlapBox(transform.position, new Vector3(overlapBox, 5, overlapBox));

        foreach(Collider c in collisions)
        {
            if(c.CompareTag("Treasure"))
            {
                chest = c.gameObject;
                Dig(true);
                return;
            }
        }
        Dig(false);
    }

    private void StruckTreasure()
    {
        // Move chest to dig position
        float cPosY = currentPosition.y - 1.76f;
        cPosY += .8f;
        Vector3 cPos = new Vector3(currentPosition.x, cPosY, currentPosition.z);
        chest.transform.position = cPos;
        StartCoroutine(Counter(cPos, cPosY));
    }
    private void Dig(bool treasure)
    {
        if (GetComponent<PlayerController>().controller.isGrounded)
        {
            GetComponent<PlayerController>().moveEnabled = false;
            // Instantiate a dirt patch
            float posY = currentPosition.y + increaseY;
            Vector3 pos = new Vector3(currentPosition.x, posY, currentPosition.z);

            Instantiate(dirtPrefab, pos, Quaternion.identity);

            if (treasure)
            {
                StruckTreasure();
            }
        }
    }

    private IEnumerator Counter(Vector3 cPos, float cPosY)
    {
        yield return new WaitForSeconds(2);
        cPosY += 0.8f;
        cPos = new Vector3(currentPosition.x, cPosY, currentPosition.z);
        chest.transform.position = cPos;
        yield return new WaitForSeconds(2);
        cPosY += 0.4f;
        cPos = new Vector3(currentPosition.x, cPosY, currentPosition.z);
        chest.transform.position = cPos;
        GetComponent<PlayerController>().moveEnabled = true;
        //Done
    }
}
