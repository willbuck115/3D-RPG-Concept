using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    private int health;
    private float attackCooldown;

    private Animator animatior;

    private GameStates state;

    public int Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
            if(health <= 0)
            {
                OnDeath();
            }
        }
    }

    public void OnHit(int damage)
    {
        Health = health - damage;
    }

    private void OnDeath()
    {
        // Game Over
        // Play death animation
        // End the game and load the game over screen.
        //animator.SetBoot("Death", true)
        state.DeathState();
    }
}
