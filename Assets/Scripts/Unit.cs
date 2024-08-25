using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Unit : MonoBehaviour
{

    public static event EventHandler OnAnyActionPointsChanged;
    [SerializeField] private bool isEnemy;

    private HealthSystem healthSystem;
    private const int MAX_ACTION_POINTS = 2;
    private GridPosition gridPosition;
    private MoveAction moveAction;
    private SpinAction spinAction;
    private BaseAction[] baseActionArray;
    private int actionPoints = MAX_ACTION_POINTS;

    // private float toleranceToDistance = .1f;

    /*[SerializeField] private float movementSpeed = 4f;
    [SerializeField] private float rotationSpeed = 4f;*/

    private void Awake () {
        healthSystem = GetComponent<HealthSystem>();
        moveAction = GetComponent<MoveAction>();
        spinAction = GetComponent<SpinAction>();
        baseActionArray = GetComponents<BaseAction>();
    }


    private void Start () {
        gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.AddUnitAtGridPosition(gridPosition, this);
        TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;
        healthSystem.OnDead += HealthSystem_OnDead;
    }

    private void HealthSystem_OnDead (object sender, EventArgs e) {
        LevelGrid.Instance.RemoveUnitAtGridPosition (gridPosition, this);
        Destroy(gameObject);
    }

    private void TurnSystem_OnTurnChanged (object sender, EventArgs e) {
        if((IsEnemy() && !TurnSystem.Instance.IsPlayerTurn()) || (!IsEnemy() && TurnSystem.Instance.IsPlayerTurn())) {
            actionPoints = MAX_ACTION_POINTS;
            OnAnyActionPointsChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    private void Update() {
        GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        if (gridPosition != newGridPosition) {
            LevelGrid.Instance.UnitMovedGridPosition(this, gridPosition, newGridPosition);
            gridPosition = newGridPosition;
        }
    }

    public bool CanSpendActionPoints (BaseAction baseAction) {
        return actionPoints >= baseAction.GetActionPointsCost();            
    }

    public void SpendActionPoints (int pointsToSpend) {
        actionPoints -= pointsToSpend;
        OnAnyActionPointsChanged?.Invoke(this, EventArgs.Empty);
    }


    public MoveAction GetMoveAction() => moveAction;
    public SpinAction GetSpinAction () => spinAction;

    public BaseAction[] GetBaseActions() => baseActionArray;
    public GridPosition GetGridPosition() => gridPosition;

    public Vector3 GetWorldPosition () => transform.position;

    public int GetActionPoints() => actionPoints;

    public bool IsEnemy () => isEnemy;

    public void Damage (int damageAmount) {
        healthSystem.Damage(damageAmount);
    }

}
