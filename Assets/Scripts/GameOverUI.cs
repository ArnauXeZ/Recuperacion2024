using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Abstract class representing a vehicle
public abstract class Vehicle
{
    public string name;
}

// Class representing a car, inheriting from Vehicle
public class Car : Vehicle
{
    public string model;
}

// Class representing a bike, inheriting from Vehicle
public class Bike : Vehicle
{
    public int power;
}

// Class responsible for managing the Game Over UI
public class GameOverUI : MonoBehaviour
{
    // Singleton instance of the GameOverUI class
    public static GameOverUI Instance { get; private set; }

    // Text elements for displaying messages, score, and high score
    [SerializeField] private TextMeshProUGUI messsageText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;

    // Awake method called when the script instance is being loaded
    private void Awake()
    {
        // Creating an instance of Car, a subclass of Vehicle
        Vehicle v = new Car();

        // Singleton pattern: ensure there is only one instance of GameOverUI
        if (Instance != null)
        {
            Debug.LogError("More than one Instance");
        }

        Instance = this; // Set the singleton instance to this object

        Hide(); // Hide the Game Over UI by default
    }

    // Method to show the Game Over UI with updated information
    public void Show(bool hasNewHighScore)
    {
        UpdateScoreAndHighScore(hasNewHighScore); // Update score and high score
        gameObject.SetActive(true); // Set the UI game object to active
    }

    // Method to hide the Game Over UI
    public void Hide()
    {
        gameObject.SetActive(false); // Set the UI game object to inactive
    }

    // Method to update score and high score displayed on the UI
    private void UpdateScoreAndHighScore(bool hasNewHighScore)
    {
        // Set the text of score and high score elements
        scoreText.text = Score.GetScore().ToString(); // Get the current score
        highScoreText.text = Score.GetHighScore().ToString(); // Get the high score
        // Set the message text based on whether there is a new high score
        messsageText.text = hasNewHighScore ? "CONGRATULATIONS!" : "DON'T WORRY, NEXT TIME";
    }
}

