using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public LayerMask placeMask;
    public GameObject[] turrets;
    [HideInInspector] public bool placeIt;
    [HideInInspector] public int placementCounter;
    public GameObject Monkey, Duck, Elephant, Frog, Eagle, Koala;
    public float timer;
    UIManager UM;
    void Start()
    {
        UM = FindObjectOfType<UIManager>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(placementCounter == 2)
        {
            Duck.SetActive(false);
            Eagle.SetActive(true);
        }
        if(placementCounter == 3)
            Eagle.SetActive(false);
        if(placementCounter == 3 && timer>=15)
            Monkey.SetActive(true);
        if(placementCounter == 4)
            Monkey.SetActive(false);
        if(placementCounter == 4 && timer>=30)
            Frog.SetActive(true);
        if(placementCounter == 5)
            Frog.SetActive(false);
        if(placementCounter == 5 && timer>=40)
            Elephant.SetActive(true);
        if(placementCounter == 6)
        {
            Elephant.SetActive(false);
            Koala.SetActive(true);
        }
        if(placementCounter == 7)
            Koala.SetActive(false);
    }
    public void Placement(int num)
    {
        if (Input.GetButtonDown("Mouse0"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, placeMask))
            {
                Instantiate(turrets[num], hit.transform.position, Quaternion.identity);
                foreach (GameObject light in UM.lights)
                {
                    light.SetActive(false);
                }
                Destroy(hit.transform.gameObject);
                placeIt = false;
                placementCounter++;
            }
        }
    }
}
