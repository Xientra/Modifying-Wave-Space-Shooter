/*==================*\
|*   Unity Usings   *|
\*==================*/

using UnityEngine;

/*===========================*\
|*   CLASS: PlayerMovement   *|
\*===========================*/

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    /*=====================*\
    |*   Unity Functions   *|
    \*=====================*/

	private void Update()
	{
        // Get inputs
        // ----------
		Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		
        // Clamp direction to max 1
        // ------------------------
        input = Vector2.ClampMagnitude(input, 1f);

        // Compute velocity
        // ----------------
		velocity += new Vector3(input.x, 0, input.y) * Time.deltaTime * m_speed;

        // Convert mouseVector to 3D
        // -------------------------
		Vector3 mouse = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
		
        // VecTowards mouse
        // ----------------
        Vector3 lookTarget = cam.ScreenToWorldPoint(mouse);
        lookTarget.y       = transform.position.y;

        // Compute lookDirection
        // ---------------------
		lookDirection = Quaternion.LookRotation(lookTarget - transform.position);
	}
    private void FixedUpdate()
	{
		// Update velocity
        // ---------------
        velocity = velocity * (1 - Time.fixedDeltaTime * drag);
		velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
		
        // Apply velocity
        // --------------
        rbody.velocity = velocity;

        // Apply move rotation
        // -------------------
		rbody.MoveRotation(lookDirection);
	}

    /*==============================*\
    |*   Private Member Variables   *|
    \*==============================*/

        /*==================*\
        |*   Input Memory   *|
        \*==================*/

        [SerializeField] private Rigidbody rbody    = null;
	    [SerializeField] private Camera cam         = null;
	    [SerializeField] private ModificationManager m_modificationManager = null;

        [SerializeField] private float m_speed      = 10;
	    [SerializeField] private float maxSpeed     = 10;
	    [SerializeField] private float drag         = 1;
        
        /*====================*\
        |*   Runtime memory   *|
        \*====================*/

        private Vector3 velocity;
	    private Quaternion lookDirection;

    /*==============================*\
    |*   Private Member Functions   *|
    \*==============================*/

        /*=================*\
        |*   Auxiliaries   *|
        \*=================*/

        private float CollectSpeedModifiers()
        {
            // Define return value
            // -------------------
            float finalSpeed = m_speed;

            // Iterate over ModificationManager
            // --------------------------------
            foreach(Modification modification in m_modificationManager.GetModifications())
            {
                // Skip (No SpeedModifier)
                // -----------------------
                if(!(modification is SpeedModifier)) continue;

                // Multiply speedModifier with finalSpeed
                // --------------------------------------
                finalSpeed *= (modification as SpeedModifier).GetSpeed();
            }

            // Return final speed
            // ------------------
            return finalSpeed;
        }
}
