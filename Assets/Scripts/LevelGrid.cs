using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{

    public static LevelGrid Instance { get; private set; }

    [SerializeField] private Transform debugObjectPrefab;

    private GridSystem gridSystem;
    private void Awake () {

        if (Instance != null) {

            Debug.LogError("There is more than one LevelGrid " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;

        gridSystem = new GridSystem(10, 10, 2);
        gridSystem.CreateDebugObjects(debugObjectPrefab);

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddUnitAtGridPosition (GridPosition gridPosition, Unit unit) {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        gridObject.GetUnits().Add(unit);
    }

    public List<Unit> GetUnitsAtGridPosition (GridPosition gridPosition) {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        return gridObject.GetUnits();
    }

    public void RemoveUnitAtGridPosition (GridPosition gridPosition, Unit unit) {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        gridObject.RemoveUnit(unit);
    }

    public void UnitMovedGridPosition (Unit unit, GridPosition from, GridPosition to) {
        RemoveUnitAtGridPosition(from, unit);
        AddUnitAtGridPosition(to, unit);
    }

    public GridPosition GetGridPosition (Vector3 worldPosition) => gridSystem.GetGridPosition(worldPosition);

    public Vector3 GetWorldPosition (GridPosition gridPosition) => gridSystem.GetWorldPosition(gridPosition);

    public bool IsValidGridPosition (GridPosition gridPosition) => gridSystem.IsValidGridPosition(gridPosition);

    public bool IsGridPositionEmpty (GridPosition gridPosition) { 
        GridObject gridObject = gridSystem.GetGridObject (gridPosition);
        return !gridObject.HasAnyUnit();
    }

    public Unit GetUnitAtGridPosition (GridPosition gridPosition) {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        return gridObject.GetUnit();
    }

    public int GetWidth () => gridSystem.GetWidth();

    public int GetHeight () => gridSystem.GetHeight();

}
