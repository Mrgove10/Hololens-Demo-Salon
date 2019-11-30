//NYAN NYAN NYAN
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using HoloToolkit.Unity.InputModule;
using Debug = UnityEngine.Debug;

public class PrimaryScript : MonoBehaviour, IInputClickHandler
{
    private int SpawnNum;

    private int ClickNumero = 1;
    public Text CenterText;
    public Text BottomRightText;

    public GameObject SphereBluePrefab;
    public GameObject SphereVertPrefab;
    public GameObject SphereRougePrefab;
    public GameObject Cursor;

    public bool HasBeenMoved = false;
    public bool HasBeenRezized = false;

    public float TimeLeft = 240.0f;


    public bool SkipIntro = false;
    public bool SkipTuto = false;
    public bool SkipBoules = false;
    private bool MainClick;
    
    private bool TutoClick1;
    private bool TutoClick2;

    private bool StartupFinished;
    public bool TutoPart1Finished;
    public bool TutoPart2Finished;
    public bool TutoPart3Finished;
    public bool TutoPart4Finished;

    public GameObject HaveFunPartObjects;

    private bool SphereSpawned = false;

    public GameObject CurrentlyGazedAt;

    public int etape;
    public bool inpart3;

    public Color TransparencyStart = Color.white;
    public Color TransparencyEnd = Color.white;

    public GameObject E1;
    public GameObject E2;
    public GameObject E3;

    public bool isInHaveFun = false;

    // Use this for initialization
    void Awake () {
        HaveFunPartObjects = GameObject.Find("HaveFunPartObjects");
        InputManager.Instance.PushModalInputHandler(gameObject);

        CenterText = GameObject.Find("TextCenter").GetComponent<Text>();
        BottomRightText = GameObject.Find("TextBottomRight").GetComponent<Text>();

        Cursor = GameObject.Find("Cursor");

        HaveFunPartObjects.SetActive(false);

        E1 = GameObject.Find("E1");
        E2 = GameObject.Find("E2");
        E3 = GameObject.Find("E3");
        
        
        TransparencyStart.a = 0;
        E1.GetComponent<Text>().color = TransparencyStart;
        E2.GetComponent<Text>().color = TransparencyStart;
        E3.GetComponent<Text>().color = TransparencyStart;   


        if (SkipIntro)
        {
            TutoPart1Finished = true;
            TutoPart2Finished = true;
        }
        else
        {
            StartCoroutine(Part1(4));
            if (TutoPart1Finished)
            {
                StartCoroutine(Part2(4));
            }

        }
        if (SkipTuto)
        {
            TutoPart3Finished = true;
        }
        else if (TutoPart1Finished && TutoPart2Finished)
        {
            Part3();
        }
        if (TutoPart3Finished)
        {
            Part4();
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (isInHaveFun)
        {
            TimeLeft = TimeLeft - Time.deltaTime;
            if (TimeLeft < 0)
            {
                StartCoroutine(Fin());
            }
        }

        
        if (TutoPart3Finished)
        {
            Part4();
        }
        if (inpart3)
        {
            Part3();
        }
        
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        MainClick = true;
        Debug.Log("MainClick");

        if (TutoPart2Finished && !TutoPart3Finished)
        {
            SpawnNum++;
            if (SpawnNum > 3)
            {
                SpawnNum = 1;
            }
            SpawnSphere();
        }

        if (TutoClick1)
        {
            TutoClick1 = false;
            StartCoroutine(Part2(4));
        }
        if (TutoClick2)
        {
            TutoClick2 = false;
            StartCoroutine(TutoStart(4));
        }
    }

    public void SpawnSphere()
    {

        if (!SphereSpawned)
        {
            SphereSpawned = true;
        }
        Debug.Log("object spawned    " + SphereSpawned);
        if (SpawnNum == 1)
        {
            GameObject projecile = Instantiate(SphereBluePrefab);
            projecile.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 0.85f;
            Rigidbody rb = projecile.GetComponent<Rigidbody>();
            rb.velocity = Camera.main.transform.forward * 10;
        }
        if (SpawnNum == 2)
        {
            GameObject projecile = Instantiate(SphereVertPrefab);
            projecile.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 0.85f;
            Rigidbody rb = projecile.GetComponent<Rigidbody>();
            rb.velocity = Camera.main.transform.forward * 10;
        }
        if (SpawnNum == 3)
        {
            GameObject projecile = Instantiate(SphereRougePrefab);
            projecile.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 0.85f;
            Rigidbody rb = projecile.GetComponent<Rigidbody>();
            rb.velocity = Camera.main.transform.forward * 10;
        }
        
    }
 
    IEnumerator Part1(int secondsToWait)
    {

        Debug.Log("part1 lancer");
        Cursor.SetActive(false);
        TutoClick1 = false;
        CenterText.text = "Bienvenue dans l'Hololens.";
        yield return new WaitForSeconds(secondsToWait);

        CenterText.text = "Dans cette démo nous allons voir quelques fonctionnalités de base.";
        yield return new WaitForSeconds(secondsToWait);

        CenterText.text = "Afin de cliquer, fermez votre main et pincez uniquement avec votre pouce et index.";
        yield return new WaitForSeconds(secondsToWait);

        CenterText.text = "Essayez de cliquer en pincant uniquement votre pouce et index.";
        
        TutoClick1 = true;
        TutoPart1Finished = true;
        Debug.Log("Fin Partie 1");

    }   

    IEnumerator Part2(int secondsToWait)
    {
        Debug.Log("part2 lancer");
        CenterText.text = "Parfait !";
        yield return new WaitForSeconds(secondsToWait);

        CenterText.text = "Gardez cliqué pour faire un clique long.";
        yield return new WaitForSeconds(secondsToWait);

        CenterText.text = "Cliquez pour accéder à la suite de la démo.";

        TutoClick2 = true;
     
        TutoPart2Finished = true;
        Debug.Log("Fin Partie 2");
    }
    
    IEnumerator TutoStart(int secondsToWait)
    {
        Debug.Log("mainstart lancer");
        CenterText.text = "Analyse de l'environement...";
        yield return new WaitForSeconds(secondsToWait);
        Cursor.SetActive(true);
        CenterText.text = "";
        StartupFinished = true;
        Debug.Log("Startup Finished");
        Part3();
    }

    private void Part3()
    {
        inpart3 = true;
        E1.GetComponent<Text>().text = "Faire apparaitre des sphères.";
        E2.GetComponent<Text>().text = "Déplacer une sphère verte.";
        E3.GetComponent<Text>().text = "Redimensionner une sphère rouge.";

        TransparencyEnd.a = 1.0f;
        E1.GetComponent<Text>().color = TransparencyEnd;
        E2.GetComponent<Text>().color = TransparencyEnd;
        E3.GetComponent<Text>().color = TransparencyEnd;

        if (SphereSpawned)
        {
            GameObject.Find("E1").GetComponent<Text>().color = Color.green;
        }
        if (HasBeenMoved)
        {
            GameObject.Find("E2").GetComponent<Text>().color = Color.green;
        }
        if (HasBeenRezized)
        {
            GameObject.Find("E3").GetComponent<Text>().color = Color.green;
        }

        if (HasBeenMoved && HasBeenRezized && SphereSpawned)
        {
            inpart3 = false;
            TutoPart3Finished = true;
        }
    }

    private void Part4()
    {
      //  Debug.Log("fin tuto general");

        StartCoroutine(HaveFunStart());
        TutoPart4Finished = true;
    }

    IEnumerator HaveFunStart()
    {
        isInHaveFun = true;
        yield return new WaitForSeconds(2.5f);

        var objectstodestry = GameObject.FindGameObjectsWithTag("Sphere");
        objectstodestry = objectstodestry.Concat(GameObject.FindGameObjectsWithTag("UITuto")).ToArray();
        foreach (var o in objectstodestry)
        {
            //Debug.Log(o);
            DestroyObject(o);
        }

        HaveFunPartObjects.SetActive(true);
        
    }

    IEnumerator Fin()
    {
        Cursor.SetActive(false);
        CenterText.text = "Merci d'avoir testé :) \n Demo cree par \n Adrien RICHARD, 1er année (B1) \n \n logiciel utiliser : \n - Unity \n - Visual Studio (Language C#) \n - Microsoft Mixed Reality ToolKit (SDK) \n \n adrien.richard@epsi.fr"; ;
        yield return new WaitForSeconds(20f);
        Application.Quit();

    }
}
