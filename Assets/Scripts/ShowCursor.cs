using UnityEngine;

public class ShowCursor : MonoBehaviour {

	public bool showCursor = true;

	void Start(){
		Cursor.visible = showCursor;
		Cursor.lockState = showCursor ? CursorLockMode.Confined : CursorLockMode.Locked;
	}
}
