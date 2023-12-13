using System;
using System.Linq;
using PowerUp;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;


public class PointShop : MonoBehaviour
{
    [SerializeField] private Button expandButton;
    [SerializeField] private Button speedUpButton;
    [SerializeField] private Button explosionButton;
    [SerializeField] private Button magnetButton;
    [SerializeField] private Button oneUpButton;


    public TMP_Text pointsText;
    private int _points;

    private void Start()
    {
        _points = PlayerPrefs.GetInt("Points");
        pointsText.text = "Points: " + _points.ToString();
        expandButton.interactable =  PlayerPrefs.GetInt("isExpandPaddle") != 1;
        speedUpButton.interactable = PlayerPrefs.GetInt("isSpeedUp") != 1;
        explosionButton.interactable = PlayerPrefs.GetInt("isExplotion") !=1;
        magnetButton.interactable =  PlayerPrefs.GetInt("isMagnet") !=1;
        oneUpButton.interactable =  PlayerPrefs.GetInt("isOneUp") != 1;
    }

    public void BuyExpand()
    {
        if (_points >= 2)
        {
            _points -= 2;
            PlayerPrefs.SetInt("Points", _points);
            PlayerPrefs.SetInt("isExpandPaddle", 1);
            PlayerPrefs.Save();
            pointsText.text = "Points: " + _points.ToString();
            expandButton.interactable = false;
        }
    }

    public void BuySpeedUp()
    {
        if (_points >= 2)
        {
            _points -= 2;
            PlayerPrefs.SetInt("Points", _points);
            PlayerPrefs.SetInt("isSpeedUp", 1);
            PlayerPrefs.Save();
            pointsText.text = "Points: " + _points.ToString();
            speedUpButton.interactable = false;
        }
    }

    public void BuyExplotion()
    {
        if (_points >= 2)
        {
            _points -= 2;
            PlayerPrefs.SetInt("Points", _points);
            PlayerPrefs.SetInt("isExplotion", 1);
            PlayerPrefs.Save();
            pointsText.text = "Points: " + _points.ToString();
            explosionButton.interactable = false;
        }
    }

    public void BuyMagnet()
    {
        if (_points >= 2)
        {
            _points -= 2;
            PlayerPrefs.SetInt("Points", _points);
            PlayerPrefs.SetInt("isMagnet", 1);
            PlayerPrefs.Save();
            pointsText.text = "Points: " + _points.ToString();
            magnetButton.interactable = false;
        }
    }

    public void BuyOneUP()
    {
        if (_points >= 2)
        {
            _points -= 2;
            PlayerPrefs.SetInt("Points", _points);
            PlayerPrefs.SetInt("isOneUp", 1);
            PlayerPrefs.Save();
            pointsText.text = "Points: " + _points.ToString();
            oneUpButton.interactable = false;
        }
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(0);
    }
}