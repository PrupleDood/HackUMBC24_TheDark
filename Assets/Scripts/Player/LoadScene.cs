using UnityEngine;
using UnityEngine.UI;  // Required for UI elements
using UnityEngine.SceneManagement;  // Required for scene management

public class LoadScene : MonoBehaviour
{
    // This function will be called when the button is pressed
    public float t;
    public bool loading;
    private int sceneToLoad;

    public void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }
    
    public void Update()
    {
        if (loading) t += Time.deltaTime;
        if (t >= 2f) SceneManager.LoadScene(sceneToLoad);
    }
    public void OnButtonPressed(int scene)
    {
        if (scene == 1) Cursor.lockState = CursorLockMode.Locked;
        // Load the specified scene
        loading = true;
        sceneToLoad = scene;
    }
}
