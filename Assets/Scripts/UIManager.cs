using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject[] lights;
    InputManager IM;
    public int num;
    private void Start()
    {
        IM = FindObjectOfType<InputManager>();
    }
    void Update()
    {
        if(IM.placeIt)
        {
            IM.Placement(num);
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
            IM.placeIt = true;
        }
    }
    public void Eagle()
    {
        foreach (GameObject light in lights)
        {
            light.SetActive(true);
            num = 4;
            IM.placeIt = true;
        }
    }
    public void Koala()
    {
        foreach (GameObject light in lights)
        {
            light.SetActive(true);
            num = 5;
            IM.placeIt = true;
        }
    }
    
}
