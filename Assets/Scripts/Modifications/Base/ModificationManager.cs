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
            if (mod.IsEquipped()) { mod.ApplyModification(); }
        }
    }

    /*=============================*\
    |*   Public Member Functions   *|
    \*=============================*/

        /*============*\
        |*   Setter   *|
        \*============*/

        public void AddModification(Modification modification)   { m_modifications.Add(modification); }
        public void RemoveModiciation(Modification modification) { m_modifications.Remove(modification); }

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

    /*==============================*\
    |*   Private Member Variables   *|
    \*==============================*/

        /*==================*\
        |*   Input Memory   *|
        \*==================*/

        [SerializeField] private List<Modification> m_modifications = new List<Modification>();
}
