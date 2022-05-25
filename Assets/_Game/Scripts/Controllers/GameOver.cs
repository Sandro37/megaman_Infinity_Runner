using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] private Text pointTextRecord;
    [SerializeField] private Text pointText;
    void Start()
    {
        if (pointText != null && pointTextRecord != null)
        {
            pointText.text = PlayerPrefs.GetInt("Point").ToString();
            pointTextRecord.text = PlayerPrefs.GetInt("Record").ToString();
        }

    }

}
