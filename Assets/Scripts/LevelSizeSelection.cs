using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Class responsible for managing level size selection
public class LevelSizeSelection : MonoBehaviour
{
    // Sliders for selecting width and height of the level
    public Slider widthSlider;
    public Slider heightSlider;

    // Minimum and maximum values for width and height sliders
    private int minWidth = 11;
    private int minHeight = 11;
    private int maxWidth = 41;
    private int maxHeight = 21;

    // Method to start the game with the selected level size
    public void StartGame()
    {
        int width = (int)widthSlider.value; // Get selected width value
        int height = (int)heightSlider.value; // Get selected height value

        // Check if the current scene is the level size selection scene
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            // Save the level size in PlayerPrefs
            PlayerPrefs.SetInt("LevelWidth", width);
            PlayerPrefs.SetInt("LevelHeight", height);
        }

        // Start the game with the selected level size
        Loader.Load(Loader.Scene.Game);
    }

    // Awake method called when the script instance is being loaded
    private void Awake()
    {
        // Set the minimum and maximum values for width and height sliders
        widthSlider.minValue = minWidth;
        widthSlider.maxValue = maxWidth;
        heightSlider.minValue = minHeight;
        heightSlider.maxValue = maxHeight;

        // Set the initial size of the sliders
        widthSlider.value = minWidth;
        heightSlider.value = minHeight;
    }
}


