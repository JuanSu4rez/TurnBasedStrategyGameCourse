using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystemVisual : MonoBehaviour
{
    
    public static GridSystemVisual Instance { get; private set; }

    [SerializeField] private Transform gridSystemVisualSinglePrefab;

    private GridSystemVisualSingle[,] gridSystemVisualSingleArray;


    private void Awake () {
        if (Instance != null) {
            Debug.Log("There is more than one GridSystemVisual ");
            Destroy(gameObject);
            return;

        }

        Instance = this;
    }


    void Start()
    {
        gridSystemVisualSingleArray = new GridSystemVisualSingle[LevelGrid.Instance.GetWidth(), LevelGrid.Instance.GetHeight()];
        for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++) {
            for (int z = 0; z < LevelGrid.Instance.GetHeight(); z++) { 
                
                GridPosition gridPosition = new GridPosition(x, z);
                Transform gridSystemVisualSingleTransform = 
                    Instantiate(gridSystemVisualSinglePrefab, LevelGrid.Instance.GetWorldPosition(gridPosition), Quaternion.identity);
                gridSystemVisualSingleArray[x, z] = gridSystemVisualSingleTransform.GetComponent<GridSystemVisualSingle>();
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGridVisual();
    }

    public void HideAllGridPositions () {

        for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++) {
            for (int z = 0; z < LevelGrid.Instance.GetHeight(); z++) {
                gridSystemVisualSingleArray[x,z].HideVisual();
            }
        }
    }

    public void ShowGridPositionList (List<GridPosition> gridPositions) {

        foreach (GridPosition gridPosition in gridPositions) {
            gridSystemVisualSingleArray[gridPosition.x, gridPosition.z].ShowVisual();
        }

    }

    private void UpdateGridVisual () { 
        HideAllGridPositions();
        BaseAction selectedAction = UnitActionSystem.instance.GetSelectedAction();
        ShowGridPositionList(selectedAction.GetValidActionGridPositions());

    }
}
