using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Unit : MonoBehaviour
{

    private readonly string IS_WALKING = "IsWalking";

    private Vector3 targetPosition;
    private float toleranceToDistance = .1f;
    [SerializeField] private Animator animator;
    [SerializeField] private float movementSpeed = 4f;
    [SerializeField] private float rotationSpeed = 4f;


    private void Awake() {
        targetPosition = transform.position;
    }

    private void Update() {
        MoveToTarget();
    }


    public void MoveToTarget() {
        if (Vector3.Distance(targetPosition, transform.position) > toleranceToDistance) {
            Vector3 movementDirection = (targetPosition - transform.position).normalized; //Normalize since we only need direction not magnitude!
            transform.position += movementDirection * Time.deltaTime * movementSpeed;
            transform.forward = Vector3.Lerp(transform.forward, movementDirection, Time.deltaTime * rotationSpeed);
            animator.SetBool(IS_WALKING, true);
            return;
        } 
        animator.SetBool(IS_WALKING, false);        
    }

    public void SetTarget(Vector3 targetPosition) => this.targetPosition = targetPosition;
    

}
