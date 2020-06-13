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

        public ModificationObject GetModificationTarget() { return m_modificationTarget; }

        /*============*\
        |*   Setter   *|
        \*============*/

        public void SetRemovable(bool removeable)                                        { m_removeable = removeable; }
        public virtual void SetEquipped(bool equipped)                                   { m_isEquipped = equipped; }
        public virtual void SetModificationTarget(ModificationObject modificationTarget) { m_modificationTarget = modificationTarget; }
		public void SetPlayerMod(bool isPlayerMod) { m_isPlayerMod = isPlayerMod; }

        /*==============*\
        |*   Virtuals   *|
        \*==============*/

        public virtual void ApplyModification(){}

        /*================*\
        |*   Conditions   *|
        \*================*/

        public bool IsRemovable() { return m_removeable; }
        public bool IsEquipped() { return m_isEquipped; }
		public bool IsPlayerMod() { return m_isPlayerMod; }

    /*================================*\
    |*   Protected Member Variables   *|
    \*================================*/

        /*==================*\
        |*   Input Memory   *|
        \*==================*/

        [SerializeField] protected ModificationObject m_modificationTarget;

    /*==============================*\
    |*   Private Member Variables   *|
    \*==============================*/

        /*==================*\
        |*   Input Memory   *|
        \*==================*/

        [SerializeField] protected bool m_removeable = true;
        [SerializeField] protected bool m_isEquipped = true;
		[SerializeField] protected bool m_isPlayerMod = false;
}
