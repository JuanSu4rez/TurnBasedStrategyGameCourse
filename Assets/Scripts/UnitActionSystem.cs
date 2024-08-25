using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class UnitActionSystem : MonoBehaviour {
    public static UnitActionSystem instance { get; private set; }

    public event EventHandler OnSelectedUnitChanged;
    public event EventHandler OnSelectedActionChanged;
    public event EventHandler<bool> OnBusyChanged;
    public event EventHandler OnActionStarted;


    private BaseAction selectedAction;
    [SerializeField] private Unit selectedUnit;
    [SerializeField] private LayerMask unitLayerMask;
    private bool isBusy;

    private void Awake () {
        if (instance != null) {

            Debug.Log("More than one instance of UnitActionSystem");
            Destroy(gameObject);
        }
        instance = this;


    }

    // Start is called before the first frame update
    void Start () {
        SetSelectedUnit(selectedUnit);
    }

    // Update is called once per frame
    void Update () {

        if(isBusy)
            return;

        if(!TurnSystem.Instance.IsPlayerTurn())
            return;

        if (!EventSystem.current.IsPointerOverGameObject()) { 
            if (Input.GetMouseButtonDown(0)) {
                if (SelectUnit())
                    return;

                HandleSelectedAction();
            }
        
        }
    }

    private void HandleSelectedAction () {
        if (Input.GetMouseButtonDown(0)) {            

            GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetMousePosition());

            if (!selectedAction.IsValidActionGridPosition(mouseGridPosition))
                return;

            if (!selectedUnit.CanSpendActionPoints(selectedAction))  
                return;

            SetBusy();
            selectedUnit.SpendActionPoints(selectedAction.GetActionPointsCost());                
            selectedAction.TakeAction(mouseGridPosition, ClearBusy);
            OnActionStarted?.Invoke(this, EventArgs.Empty);
        }
    }

    public bool SelectUnit () {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, unitLayerMask)) {
            if (raycastHit.transform.TryGetComponent<Unit>(out Unit unit)) {
                if (unit.IsEnemy())
                    return false;
                if (unit == selectedUnit)
                    return false;
                SetSelectedUnit(unit);
                return true;
            }
        }
        return false;
    }

    private void SetSelectedUnit (Unit unit) {
        selectedUnit = unit;
        SetSelectedAction(unit.GetMoveAction());
        OnSelectedUnitChanged?.Invoke(this, new EventArgs());
    }

    public void SetSelectedAction (BaseAction baseAction) {
        selectedAction = baseAction;
        OnSelectedActionChanged?.Invoke(this, new EventArgs());   }

    public Unit GetSelectedUnit () => selectedUnit;

    public BaseAction GetSelectedAction () => selectedAction;

    private void SetBusy () { 
        isBusy = true;
        OnBusyChanged?.Invoke(this, isBusy);
    }
    private void ClearBusy() {
        isBusy = false;
        OnBusyChanged?.Invoke(this, isBusy);
    }

}
