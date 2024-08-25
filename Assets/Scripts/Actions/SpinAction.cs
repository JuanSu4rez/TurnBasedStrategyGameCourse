using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAction : BaseAction {
    private float totalSpinAmmount;

    void Start () {

    }

    // Update is called once per frame
    void Update () {
        if (!isActive)
            return;

        float spinAddAmount = 360f * Time.deltaTime;
        transform.eulerAngles += new Vector3(0, spinAddAmount, 0);
        totalSpinAmmount += spinAddAmount;
        if (totalSpinAmmount > 360f) {
            ActionComplete();
        }

    }

    public override void TakeAction (GridPosition gridPosition, Action onActionComplete) {
        totalSpinAmmount = 0f;
        
        ActionStart(onActionComplete);
    }

    public override string GetActionName () => "Spin";

    public override List<GridPosition> GetValidActionGridPositions () {
        var validpositions = new List<GridPosition>();
        GridPosition unitGridPosition = unit.GetGridPosition();
        return new List<GridPosition> { unitGridPosition };
    }

    public override int GetActionPointsCost () => 2;
}
