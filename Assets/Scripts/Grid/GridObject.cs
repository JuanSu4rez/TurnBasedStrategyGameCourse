﻿using System.Collections.Generic;
using UnityEngine.EventSystems;

public class GridObject {

    private GridSystem gridSystem;
    private GridPosition gridPosition;
    private List<Unit> units;

    public GridObject(GridSystem gridSystem, GridPosition gridPosition){
        this.gridSystem = gridSystem;
        this.gridPosition = gridPosition;
        units = new List<Unit>();
    }

    public override string ToString () {
        return gridPosition.ToString() + "\n" + units.Count;
    }

    public void AddUnit (Unit unit) { 
        units.Add(unit);
    }

    public void RemoveUnit (Unit unit) {
        units.Remove(unit);
    }

    public List<Unit> GetUnits () {
        return units;
    }

    public bool HasAnyUnit () { 
        return units.Count > 0;
    }

    public Unit GetUnit () { 
        if (HasAnyUnit())
            return units[0];
        return null;
    }
}
