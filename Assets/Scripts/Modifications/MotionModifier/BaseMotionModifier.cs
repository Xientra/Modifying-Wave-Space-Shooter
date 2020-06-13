/*==================*\
|*   Unity Usings   *|
\*==================*/

using UnityEngine;

/*===============================*\
|*   CLASS: BaseMotionModifier   *|
\*===============================*/

public class BaseMotionModifier : MotionModifier
{
    /*================================*\
    |*   Protected Member Functions   *|
    \*================================*/

        /*===============*\
        |*   Overrides   *|
        \*===============*/

        protected override Vector3 ComputeDirection()   { return Vector3.forward; }
        protected override Vector3 ComputeJitter()      { return Vector3.zero; }
        protected override float ComputeRotation()      { return 0; }
}
