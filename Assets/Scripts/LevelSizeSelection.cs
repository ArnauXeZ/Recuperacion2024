using UnityEngine;
using UnityEngine.UI;

public class LevelSizeSelection : MonoBehaviour
{
    public Slider widthSlider;
    public Slider heightSlider;

    private int minWidth = 11;
    private int minHeight = 11;
    private int maxWidth = 41;
    private int maxHeight = 21;

    public void StartGame()
    {
        int width = (int)widthSlider.value;
        int height = (int)heightSlider.value;

        // Guardar el tamaño del nivel en PlayerPrefs
        PlayerPrefs.SetInt("LevelWidth", width);
        PlayerPrefs.SetInt("LevelHeight", height);

        // Iniciar el juego con el tamaño seleccionado
        Loader.Load(Loader.Scene.Game);
    }

    private void Awake()
    {
        // Configurar los límites de los deslizadores
        widthSlider.minValue = minWidth;
        widthSlider.maxValue = maxWidth;
        heightSlider.minValue = minHeight;
        heightSlider.maxValue = maxHeight;

        // Configurar el tamaño inicial de los deslizadores
        widthSlider.value = minWidth;
        heightSlider.value = minHeight;
    }
}

