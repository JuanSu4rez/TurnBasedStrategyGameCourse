using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAction : BaseAction
{
    public event EventHandler<OnShootEventArgs> OnShoot;

    public class OnShootEventArgs : EventArgs {
        public Unit targetUnit;
        public Unit shootingUnit;
    }

    private enum State { 
        Aiming, 
        Shooting,
        Cooloff
    }



    private int maxShootDistance = 7;
    private float stateTimer;
    private Unit targetUnit;
    private bool canShootBullet;

    private State state;

    // Update is called once per frame
    void Update () {
        if (!isActive)
            return;

        stateTimer -= Time.deltaTime;

        switch (state) {
            case State.Aiming:
                float rotationSpeed = 10f;
                Vector3 aimDirection = (targetUnit.GetWorldPosition() - unit.GetWorldPosition()).normalized;
                transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * rotationSpeed);
                
                break;
            case State.Shooting:
                if (canShootBullet) {
                    Shoot();
                    canShootBullet = false;
                }
                break;
            case State.Cooloff:
                break;
        }
        

        if(stateTimer <= 0f)
            NextState();
    }

    private void NextState () {

        switch (state) {
            case State.Aiming:
                state = State.Shooting;
                float shootingStateTime = 0.1f;
                stateTimer = shootingStateTime;
                break;
            case State.Shooting:
                state = State.Cooloff;
                float coolOffStateTime = 0.5f;
                stateTimer = coolOffStateTime;                    
                break;
            case State.Cooloff:
                ActionComplete();
                break;
            default:
                break;
        }
    }

    private void Shoot () {
        OnShoot?.Invoke(
            this, 
            new OnShootEventArgs { 
                targetUnit = targetUnit,
                shootingUnit = unit
            });
        targetUnit.Damage(40);
    }

    public override string GetActionName () {
        return "Shoot";
    }

    public override List<GridPosition> GetValidActionGridPositions () {
        var validpositions = new List<GridPosition>();
        var currentUnitGridPosition = unit.GetGridPosition();
        for (int x = -maxShootDistance; x < maxShootDistance; x++) {
            for (int z = -maxShootDistance; z < maxShootDistance; z++) {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = currentUnitGridPosition + offsetGridPosition;

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                    continue;

                int testDistance = Math.Abs(x) + Math.Abs(z);
                if (testDistance > maxShootDistance)
                    continue;

                if (LevelGrid.Instance.IsGridPositionEmpty(testGridPosition))
                    continue;

                Unit target = LevelGrid.Instance.GetUnitAtGridPosition(testGridPosition);

                if (target.IsEnemy() == unit.IsEnemy())
                    continue;
                
                validpositions.Add(testGridPosition);                
            }
        }
        return validpositions;
    }

    public override void TakeAction (GridPosition gridPosition, Action onActionComplete) {

        targetUnit = LevelGrid.Instance.GetUnitAtGridPosition(gridPosition);


        state = State.Aiming;
        float aimingStateTime = 1f;
        stateTimer = aimingStateTime;
        
        canShootBullet = true;
        ActionStart(onActionComplete);
    }

    public Unit GetTargetUnit () { 
        return targetUnit;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

}
