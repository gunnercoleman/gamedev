using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI pumpkinText;

    [Header("Level Complete")]
    [Tooltip("How many pumpkins the player must collect to finish the level.")]
    [SerializeField] int pumpkinsRequired = 10;
    [Tooltip("Panel shown when all pumpkins are collected. Starts hidden.")]
    [SerializeField] GameObject levelCompletePanel;

    int pumpkins;
    bool levelComplete;

    void Awake()
    {
        // Fallback: if nothing was dragged into the Inspector, look on this object
        if (pumpkinText == null)
        {
            pumpkinText = GetComponent<TextMeshProUGUI>();
        }
    }

    void Start()
    {
        if (levelCompletePanel != null)
        {
            levelCompletePanel.SetActive(false);
        }
        UpdateText();
    }

    public void IncreaseScore()
    {
        pumpkins++;
        UpdateText();

        if (!levelComplete && pumpkins >= pumpkinsRequired)
        {
            ShowLevelComplete();
        }
    }

    void UpdateText()
    {
        pumpkinText.text = "Pumpkins: " + pumpkins + " / " + pumpkinsRequired;
    }

    void ShowLevelComplete()
    {
        levelComplete = true;

        if (levelCompletePanel != null)
        {
            levelCompletePanel.SetActive(true);
        }

        // pause the game so the player stops moving behind the panel
        Time.timeScale = 0f;

        // the third person controller locks the cursor, so free it to click the button
        StarterAssets.StarterAssetsInputs input = FindFirstObjectByType<StarterAssets.StarterAssetsInputs>();
        if (input != null)
        {
            input.cursorLocked = false;
            input.cursorInputForLook = false;
        }
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}