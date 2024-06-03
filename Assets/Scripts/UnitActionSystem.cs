using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitActionSystem : MonoBehaviour
{
    public static UnitActionSystem instance { get; private set; }

    public event EventHandler OnSelectedUnitChanged;

    [SerializeField] private Unit selectedUnit;
    [SerializeField] private LayerMask unitLayerMask;

    private void Awake() {
        if (instance != null) {

            Debug.Log("More than one instance of UnitActionSystem");
            Destroy(gameObject);
        }
        instance = this;


    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            if (SelectUnit())
                return;
            selectedUnit.SetTarget(MouseWorld.GetMousePosition());            
        }
    }

    public bool SelectUnit() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, unitLayerMask)) {
            if (raycastHit.transform.TryGetComponent<Unit>(out Unit unit)) { 
                SetSelectedUnit(unit);                
                return true;
            } 
        }
        return false;
    }

    private void SetSelectedUnit(Unit unit) {
        selectedUnit = unit;
        OnSelectedUnitChanged?.Invoke(this, new EventArgs());
    }

    public Unit GetSelectedUnit() => selectedUnit;

}
