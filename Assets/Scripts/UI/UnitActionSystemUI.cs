using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UnitActionSystemUI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform actionButtonPrefab;
    [SerializeField] private Transform actionButtonContainer;
    [SerializeField] private TextMeshProUGUI actionPointsText;

    private List<ActionButtonUI> actionButtonsUIList;


    private void Awake () {
        actionButtonsUIList = new List<ActionButtonUI>();
    }


    void Start()
    {
        UnitActionSystem.instance.OnSelectedUnitChanged += UnitActionSystem_OnSelectedUnitChanged;
        UnitActionSystem.instance.OnSelectedActionChanged += UnitActionSystem_OnSelectedActionChanged;
        UnitActionSystem.instance.OnActionStarted += UnitActionSystem_OnActionStarted;
        //TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;
        Unit.OnAnyActionPointsChanged += Unit_OnAnyActionPointsChanged;

        CreateUnitActionButtons();
        UpdateSelectedVisual();
        UpdateActionPoints();
    }
    private void TurnSystem_OnTurnChanged (object sender, EventArgs e) {
        UpdateActionPoints ();
    }

    private void Unit_OnAnyActionPointsChanged (object sender, EventArgs e) {
        UpdateActionPoints ();
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateUnitActionButtons () {

        foreach (Transform button in actionButtonContainer) {
            Destroy(button.gameObject);
        }        
        actionButtonsUIList.Clear();

        Unit selectedUnit = UnitActionSystem.instance.GetSelectedUnit();

        foreach (BaseAction baseAction  in selectedUnit.GetBaseActions()) {
            Transform actionButtonTransform = Instantiate(actionButtonPrefab, actionButtonContainer);
            ActionButtonUI actionButtonUI = actionButtonTransform.GetComponent<ActionButtonUI>();
            actionButtonUI.SetBaseAction(baseAction);
            actionButtonsUIList.Add(actionButtonUI);
        }    
    }

    private void UpdateSelectedVisual () {
        foreach(ActionButtonUI button in actionButtonsUIList) 
            button.UpdateSelectedVisual();
    }

    private void UnitActionSystem_OnSelectedUnitChanged (object sender, EventArgs e) {
        CreateUnitActionButtons();
        UpdateSelectedVisual();
        UpdateActionPoints();
    }

    private void UnitActionSystem_OnSelectedActionChanged (object sender, EventArgs e) {
        UpdateSelectedVisual();
    }

    private void UpdateActionPoints () {
        var test = UnitActionSystem.instance.GetSelectedUnit().GetActionPoints();
        actionPointsText.text = $"Action Points: {test}";
    }
    private void UnitActionSystem_OnActionStarted (object sender, EventArgs e) {
        UpdateActionPoints();
    }
}
