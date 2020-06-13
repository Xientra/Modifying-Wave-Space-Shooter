﻿/*===================*\
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

    /*==============================*\
    |*   Private Member Variables   *|
    \*==============================*/

        /*==================*\
        |*   Input Memory   *|
        \*==================*/

        private List<Modification> m_modifications = null;
}