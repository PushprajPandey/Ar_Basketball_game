using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI highScore;

    private void Start()
    {
        highScore.text= "High Score : "+PlayerPrefs.GetInt("HighScore").ToString();
    }
    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
