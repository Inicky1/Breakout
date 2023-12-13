using System;
using System.Linq;
using PowerUp;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;


public class PointShop : MonoBehaviour
{

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
            pointsText.text = "Points: " + _points.ToString();
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
        }
    } public void BuyMagnet()
    {
        if (_points >= 2)
        {
            _points -= 2;
            PlayerPrefs.SetInt("Points", _points);
            PlayerPrefs.SetInt("isMagnet", 1);
            PlayerPrefs.Save();
            pointsText.text = "Points: " + _points.ToString();
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
        }
    }
    
    
    
    
    
    

    public void RestartButton()
    {
        SceneManager.LoadScene(0);
    }
}