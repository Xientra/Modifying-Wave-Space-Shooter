/*==================*\
|*   Unity Usings   *|
\*==================*/

using UnityEngine;

/*===============================*\
|*   CLASS: ModificationObject   *|
\*===============================*/

public abstract class ModificationObject : MonoBehaviour
{
    /*=============================*\
    |*   Public Member Functions   *|
    \*=============================*/

        /*============*\
        |*   Getter   *|
        \*============*/

        public ModificationManager GetModificationManager() { return m_modificationManager; }
        
    /*================================*\
    |*   Protected Member Variables   *|
    \*================================*/

        /*==================*\
        |*   Input Memory   *|
        \*==================*/

        [SerializeField] protected ModificationManager m_modificationManager = null;
}
