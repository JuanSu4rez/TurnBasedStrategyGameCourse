using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelectedVisual : MonoBehaviour
{

    [SerializeField] private Unit unit;
    private MeshRenderer meshRenderer;


    private void Awake() {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Start()
    {
        UnitActionSystem.instance.OnSelectedUnitChanged += UnityActionSystem_OnSelectedUnitChanged;
        UpdateVisual();
    }

    private void UnityActionSystem_OnSelectedUnitChanged(object sender, System.EventArgs e) => UpdateVisual();
       
    void UpdateVisual() => meshRenderer.enabled = (UnitActionSystem.instance.GetSelectedUnit() == unit);
    
}
