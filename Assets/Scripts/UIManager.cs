using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject[] lights;
    public Image[] hearts;
    public int numOfHearts;
    InputManager IM;
    [HideInInspector] public int num;
    private void Start()
    {
        IM = FindObjectOfType<InputManager>();
    }
    void Update()
    {
        if (IM.placeIt)
        {
            IM.Placement(num);
        }
        if (IM.buffPlaceIt)
        {
            IM.BuffPlacement(num);
        }
        if (StatsManager.Instance.Health < numOfHearts)
        {
            numOfHearts = StatsManager.Instance.Health;
        }
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < StatsManager.Instance.Health)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }


    public void Monkey()
    {
        foreach (GameObject light in lights)
        {
            light.SetActive(true);
            num = 0;
            IM.placeIt = true;
        }
    }
    public void Duck()
    {
        foreach (GameObject light in lights)
        {
            light.SetActive(true);
            num = 1;
            IM.placeIt = true;
        }
    }
    public void Elephant()
    {
        foreach (GameObject light in lights)
        {
            light.SetActive(true);
            num = 2;
            IM.placeIt = true;
        }
    }
    public void Frog()
    {
        foreach (GameObject light in lights)
        {
            light.SetActive(true);
            num = 3;
            IM.buffPlaceIt = true;
        }
    }
    public void Eagle()
    {
        foreach (GameObject light in lights)
        {
            light.SetActive(true);
            num = 4;
            IM.buffPlaceIt = true;
        }
    }
    public void Koala()
    {
        foreach (GameObject light in lights)
        {
            light.SetActive(true);
            num = 5;
            IM.buffPlaceIt = true;
        }
    }

}
