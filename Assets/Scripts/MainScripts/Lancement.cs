//NYAN NYAN NYAN
using System.Collections;
using UnityEngine;
using  UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

public class Lancement : MonoBehaviour
{

    public GameObject CurrentlyGazedAt;

    public int TimeLookAt;

    public int timetoswitchlvl = 15;
    // Use this for initialization
    void Start()
    {

    }

    void Update()
    {
        Debug.Log(CurrentlyGazedAt);
        if (CurrentlyGazedAt.name == "Plane")
        {
            TimeLookAt++;
        }
        if (TimeLookAt > timetoswitchlvl)
        {
            nextscene();
        }
    }

    public void nextscene()
    {
        SceneManager.LoadSceneAsync("Main");
    }
    
}
