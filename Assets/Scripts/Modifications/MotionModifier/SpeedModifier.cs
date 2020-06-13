/*==========================*\
|*   CLASS: SpeedModifier   *|
\*==========================*/

public class SpeedModifier : Modification
{
    /*=============================*\
    |*   Public Member Functions   *|
    \*=============================*/

        /*============*\
        |*   Setter   *|
        \*============*/

        public void SetAdditionalSpeed(float additionalSpeed) { m_additionalSpeed = additionalSpeed; }

        /*============*\
        |*   Getter   *|
        \*============*/

        public float GetSpeed() { return m_additionalSpeed; }
        
    /*==============================*\
    |*   Private Member Variables   *|
    \*==============================*/

        /*==================*\
        |*   Input Memory   *|
        \*==================*/

        private float m_additionalSpeed = 2; 
}
