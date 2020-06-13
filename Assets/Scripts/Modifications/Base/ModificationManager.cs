/*===================*\
|*   System Usings   *|
\*===================*/

using System.Collections.Generic;

/*==================*\
|*   Unity Usings   *|
\*==================*/

using UnityEngine;

/*================================*\
|*   CLASS: ModificationManager   *|
\*================================*/

public class ModificationManager : MonoBehaviour
{
    /*=====================*\
    |*   Unity Functions   *|
    \*=====================*/
    
    void Update()
    {
        // Iterate over Modifications
        // --------------------------
        foreach (Modification mod in m_modifications)
        {
            // Set target
            // ----------
            mod.SetModificationTarget(m_modificationObject);

            // Skip (Mod not equipped)
            // -----------------------
            if (!mod.IsEquipped()) continue;

            // Apply mod
            // ---------
            if      (m_modificationObject is Player     &&  mod.IsPlayerMod()) mod.ApplyModification();
            else if (m_modificationObject is Projectile && !mod.IsPlayerMod()) mod.ApplyModification();
        }
    }

	/*=============================*\
    |*   Public Member Functions   *|
    \*=============================*/

	/*===============*\
	|*   Delegates   *|
	\*===============*/

		public delegate void ModAdded(Modification mod);
		public delegate void ModRemoved(Modification mod);

	/*============*\
	|*   Setter   *|
	\*============*/

		public void AddModification(Modification modification)   
        {
            modification.SetModificationTarget(m_modificationObject);
            m_modifications.Add(modification);
			modAdded?.Invoke(modification);
        }
        public void RemoveModiciation(Modification modification) {
			m_modifications.Remove(modification);
			modRemoved?.Invoke(modification);
		}

        /*============*\
        |*   Getter   *|
        \*============*/

        public List<Modification> GetModifications() { return m_modifications; }

        /*===============*\
        |*   Utilities   *|
        \*===============*/

        public List<TModifier> CollectModifiers<TModifier>() where TModifier : Modification
        {
            // Define return value
            // -------------------
            List<TModifier> modifiers = new List<TModifier>();

            // Iterate over ModificationManager
            // --------------------------------
            foreach(Modification modification in m_modifications)
            {
                // Skip (No SpeedModifier)
                // -----------------------
                if(!(modification is TModifier)) continue;

                // Add modifier
                // ------------
                modifiers.Add(modification as TModifier);
            }

            // Return final speed
            // ------------------
            return modifiers;
        }
	/*=============================*\
    |*   Public Member Variables   *|
    \*=============================*/

	/*==================*\
	|*   Input Memory   *|
	\*==================*/

		public ModAdded modAdded = null;
		public ModRemoved modRemoved = null;

	/*==============================*\
    |*   Private Member Variables   *|
    \*==============================*/

	/*==================*\
	|*   Input Memory   *|
	\*==================*/

	[SerializeField] private List<Modification> m_modifications      = new List<Modification>();
        [SerializeField] private ModificationObject m_modificationObject = null; 
}
