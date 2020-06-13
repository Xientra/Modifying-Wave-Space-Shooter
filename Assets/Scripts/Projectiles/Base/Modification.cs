/*==================*\
|*   Unity Usings   *|
\*==================*/

using UnityEngine;

public abstract class Modification
{
    /*=============================*\
    |*   Public Member Functions   *|
    \*=============================*/

        /*============*\
        |*   Getter   *|
        \*============*/

        public Transform GetModificationTarget() { return m_modificationTarget; }

        /*============*\
        |*   Setter   *|
        \*============*/

        public void SetRemovable(bool removeable) { m_removeable = removeable; }
        public void SetEquipped(bool equipped)    { m_isEquipped = equipped; }

        /*===============*\
        |*   Abstracts   *|
        \*===============*/

        public abstract void ApplyModification();

        /*================*\
        |*   Conditions   *|
        \*================*/

        public bool IsRemovable() { return m_removeable; }
        public bool IsEquipped() { return m_isEquipped; }

    /*==============================*\
    |*   Private Member Variables   *|
    \*==============================*/

        /*==================*\
        |*   Input Memory   *|
        \*==================*/

        [SerializeField] private bool m_removeable;
        [SerializeField] private bool m_isEquipped;
        [SerializeField] private Transform m_modificationTarget;
}
