/*===================*\
|*   System Usings   *|
\*===================*/

using System.Collections.Generic;

/*==================*\
|*   Unity Usings   *|
\*==================*/

using UnityEngine;

/*==================================*\
|*   CLASS: HommingMotionModifier   *|
\*==================================*/

public class HommingMotionModifier : Modification
{
    /*=============================*\
    |*   Public Member Functions   *|
    \*=============================*/

        /*============*\
        |*   Setter   *|
        \*============*/

        public void SetDriftAngle(float driftAngle)           { m_driftAngle    = driftAngle; }
        public void SetHommingTarget(Transform hommingTarget) { m_hommingTarget = hommingTarget; }

        /*===============*\
        |*   Overrides   *|
        \*===============*/

        public override void ApplyModification()
        {
            // Early exit (No hommingTarget)
            // -----------------------------
            if (m_hommingTarget == null) return;

            // Compute vec to target
            // ---------------------
            Vector3 vecToTarget = m_hommingTarget.transform.position - m_modificationTarget.transform.position;
            
            Vector3 newDirection = Vector3.RotateTowards(m_modificationTarget.transform.forward, vecToTarget, m_driftAngle * Mathf.Deg2Rad * Time.deltaTime, 0.0f);
            
            // Rotate towards hommingTarget
            // ----------------------------
            m_modificationTarget.transform.transform.rotation = Quaternion.LookRotation(newDirection);
        }

    /*==============================*\
    |*   Private Member Variables   *|
    \*==============================*/

        /*==================*\
        |*   Input Memory   *|
        \*==================*/

        [SerializeField] private float m_driftAngle         = 180;
        [SerializeField] private Transform m_hommingTarget  = null;

        /*====================*\
        |*   Runtime memory   *|
        \*====================*/

        // private float dir = 1;
}

