/*==================*\
|*   Unity Usings   *|
\*==================*/

using UnityEngine;

public abstract class MotionModifier : Modification
{
    /*=====================*\
    |*   Unity Functions   *|
    \*=====================*/

    private void Awake() 
    {
        // Define runtime
        // --------------
        m_formerPosition = m_modificationTarget.position;
    }

    /*=============================*\
    |*   Public Member Functions   *|
    \*=============================*/

        /*============*\
        |*   Setter   *|
        \*============*/

        public void SetSpeed(float speed)                   { m_speed = speed; }
        public void SetJitterStrength(float jitterStrength) { m_jitterStrength = jitterStrength; }

        /*===============*\
        |*   Overrides   *|
        \*===============*/

        public override void ApplyModification()
        {
            // Apply Transformation and rotation modifications
            // -----------------------------------------------
            m_modificationTarget.Translate(ComputeDirection() * m_speed, Space.Self);
            m_modificationTarget.Rotate(Vector3.up, ComputeRotation() * m_speed, Space.Self);
            m_modificationTarget.Translate(ComputeJitter() * m_jitterStrength, Space.Self);
        }

    /*================================*\
    |*   Protected Member Variables   *|
    \*================================*/

        /*==================*\
        |*   Input Memory   *|
        \*==================*/
        
        [SerializeField] protected float m_speed            = 1;
        [SerializeField] protected float m_jitterStrength   = 1;

        /*====================*\
        |*   Runtime memory   *|
        \*====================*/

        private Vector3 m_formerPosition = default;
        
    /*================================*\
    |*   Protected Member Functions   *|
    \*================================*/

        /*===============*\
        |*   Abstracts   *|
        \*===============*/

        protected abstract Vector3 ComputeDirection();
        protected abstract float ComputeRotation();

        protected abstract Vector3 ComputeJitter();
}
