using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject instructions;
    [SerializeField]
    private GameObject menu;
    [SerializeField]
    private GameObject logo;

    public void PlayGame()
    {
        /*StartCoroutine("ShowInstructions");*/
        SceneManager.LoadScene(1);
    }

    public void QuitGame() 
    {
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
	}

    public void Restart() 
    {
        SceneManager.LoadScene(1);
    }

    public void ShowInstructions() 
    {
        menu.SetActive(false);
        logo.SetActive(false);
        instructions.SetActive(true);
    }

    /*IEnumerator ShowInstructions() 
    {
        menu.SetActive(false);
        instructions.SetActive(true);
        yield return new WaitForSeconds(500.0f);
    }*/
}
