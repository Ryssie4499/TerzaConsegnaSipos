using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public LayerMask placeMask;
    public GameObject[] turrets;
    public bool placeIt;
    void Start()
    {
        
    }

    void Update()
    {

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
                placeIt = false;
            }
        }
    }
}
