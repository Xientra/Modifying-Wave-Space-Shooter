/*==================*\
|*   Unity Usings   *|
\*==================*/

using UnityEngine;

/*==================================*\
|*   CLASS: HommingMotionModifier   *|
\*==================================*/

public class HommingMotionModifier : MotionModifier
{
    /*=============================*\
    |*   Public Member Functions   *|
    \*=============================*/

        /*============*\
        |*   Setter   *|
        \*============*/

        public void SetDriftAngle(float driftAngle)           { m_driftAngle    = driftAngle; }
        public void SetHommingTarget(Transform hommingTarget) { m_hommingTarget = hommingTarget; }

    /*================================*\
    |*   Protected Member Functions   *|
    \*================================*/

        /*===============*\
        |*   Overrides   *|
        \*===============*/

        protected override Vector3 ComputeDirection() { return Vector3.zero; }
        protected override Vector3 ComputeJitter() { return Vector3.zero; }
        protected override float ComputeRotation() 
        {
            // Compute vecToTarget
            // -------------------
        	Vector3 vecToTarget = m_hommingTarget.position - m_modificationTarget.position;
            
            // Compute angle between forward and vecToTarget
            // ---------------------------------------------
            float angleToTarget = Vector3.Angle(m_modificationTarget.forward, vecToTarget);

            // Return min of Angles
            // --------------------
            return Mathf.Abs(angleToTarget) < m_driftAngle ? angleToTarget : (m_driftAngle * Mathf.Sign(angleToTarget)); 
        }

    /*==============================*\
    |*   Private Member Variables   *|
    \*==============================*/

        /*==================*\
        |*   Input Memory   *|
        \*==================*/

        [SerializeField] private float m_driftAngle         = 10;
        [SerializeField] private Transform m_hommingTarget  = null;
}

