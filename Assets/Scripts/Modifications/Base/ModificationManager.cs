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
    
    void FixedUpdate()
    {
        // Iterate over Modifications
        // --------------------------
        foreach(Modification mod in m_modifications) mod.ApplyModification();
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

    /*==============================*\
    |*   Private Member Variables   *|
    \*==============================*/

        /*==================*\
        |*   Input Memory   *|
        \*==================*/

        [SerializeField] private List<Modification> m_modifications = new List<Modification>();
}
