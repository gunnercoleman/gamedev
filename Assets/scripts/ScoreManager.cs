using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    TextMeshProUGUI pumpkinText;
    int pumpkins;

    void Awake()
    {
        pumpkinText = GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        UpdateText();
    }

    public void IncreaseScore()
    {
        pumpkins++;
        UpdateText();
    }

    void UpdateText()
    {
        pumpkinText.text = "Pumpkins: " + pumpkins;
    }
}