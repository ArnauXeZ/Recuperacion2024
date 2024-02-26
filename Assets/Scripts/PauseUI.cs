using UnityEngine;
using UnityEngine.UI;

// Class responsible for managing the pause menu UI
public class PauseUI : MonoBehaviour
{
    // Singleton instance of the PauseUI class
    public static PauseUI Instance { get; private set; }

    // References to UI buttons for resuming the game and returning to the main menu
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;

    // Awake method called when the script instance is being loaded
    private void Awake()
    {
        // Singleton pattern implementation
        if (Instance != null)
        {
            Debug.LogError("More than one Instance");
        }

        Instance = this; // Set the instance to this script

        // Add listener to the resume button to resume the game when clicked
        resumeButton.onClick.AddListener(() =>
        {
            GameManager.Instance.ResumeGame(); // Call the ResumeGame method of the GameManager
        });

        // Add listener to the main menu button to return to the main menu when clicked
        mainMenuButton.onClick.AddListener(() =>
        {
            Time.timeScale = 1f; // Reset the time scale to normal
            Loader.Load(Loader.Scene.MainMenu); // Load the main menu scene using the Loader
            GameManager.Instance.ResetAllValues(); // Reset all game values using the GameManager
        });

        // Hide the pause menu UI initially
        Hide();
    }

    // Method to show the pause menu UI
    public void Show()
    {
        gameObject.SetActive(true); // Set the game object of the pause menu UI to active
    }

    // Method to hide the pause menu UI
    public void Hide()
    {
        gameObject.SetActive(false); // Set the game object of the pause menu UI to inactive
    }
}

