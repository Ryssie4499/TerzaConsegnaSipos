using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public LayerMask placeMask;
    public LayerMask pedana;
    public GameObject[] turrets;
    [HideInInspector] public bool placeIt;
    [HideInInspector] public bool buffPlaceIt;
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
        if (placementCounter == 2)
        {
            Duck.SetActive(false);
            Eagle.SetActive(true);
        }
        if (placementCounter == 3)
            Eagle.SetActive(false);
        if (placementCounter == 3 && timer >= 15)
            Monkey.SetActive(true);
        if (placementCounter == 4)
            Monkey.SetActive(false);
        if (placementCounter == 4 && timer >= 30)
            Frog.SetActive(true);
        if (placementCounter == 5)
            Frog.SetActive(false);
        if (placementCounter == 5 && timer >= 40)
            Elephant.SetActive(true);
        if (placementCounter == 6)
        {
            Elephant.SetActive(false);
            Koala.SetActive(true);
        }
        if (placementCounter == 7)
            Koala.SetActive(false);
    }
    public void Placement(int num)
    {
        if (Input.GetButtonDown("Mouse0"))                                                          //col tasto sinistro del mouse
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);                            //faccio partire un raggio dalla camera al punto esatto sul quale è posizionato il mouse
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, placeMask | pedana))                  //se si tratta di una pedana o di una torretta
            {
                if (hit.transform.position.y < 50)
                {
                    Instantiate(turrets[num], hit.transform.position, Quaternion.identity);         //istanzio la torretta corrispondente nella posizione cliccata con il mouse
                    foreach (GameObject light in UM.lights)
                    {
                        light.SetActive(false);
                    }
                    Destroy(hit.transform.gameObject);                                              //elimino il collider sotto la torretta appena piazzata così non si può più piazzarvi sopra altre torrette
                    placeIt = false;                                                                //una volta piazzata la torretta non ne posso piazzare altre dello stesso tipo senza ricliccare sull'icona corrispondente
                    placementCounter++;                                                             //il conteggio delle torrette e dei buff piazzati aumenta
                }
            }
        }
    }
    public void BuffPlacement(int num)
    {
        if (Input.GetButtonDown("Mouse0"))                                                          //col tasto sinistro del mouse
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);                            //faccio partire un raggio dalla camera al punto esatto sul quale è posizionato il mouse
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, placeMask))                           //se si tratta di una torretta
            {
                if (hit.transform.position.y < 50)
                {
                    Instantiate(turrets[num], hit.transform.position, Quaternion.identity);         //istanzio il buff corrispondente nella posizione cliccata con il mouse
                    foreach (GameObject light in UM.lights)
                    {
                        light.SetActive(false);
                    }
                    Destroy(hit.transform.gameObject);                                              //elimino il collider sotto il buff appena piazzata così non si può più piazzarvi sopra altre torrette
                    buffPlaceIt = false;                                                            //una volta piazzato il buff non ne posso piazzare altri dello stesso tipo senza ricliccare sull'icona corrispondente
                    placementCounter++;                                                             //il conteggio delle torrette e dei buff piazzati aumenta
                }
            }
        }
    }
}
