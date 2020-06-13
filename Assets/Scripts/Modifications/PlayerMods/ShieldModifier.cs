/*===========================*\
|*   CLASS: ShieldModifier   *|
\*===========================*/

public class ShieldModifier : Modification
{
    /*=============================*\
    |*   Public Member Functions   *|
    \*=============================*/

        /*============*\
        |*   Setter   *|
        \*============*/

        public void SetShieldValue(int shieldValue) { m_shieldValue = shieldValue; }

        /*============*\
        |*   Getter   *|
        \*============*/

        public int GetShieldValue() { return m_shieldValue; }
        
    /*==============================*\
    |*   Private Member Variables   *|
    \*==============================*/

        /*==================*\
        |*   Input Memory   *|
        \*==================*/

        private int m_shieldValue = 1; 
}
