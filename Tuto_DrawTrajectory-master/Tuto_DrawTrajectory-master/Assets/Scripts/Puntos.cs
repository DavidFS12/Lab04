using UnityEngine;
using UnityEngine.UIElements;

public class Puntos : MonoBehaviour
{
    private Label scoreLabel;
    private int score = 0;

    void OnEnable()
    {
        // Obtener el componente VisualElement del UI Document
        var root = GetComponent<UIDocument>().rootVisualElement;
        // Asignar el Label usando el nombre que le diste
        scoreLabel = root.Q<Label>("scoreLabel");
        // Actualizar el texto inicial
        UpdateScore();
    }

    public void IncreaseScore()
    {
        score++; // Aumentar el puntaje
        UpdateScore(); // Actualizar la UI
    }

    private void UpdateScore()
    {
        if (scoreLabel != null)
        {
            scoreLabel.text = "Score: " + score.ToString();
        }
        else
        {
            Debug.LogWarning("No se encontró el Label 'scoreLabel'. Asegúrate de que el nombre esté bien escrito.");
        }
    }
}
