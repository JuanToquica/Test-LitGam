using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeToMenu : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;

    private void OnEnable()
    {
        inputReader.escapeEvent += ReturnToMenu;
    }

    private void OnDisable()
    {
        inputReader.escapeEvent -= ReturnToMenu;
    }

    private void ReturnToMenu()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        SceneManager.LoadScene("MainMenu");
    }
}
