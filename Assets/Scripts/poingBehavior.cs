using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poingBehavior : StateMachineBehaviour
{
    private Transform playerTransform; // To hold the player's transform
    private float timer; // To keep track of the time
    private float speed = 5f; // Speed of the fist

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // Assuming your player has the tag "Player"
        timer = 0; // Reset the timer when the state starts
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime; // Increase the timer by the time passed since the last frame

        if (timer <= 15) // If 15 seconds have not passed yet
        {
            Vector2 targetPosition = new Vector2(animator.transform.position.x, playerTransform.position.y); // Target position is at the player's y-coordinate and the fist's initial x-coordinate
            animator.transform.position = Vector2.MoveTowards(animator.transform.position, targetPosition, speed * Time.deltaTime); // Move the boss's fist towards the target position
        }
        else
        {
            animator.SetTrigger("NextState"); // Transition to the next state
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // You can reset any parameters if needed
    }
}
