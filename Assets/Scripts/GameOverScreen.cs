using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public TMP_Text pointsText;

    private void Start()
    {
        int points = PlayerPrefs.GetInt("Points");
        pointsText.text = points.ToString() + " POINTS";
    }

    public void RestartButton()
    {
        Debug.Log("button working");
        SceneManager.LoadScene(0);
    }

    public void PointShopButton()
    {
    }
}