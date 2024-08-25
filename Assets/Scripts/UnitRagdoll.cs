using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitRagdoll : MonoBehaviour
{
    [SerializeField] private Transform ragdollRootBone;

    public void Setup (Transform originallRootBone) {
        MatchAllChildTransforms(originallRootBone, ragdollRootBone);
        ApplyExplosion(ragdollRootBone, 300f, transform.position, 10f);
    }

    private void MatchAllChildTransforms (Transform root, Transform clone) {
        foreach (Transform child in root) {
            Transform cloneChild = clone.Find(child.name);
            if (cloneChild != null) { 
                cloneChild.position = child.position;
                cloneChild.rotation = child.rotation;

                MatchAllChildTransforms(child, cloneChild);
            }
        }
    }

    private void ApplyExplosion (Transform root, float explostionForce, Vector3 explostionPosition, float explosionRange) {
        foreach (Transform child in root) {


            if (child.TryGetComponent<Rigidbody> (out Rigidbody childRigidbody)) {
                childRigidbody.AddExplosionForce(explostionForce, explostionPosition, explosionRange);
            }

            ApplyExplosion(child, explostionForce, explostionPosition, explosionRange);
        }
    }
}