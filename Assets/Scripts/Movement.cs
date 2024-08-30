using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Animator animator;
    private float speed = .01f;

    private bool isAttacking;
    private int numClicks = 0;
    private float lastClickTime = 0;
    private float comboDelay = 0.8f;
    private float nextFireTime = 0f;

    private float cooldownTime = 0.8f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("moving", false);
        isAttacking = false;    
        
    }

    // Update is called once per frame
    void Update()
    {   

        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("attack1")) {
            animator.SetBool("attack1", false);
        }

        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("attack2")) {
            animator.SetBool("attack2", false);
        }

        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("attack3")) {
            animator.SetBool("attack3", false);
            numClicks = 0;
        }

        if (Time.time - lastClickTime > comboDelay) {
            numClicks = 0;
        }

        if (Time.time > nextFireTime) {
            if (Input.GetKeyDown(KeyCode.P)) {
                OnAttack();
            }
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("attack1") || animator.GetCurrentAnimatorStateInfo(0).IsName("attack2") || animator.GetCurrentAnimatorStateInfo(0).IsName("attack3")) {
            isAttacking = true;
        } else {
            isAttacking = false;
        }

        if (isAttacking) {
            animator.SetBool("moving", false);
        }

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

        //If no key is pressed, the player is not moving
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            animator.SetBool("moving", false);
        }
        
    }

    void OnAttack() {
        isAttacking = true;
        lastClickTime = Time.time;
        numClicks++;

        numClicks = Mathf.Clamp(numClicks, 0, 3);   

        if (numClicks == 1) {
            animator.SetBool("attack1", true);
        }


        if (numClicks >= 2 && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("attack1")) {
            animator.SetBool("attack1", false);
            animator.SetBool("attack2", true);
        }

        if (numClicks >= 3 && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("attack2")) {
            animator.SetBool("attack2", false);
            animator.SetBool("attack3", true);
        }
    }

    IEnumerator Wait(float seconds) {
        yield return new WaitForSeconds(seconds);
    }
}
