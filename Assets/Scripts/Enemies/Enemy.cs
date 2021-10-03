using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int cooldown;
    public int damage;
    public PlayerManager player;

    private void Update()
    {
        
    }

    public virtual void OnAttack()
    {
        player.OnHit(damage);
    }
}
