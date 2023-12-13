using System;
using System.Linq;
using PowerUp;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;


public class PointShop : MonoBehaviour
{
    [SerializeField] GameController gameController;
    public TMP_Text pointsText;
    private int _points;

    private void Start()
    {
        _points = PlayerPrefs.GetInt("Points");
        pointsText.text = "Points: " + _points.ToString();
    }

    public void BuyExpand()
    {
        if (_points >= 2)
        {
            _points -= 2;
            PlayerPrefs.SetInt("Points", _points);
            PlayerPrefs.SetInt("isExpandPaddle", 1);
            PlayerPrefs.Save();
        }
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(0);
    }
}