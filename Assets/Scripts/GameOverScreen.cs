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
        var points = PlayerPrefs.GetInt("Points");
        pointsText.text = points.ToString() + " POINTS";
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(0);
    }

    public void PointShopButton()
    {
        SceneManager.LoadScene(2);
    }
}