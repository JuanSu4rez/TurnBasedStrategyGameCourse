using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GridDebugObject : MonoBehaviour
{
    [SerializeField] private TextMeshPro textMeshPro;
    private GridObject _gridObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        textMeshPro.text = _gridObject.ToString();
    }

    public void SetGridObject (GridObject gridObject) {
        _gridObject = gridObject;

    }
}
