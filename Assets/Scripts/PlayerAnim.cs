using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    private Player player;
    private Animator animator;



    void Start()
    {
        player = GetComponent<Player>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        OnMove();

        onRun();
    }


    #region Movement

    void OnMove()
    {
        if (player.direction.sqrMagnitude > 0)
        {
            if(player.isRolling)
            {
                // Vai rolar
                animator.SetTrigger("isRoll");
            }
            else
            {   // Vai andar normalmente
                animator.SetInteger("transition", 1);
            }
            
        }
        else
        {
            animator.SetInteger("transition", 0);
        }

        if (player.direction.x > 0)
        {
            transform.eulerAngles = new Vector2(0, 0);
        }
        if (player.direction.x < 0)
        {
            transform.eulerAngles = new Vector2(0, 180);
        }

        if(player.IsCutting)
        {
            animator.SetInteger("transition", 3);
        }
    }

    void onRun()
    {
        if (player.isRunning)
        {
            animator.SetInteger("transition", 2);
        }
    }

    #endregion
}
