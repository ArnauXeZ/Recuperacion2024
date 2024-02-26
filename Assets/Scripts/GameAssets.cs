using System;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    // Serializable class to hold sound and audio clip pairs
    [Serializable]
    public class SoundAudioClip
    {
        public SoundManager.Sound sound; // Enum representing sound type
        public AudioClip audioClip; // Audio clip associated with the sound
    }

    // Singleton instance of the GameAssets class
    public static GameAssets Instance { get; private set; }

    // Sprites for snake head, body, and food
    public Sprite snakeHeadSprite;
    public Sprite snakeBodySprite;
    public Sprite foodSprite;

    // Array to store sound-audio clip pairs
    public SoundAudioClip[] soundAudioClipsArray;

    // Awake method called when the script instance is being loaded
    private void Awake()
    {
        // Singleton pattern: ensure there is only one instance of GameAssets
        if (Instance != null)
        {
            Debug.LogError("There is more than one Instance");
        }

        Instance = this; // Set the singleton instance to this object
    }
}

