/*==================*\
|*   Unity Usings   *|
\*==================*/

using UnityEngine;

public class KillOnParticleDone: MonoBehaviour
{
    /*=====================*\
    |*   Unity Functions   *|
    \*=====================*/

    private void Start()
    {
        m_particleSystem.Play();
    }
    private void Update()
    {
        if (!m_particleSystem.isPlaying) Destroy(this); 
    }

    /*==============================*\
    |*   Private Member Variables   *|
    \*==============================*/

    /*==================*\
    |*   Input Memory   *|
    \*==================*/

    [SerializeField] private ParticleSystem m_particleSystem = null;
}
