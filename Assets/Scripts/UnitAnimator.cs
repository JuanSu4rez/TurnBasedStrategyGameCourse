using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UnitAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Transform bulletProjectilePrefab;
    [SerializeField] private Transform ShootPoint;


    private readonly string IS_WALKING = "IsWalking";
    private readonly string SHOOT = "Shoot";

    private void Awake () {
        if (TryGetComponent<MoveAction>(out MoveAction moveAction)) {
            moveAction.OnStartMoving += MoveAction_OnStartMoving;
            moveAction.OnStopMoving += MoveAction_OnStopMoving;
        }

        if (TryGetComponent<ShootAction>(out ShootAction shootAction)) {
            shootAction.OnShoot += ShootAction_OnShoot;
        }
    }

    private void ShootAction_OnShoot (object sender, ShootAction.OnShootEventArgs e) {
        animator.SetTrigger(SHOOT);
        Transform bulletProjectileTransform = Instantiate(bulletProjectilePrefab, ShootPoint.position, Quaternion.identity);
        var bulletProjectile = bulletProjectileTransform.GetComponent<BulletProjectile>();

        var targetUnitShootAtPosition = e.targetUnit.GetWorldPosition();
        targetUnitShootAtPosition.y = ShootPoint.position.y;

        bulletProjectile.SetUp(targetUnitShootAtPosition);

    }

    private void MoveAction_OnStopMoving (object sender, System.EventArgs e) {
        animator.SetBool(IS_WALKING, false);
    }

    private void MoveAction_OnStartMoving (object sender, System.EventArgs e) {
        animator.SetBool(IS_WALKING, true);
    }
}
