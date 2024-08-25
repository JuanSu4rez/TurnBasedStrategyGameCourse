using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ActionButtonUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private Button button;
    [SerializeField] private GameObject selectedGameObject;

    private BaseAction BaseAction;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetBaseAction (BaseAction baseAction) { 

        this.BaseAction = baseAction;
        textMeshPro.text = baseAction.GetActionName().ToUpper();
        button.onClick.AddListener(() => { 
            UnitActionSystem.instance.SetSelectedAction(baseAction);
        });
    }

    public void UpdateSelectedVisual () { 
        BaseAction selectedAction = UnitActionSystem.instance.GetSelectedAction();
        selectedGameObject.SetActive(selectedAction == BaseAction);
    }
}

