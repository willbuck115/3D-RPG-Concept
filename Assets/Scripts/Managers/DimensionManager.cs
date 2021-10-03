using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionManager : MonoBehaviour
{

    // True mode is the main dimension
    // False mode is the inverse fourth dimension
    private int mode;
    private bool overworldTransition = false;
    private bool underworldTransition = false;

    private ColorChanges color;

    [SerializeField]
    private float colorSpeed = 2.5f;
    private float lerpTime;
    private void Start()
    {
        mode = PlayerPrefs.GetInt("mode");
        color = gameObject.GetComponent<ColorChanges>();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            if (mode == 0 && !overworldTransition && !underworldTransition)
            { 
                overworldTransition = true;
            }
            else if(mode == 1  && !overworldTransition && !underworldTransition)
            {
                underworldTransition = true;
            }
        }
        
        if(overworldTransition)
        {
            lerpTime += colorSpeed;

            color.treeLeaves.color = Color.Lerp(color.liveLeaves, color.deadLeaves, lerpTime);
            color.treeTrunk.color = Color.Lerp(color.liveTrunk, color.deadTrunk, lerpTime); 
            color.grass.color = Color.Lerp(color.liveGrass, color.deadGrass, lerpTime);
            color.skin.color = Color.Lerp(color.normalSkin, color.zombiesSkin, lerpTime);
            color.shirt.color = Color.Lerp(color.normalShirt, color.zombieShirt, lerpTime);
            color.pants.color = Color.Lerp(color.normalPants, color.zombiePants, lerpTime);
            color.shoes.color = Color.Lerp(color.normalShoes, color.zombieShoes, lerpTime);

            if (lerpTime >= 1)
            {
                overworldTransition = false;
                //underworldTransition = true; // trailer purposes
                mode = 1;
                PlayerPrefs.SetInt("mode", mode);
                lerpTime = 0;
            }
        }
        else if(underworldTransition)
        {
            lerpTime += colorSpeed;

            color.treeLeaves.color = Color.Lerp(color.deadLeaves, color.liveLeaves, lerpTime);
            color.treeTrunk.color = Color.Lerp(color.deadTrunk, color.liveTrunk, lerpTime);
            color.grass.color = Color.Lerp(color.deadGrass, color.liveGrass, lerpTime);
            color.skin.color = Color.Lerp(color.zombiesSkin, color.normalSkin, lerpTime);
            color.shirt.color = Color.Lerp(color.zombieShirt, color.normalShirt, lerpTime);
            color.pants.color = Color.Lerp(color.zombiePants, color.normalPants, lerpTime);
            color.shoes.color = Color.Lerp(color.zombieShoes, color.normalShoes, lerpTime);

            if (lerpTime >= 1)
            {
                underworldTransition = false;
                //overworldTransition = true; // trailer purposes
                mode = 0;
                PlayerPrefs.SetInt("mode", mode);
                lerpTime = 0;
            }
        }
    }
}
