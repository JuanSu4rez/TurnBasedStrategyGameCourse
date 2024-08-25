using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurnSystemUI : MonoBehaviour
{
    [SerializeField] private Button endTurnBtn;
    [SerializeField] private TextMeshProUGUI turnNumberText;
    [SerializeField] private GameObject enemyTurnVisualGameObject;

    private void Start () {
        endTurnBtn.onClick.AddListener(() => {
            TurnSystem.Instance.NextTurn();
        });

        TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;
        UpdateTurnText();
        UpdateEnemyTUrnVisual();
        UpdateEndTurnButtonVisivility();
    }

    private void TurnSystem_OnTurnChanged (object sender, EventArgs e) {
        UpdateTurnText();
        UpdateEnemyTUrnVisual();
        UpdateEndTurnButtonVisivility();
    }

    private void UpdateTurnText () {
        turnNumberText.text = $"TURN {TurnSystem.Instance.GetTurnNumber()}";
    }

    private void UpdateEnemyTUrnVisual () { 
        enemyTurnVisualGameObject.SetActive(!TurnSystem.Instance.IsPlayerTurn());
    }

    private void UpdateEndTurnButtonVisivility () {
        endTurnBtn.gameObject.SetActive(TurnSystem.Instance.IsPlayerTurn());
    }

}
