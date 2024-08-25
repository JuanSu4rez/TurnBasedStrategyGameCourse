using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoveAction : BaseAction
{

    public event EventHandler OnStartMoving;
    public event EventHandler OnStopMoving;
    private Vector3 targetPosition;
    

    private float toleranceToDistance = .1f;

    [SerializeField] private float movementSpeed = 4f;
    [SerializeField] private float rotationSpeed = 4f;    
    [SerializeField] private int maxMoveDistance = 4;

    protected override void Awake (){        
        base.Awake();
        targetPosition = transform.position;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive)
            return;
        MoveToTarget();
    }

    public void MoveToTarget () {
        if (Vector3.Distance(targetPosition, transform.position) > toleranceToDistance) {
            Vector3 movementDirection = (targetPosition - transform.position).normalized;   //Normalize since we only need direction not magnitude!
            transform.position += movementDirection * Time.deltaTime * movementSpeed;
            transform.forward = Vector3.Lerp(transform.forward, movementDirection, Time.deltaTime * rotationSpeed);
            return;
        }
        OnStopMoving?.Invoke(this, EventArgs.Empty);
        ActionComplete();
    }

    public override void TakeAction (GridPosition targetPosition, Action onActionComplete) {
        this.targetPosition = LevelGrid.Instance.GetWorldPosition(targetPosition);
        OnStartMoving?.Invoke(this, EventArgs.Empty);
        ActionStart(onActionComplete);
    }

  

    public override List<GridPosition> GetValidActionGridPositions () { 
        var validpositions = new List<GridPosition>();
        var currentUnitGridPosition = unit.GetGridPosition();
        for (int x = -maxMoveDistance; x < maxMoveDistance; x++) {
            for (int z = -maxMoveDistance; z < maxMoveDistance; z++) {
                GridPosition offsetGridPosition = new GridPosition(x, z);                
                GridPosition testGridPosition = currentUnitGridPosition + offsetGridPosition;                
                if (LevelGrid.Instance.IsValidGridPosition(testGridPosition)) {
                    if(currentUnitGridPosition != testGridPosition)
                        if (LevelGrid.Instance.IsGridPositionEmpty(testGridPosition)) {
                            validpositions.Add(testGridPosition);
                        }
                }

            }
        }        

        return validpositions;
    }

    public override string GetActionName () => "Move";
}
