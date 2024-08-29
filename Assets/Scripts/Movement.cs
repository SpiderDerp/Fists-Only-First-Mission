using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Animator animator;
    private float speed = .01f;

    private bool isAttacking = false;

    private int numClicks = 0;
    private float lastClickTime = 0;

    private float comboDelay = 1.2f;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("moving", false);
        
    }

    // Update is called once per frame
    void Update()
    {

        //Movement using WASD
        if (!isAttacking) {
            if (Input.GetKey(KeyCode.W))
            {
                transform.position += new Vector3(0, speed, 0);
                animator.SetBool("moving", true);
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.position += new Vector3(0, -1 * speed, 0);
                animator.SetBool("moving", true);
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.position += new Vector3(-1 * speed, 0, 0);
                animator.SetBool("moving", true);
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.position += new Vector3(speed, 0, 0);
                animator.SetBool("moving", true);
            }
        }

        if (Time.time - lastClickTime > comboDelay) {
            numClicks = 0;
            isAttacking = false;
        }

        if (Input.GetKey(KeyCode.P)) {
            Debug.Log("attacking");
            //player can attack upto three times
            isAttacking = true;
            lastClickTime = Time.time;
            numClicks++;
        

            if (numClicks == 1) {
                animator.SetTrigger("attack1");
            }

            numClicks = Mathf.Clamp(numClicks, 0, 3);
            
            
        }

        //If no key is pressed, the player is not moving
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            animator.SetBool("moving", false);
        }
        
    }

    public void ComboAttack1Transition() {
        if(numClicks >= 2)
            animator.SetTrigger("attack2");
    }
    public void ComboAttack2Transition() {
        if(numClicks >= 3)
            animator.SetTrigger("attack3");
    }
    
}
