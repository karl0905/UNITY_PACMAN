using UnityEngine;
using TMPro; // Import TextMeshPro namespace
using System.Collections;

public class GameManager : MonoBehaviour
{
    // Static instance to enable easy access from other scripts
    public static GameManager Instance { get; private set; }

    // Score tracking
    [SerializeField] private int score = 0;
    private TextMeshProUGUI scoreText; // Using TMP instead of regular Text

    // Optional: Track high score
    [SerializeField] private int highScore = 0;
    private TextMeshProUGUI highScoreText; // Using TMP instead of regular Text
    
    // Called when the script instance is being loaded
    private void Awake()
    {
        // Singleton pattern - ensure only one GameManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: Keep game manager between scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("GameManager started!");
        
        // Find TextMeshPro components automatically
        FindScoreText();
        
        // Initialize score display
        UpdateScoreDisplay();
        
        // Optional: Load high score from player preferences
        LoadHighScore();
    }
    
    // Find TextMeshPro texts in the scene
    private void FindScoreText()
    {
        // Find all TextMeshProUGUI components in the scene
        TextMeshProUGUI[] allTexts = FindObjectsOfType<TextMeshProUGUI>();
        
        Debug.Log("Found " + allTexts.Length + " TextMeshProUGUI components in the scene");
        
        // Look for a score text
        foreach (TextMeshProUGUI tmp in allTexts)
        {
            Debug.Log("Found TextMeshProUGUI: " + tmp.name + " with text: " + tmp.text);
            
            // If it's named ScoreText or contains "Score" in its text
            if (tmp.name.Contains("Score") || tmp.text.Contains("Score"))
            {
                scoreText = tmp;
                Debug.Log("Found and assigned score text: " + tmp.name);
                break;
            }
        }
        
        // If still not found, try to create one
        if (scoreText == null)
        {
            Debug.LogWarning("No score text found in scene, game will work but score won't display");
        }
    }

    // Add points to the score
    public void AddPoints(int points)
    {
        score += points;
        Debug.Log("Score updated: " + score); // Debug log
        UpdateScoreDisplay();
        
        // Optional: Check for new high score
        if (score > highScore)
        {
            highScore = score;
            SaveHighScore();
            
            if (highScoreText != null)
            {
                highScoreText.text = "High Score: " + highScore;
            }
        }
    }

    // Update the score UI
    private void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
            Debug.Log("Updated score display to: " + score);
        }
        else
        {
            Debug.LogWarning("scoreText is still null after attempts to find it");
        }
    }

    // Optional: Reset score (for game over or new game)
    public void ResetScore()
    {
        score = 0;
        UpdateScoreDisplay();
    }
    
    // Optional: Load high score from player preferences
    private void LoadHighScore()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        
        if (highScoreText != null)
        {
            highScoreText.text = "High Score: " + highScore;
        }
    }
    
    // Optional: Save high score to player preferences
    private void SaveHighScore()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.Save();
    }
}
