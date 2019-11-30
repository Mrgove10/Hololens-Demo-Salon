using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using HoloToolkit.Unity.InputModule;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Hold : MonoBehaviour, IHoldHandler
{
    public float TimeHolded;
    private GameObject CurrentlyGazedAt;
    private Scene CurrentScene;

    private bool HoldStarted;
    private bool HoldComplete;
    private bool HoldCanceled;

    // Use this for initialization
    void Start () {
	    InputManager.Instance.PushModalInputHandler(this.gameObject);
    }

    void Update()
    {
        CurrentScene = SceneManager.GetActiveScene();

        CurrentlyGazedAt = GameObject.Find("ScriptManager").GetComponent<PrimaryScript>().CurrentlyGazedAt;

     /*   if (CurrentScene.name == "Main")
        {
            TimeHolded++;
            if (TimeHolded > 30) // if 60fs = demi seconde
            {
                TimeHolded = 0; 
                if (CurrentlyGazedAt.name == "chest")
                {
                    Debug.Log("chest");
                }
               //Debug.Log(CurrentlyGazedAt.name);
            }
        }*/

    }
    public void OnHoldStarted(HoldEventData eventData)
    {
        HoldStarted = true;
     //   Debug.Log("hold started");
    }

    public void OnHoldCompleted(HoldEventData eventData)
    {
        HoldComplete = true;
        //  Debug.Log("hold complete");
    }

    public void OnHoldCanceled(HoldEventData eventData)
    {
        HoldCanceled = true;
        //Debug.Log("hold canceled");
    }

}
