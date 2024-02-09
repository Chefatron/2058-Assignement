using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[ExecuteInEditMode]

public class GizmoHandler : MonoBehaviour
{

    [SerializeField] float radius;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
        Gizmos.DrawIcon(transform.position, "BuildSettings.Lumin@2x");
    }
}
