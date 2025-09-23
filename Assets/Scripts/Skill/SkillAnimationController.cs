using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAnimationController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Get the animator and destroy the game object after animation completes
        Animator animator = GetComponent<Animator>();
        if (animator != null)
        {
            // Get the length of the current animation state
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            float animationLength = stateInfo.length;
            
            // Destroy the animation object after it completes
            Destroy(gameObject, animationLength);
        }
        else
        {
            // If no animator, destroy after a default time (e.g., 2 seconds)
           Destroy(gameObject, 2f);
        }
    }
}
