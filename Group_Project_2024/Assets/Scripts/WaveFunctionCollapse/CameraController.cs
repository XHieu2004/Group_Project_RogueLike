using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera mainCamera;  // Reference to the default main camera
    public Camera wfcCamera;   // Reference to the WFC camera

    // Start is called before the first frame update
    void Start()
    {
        // Ensure only the main camera is active at the start of the game
        if (mainCamera != null)
            mainCamera.gameObject.SetActive(true);

        if (wfcCamera != null)
            wfcCamera.gameObject.SetActive(false);
    }

    // Function to activate the WFC camera (used before game starts or during WFC generation)
    public void EnableWFC()
    {
        if (mainCamera != null)
            mainCamera.gameObject.SetActive(false);  // Disable main camera

        if (wfcCamera != null)
            wfcCamera.gameObject.SetActive(true);  // Enable WFC camera
    }

    // Function to activate the main camera (used during normal gameplay)
    public void EnableMainCamera()
    {
        if (mainCamera != null)
            mainCamera.gameObject.SetActive(true);  // Enable main camera

        if (wfcCamera != null)
            wfcCamera.gameObject.SetActive(false);  // Disable WFC camera
    }
}
