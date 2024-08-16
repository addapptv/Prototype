using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorStateControl : MonoBehaviour
{
    Animator animator;
    Vector2 motion;
    CharacterController controller;

    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        motion = controller.velocity;
        if (motion.magnitude > 0.2)
        {
            animator.SetBool("isWalking", true);
        }

        if (motion.magnitude < 0.2)
        {
            animator.SetBool("isWalking", false);
        }
    }

}
