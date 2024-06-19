using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;

public class GameManager : MonoBehaviour
{
   public ARPlaneManager planeManager;
    [SerializeField] TextMeshProUGUI highScore;
    [SerializeField] TextMeshProUGUI score;
    int hScore;

    public static Action<int> UpdateScore;

    private void OnEnable()
    {
        UpdateScore += ScoreUpdater;
        if (!PlayerPrefs.HasKey("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", 0);
        }
    }
    private void OnDisable()
    {
        UpdateScore-= ScoreUpdater;
    }
    private void Start()
    {
        hScore = PlayerPrefs.GetInt("HighScore");
        highScore.text = "High Score : " + hScore.ToString();
    }

    public void RestartScene()
    {
        planeManager.gameObject.SetActive(false);
        planeManager.gameObject.SetActive(true);
       

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public  void ScoreUpdater(int score)
    {
      if(score > hScore)
        {
            hScore = score;
            PlayerPrefs.SetInt("HighScore", hScore);
        }
        highScore.text="High Score : "+hScore.ToString();
        this.score.text = "Score : "+score.ToString();
       
    }
}
