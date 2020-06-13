/*===================*\
|*   System Usings   *|
\*===================*/

using System;

/*==================*\
|*   Unity Usings   *|
\*==================*/

using UnityEngine;

[Serializable]
public class Modification
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

        public void SetRemovable(bool removeable)                       { m_removeable = removeable; }
        public void SetEquipped(bool equipped)                          { m_isEquipped = equipped; }
        public void SetModificationTarget(Transform modificationTarget) { m_modificationTarget = modificationTarget; }

        /*==============*\
        |*   Virtuals   *|
        \*==============*/

        public virtual void ApplyModification(){}

        /*================*\
        |*   Conditions   *|
        \*================*/

        public bool IsRemovable() { return m_removeable; }
        public bool IsEquipped() { return m_isEquipped; }

    /*================================*\
    |*   Protected Member Variables   *|
    \*================================*/

        /*==================*\
        |*   Input Memory   *|
        \*==================*/

        [SerializeField] protected Transform m_modificationTarget;

    /*==============================*\
    |*   Private Member Variables   *|
    \*==============================*/

        /*==================*\
        |*   Input Memory   *|
        \*==================*/

        [SerializeField] private bool m_removeable = true;
        [SerializeField] private bool m_isEquipped = false;
}
