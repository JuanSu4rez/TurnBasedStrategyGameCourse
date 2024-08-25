using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GridSystem // NO MonoBehaviour since we need a constructor
{
    private int width;
    private int height;
    private float cellSize;

    private GridObject[,] gridObjectArray; 
    public GridSystem (int width, int height, float cellSize) {

        this.width = width;
        this.height = height;
        this.cellSize = cellSize;

        gridObjectArray = new GridObject[width, height];

        for (int x = 0; x < width; x++) {
            for (int z = 0; z < height; z++) {
                GridPosition gridPosition = new GridPosition(x, z);
                gridObjectArray[x,z] = new GridObject(this, gridPosition);

            }
        }
    }

    public Vector3 GetWorldPosition (GridPosition gridPosition) => new Vector3(gridPosition.x, 0, gridPosition.z) * cellSize;

    public GridPosition GetGridPosition (Vector3 worldPosition) =>
        new GridPosition
        (
            Mathf.RoundToInt(worldPosition.x / cellSize),
            Mathf.RoundToInt(worldPosition.z / cellSize)
        );

    public void CreateDebugObjects (Transform debugPrefab) {

        for (int x = 0; x < width; x++) {
            for (int z = 0; z < height; z++) {                 
                var gridPosition = new GridPosition (x, z);
                var debugObject = GameObject.Instantiate(debugPrefab, GetWorldPosition(gridPosition), Quaternion.identity);
                var gridDebugObject = debugObject.GetComponent<GridDebugObject>();
                gridDebugObject.SetGridObject(GetGridObject(gridPosition));


            }
        }

    }

    public GridObject GetGridObject (GridPosition gridPosition) {
        return gridObjectArray[gridPosition.x, gridPosition.z];
    }

    public bool IsValidGridPosition (GridPosition gridPosition) {
         return  gridPosition.x >= 0 &&
                 gridPosition.z >= 0 &&
                 gridPosition.x < width &&
                 gridPosition.z < height;
    }


    public int GetWidth () => width;
    public int GetHeight () => height;

}
