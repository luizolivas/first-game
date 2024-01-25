using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public float speed;
    private float initalSpeed;
    private Animator anim;

    private int index;

    public List<Transform> paths = new List<Transform>();
    public DialogueSettings instance;

    private void Start()
    {
        initalSpeed = speed;
        anim = GetComponent<Animator>();
    }

    // Mover NPC
    void Update()
    {

        if (DialogueControl.instance.IsShowing)
        {
            speed = 0f;
            anim.SetBool("isWalking", false);
        }
        else
        {
            speed = initalSpeed;
            anim.SetBool("isWalking", true);
        }

        transform.position = Vector2.MoveTowards(transform.position, paths[index].position, speed * Time.deltaTime);

        if(Vector2.Distance(transform.position, paths[index].position) < 0.1f)
        {
            if(index < paths.Count - 1)
            {
                //andar seguindo a ordem de path
                //index++;


                // andar aletoriamente
                index = Random.Range(0, paths.Count - 1);
            }
            else
            {
                index = 0;
            }
        }

        // virar a direção do NPC
        Vector2 direction = paths[index].position - transform.position;
        if(direction.x > 0)
        {
            transform.eulerAngles = new Vector2(0,0);
        }
        if(direction.x < 0)
        {
            transform.eulerAngles = new Vector2(0, 180);
        }
    }
}
