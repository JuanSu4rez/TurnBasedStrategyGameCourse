using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
   [SerializeField] private GameObject actionCameraGameObject;


    private void Start () {
        BaseAction.OnAnyActionStarted += BaseAction_OnAnyActionStarted;
        BaseAction.OnAnyActionCompleted += BaseAction_OnAnyActionCompleted;
        HideActionCamera();
    }

    private void BaseAction_OnAnyActionCompleted (object sender, System.EventArgs e) {
        switch (sender) {
            case ShootAction shootAction:
                HideActionCamera();
                break;
        }
    }

    private void BaseAction_OnAnyActionStarted (object sender, System.EventArgs e) {
        switch (sender) {
            case ShootAction shootAction:
                Unit shooter = shootAction.GetUnit();
                Unit target = shootAction.GetTargetUnit();

                Vector3 cameraCharacterHeight = Vector3.up * 1.7f;
                Vector3 shootDirection = (target.GetWorldPosition() - shooter.GetWorldPosition()).normalized;

                float shoulderOffSetAmount = 0.5f;
                Vector3 shoulderOffSet = Quaternion.Euler(0,90,0) * shootDirection * shoulderOffSetAmount;

                Vector3 actionCameraPosition = shooter.GetWorldPosition() + cameraCharacterHeight + shoulderOffSet + (shootDirection * -1);
                actionCameraGameObject.transform.position = actionCameraPosition;
                actionCameraGameObject.transform.LookAt(target.GetWorldPosition() + cameraCharacterHeight);

                ShowActionCamera();
                break;  
        }
    }

    private void ShowActionCamera () {
        actionCameraGameObject.SetActive(true);
    }



    private void HideActionCamera () {
        actionCameraGameObject.SetActive(false);
    }
}
