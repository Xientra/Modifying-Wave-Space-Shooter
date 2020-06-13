/*==================*\
|*   Unity Usings   *|
\*==================*/

using UnityEngine;

/*==========================*\
|*   CLASS: SpeedModifier   *|
\*==========================*/

public class SpeedModifier : MotionModifier
{
    /*=============================*\
    |*   Public Member Functions   *|
    \*=============================*/

        /*============*\
        |*   Setter   *|
        \*============*/

        public void SetAdditionalSpeed(float additionalSpeed) { m_additionalSpeed = additionalSpeed; }

    /*================================*\
    |*   Protected Member Functions   *|
    \*================================*/

        /*===============*\
        |*   Overrides   *|
        \*===============*/

        protected override Vector3 ComputeDirection() { return Vector3.forward * m_additionalSpeed; }
        protected override Vector3 ComputeJitter()    { return Vector3.zero; }
        protected override float ComputeRotation()    { return 0; }

    /*==============================*\
    |*   Private Member Variables   *|
    \*==============================*/

        /*==================*\
        |*   Input Memory   *|
        \*==================*/

        private float m_additionalSpeed = 2; 
}
