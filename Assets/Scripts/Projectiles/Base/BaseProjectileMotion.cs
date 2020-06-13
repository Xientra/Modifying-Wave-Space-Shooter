/*==================*\
|*   Unity Usings   *|
\*==================*/

using UnityEngine;

public class BaseMotionModifier : MotionModifier
{
    /*================================*\
    |*   Protected Member Functions   *|
    \*================================*/

        /*===============*\
        |*   Overrides   *|
        \*===============*/

        protected override Vector3 ComputeDirection()                     { return Vector3.forward; }
        protected override Vector3 ComputeJitter(Vector3 currentPosition) { return Vector3.zero; }
}
