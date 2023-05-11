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
    public GameObject Monkey, Duck, Elephant, Frog, Eagle, Koala, Turtle;
    public float timer;
    public float randomicTimer;
    public bool turtle;
    public bool used;
    public float rate;
    UIManager UM;
    public GameManager GM;
    void Start()
    {
        UM = FindObjectOfType<UIManager>();
    }

    void Update()
    {
        if (GM.gameStatus == GameManager.GameStatus.gameRunning)
        {
            timer += Time.deltaTime;
            Activation();
        }
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
                if (hit.transform.position.y < 32)
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
    public void Activation()
    {
        int index = 0;
        if (placementCounter == 2 || placementCounter == 8)
        {
            Duck.SetActive(false);
            Eagle.SetActive(true);
        }
        if (placementCounter == 3 || placementCounter == 9)
            Eagle.SetActive(false);
        if ((placementCounter == 3 && timer >= 15) || (placementCounter == 9 && timer >= 90))
            Monkey.SetActive(true);
        if (placementCounter == 4 || placementCounter == 10)
            Monkey.SetActive(false);
        if ((placementCounter == 4 && timer >= 30) || (placementCounter == 10 && timer >= 100))
            Frog.SetActive(true);
        if (placementCounter == 5 || placementCounter == 11)
            Frog.SetActive(false);
        if ((placementCounter == 5 && timer >= 40) || (placementCounter == 11 && timer >= 110))
            Elephant.SetActive(true);
        if (placementCounter == 6 || placementCounter == 12)
        {
            Elephant.SetActive(false);
            Koala.SetActive(true);
        }
        if (placementCounter == 7 || placementCounter == 13)
            Koala.SetActive(false);
        if ((placementCounter == 7 && timer >= 60))
            Duck.SetActive(true);
        if (timer >= 95 && used == false)
            Turtle.SetActive(true);
        if (timer >= 120)
        {
            randomicTimer += Time.deltaTime;
            if (randomicTimer >= rate)
            {
                index = Random.Range(0, 6);
                switch (index)
                {
                    case 0:
                        Duck.SetActive(true);
                        break;
                    case 1:
                        Eagle.SetActive(true);
                        break;
                    case 2:
                        Monkey.SetActive(true);
                        break;
                    case 3:
                        Frog.SetActive(true);
                        break;
                    case 4:
                        Elephant.SetActive(true);
                        break;
                    case 5:
                        Koala.SetActive(true);
                        break;
                }
                randomicTimer = 0;
            }
        }
        //if(placementCounter == 13 && timer>= 135)
        //    Duck.SetActive(true);
        //if (timer >= 145)
        //    Eagle.SetActive(true);
        //if(timer >= 155)
        //    Monkey.Set
    }
}
