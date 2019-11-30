//NYAN NYAN NYAN
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using HoloToolkit.Unity.InputModule;
using Debug = UnityEngine.Debug;

public class havefun : MonoBehaviour, IInputClickHandler
{

    public GameObject PrimaryScript;
    public GameObject CurrentlyLookedAt;
    public GameObject overobj;
    public bool IsDeployed = false;

    // Use this for initialization
    void Start () {
		PrimaryScript = GameObject.Find("ScriptManager");
        overobj.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
	{
	    CurrentlyLookedAt = PrimaryScript.GetComponent<PrimaryScript>().CurrentlyGazedAt;
        //Debug.Log(CurrentlyLookedAt.name);
	}

    public void OnInputClicked(InputClickedEventData eventData)
    {
        if (CurrentlyLookedAt.name == "chess" && IsDeployed == false)
        {
            //overobj.SetActive(true);
            IsDeployed = true;
            Debug.Log("open");
        }

        if (CurrentlyLookedAt.name == "chess" && IsDeployed == true) 
        {
            //overobj.SetActive(false);
            IsDeployed = false;
            Debug.Log("close");
        }
    }

}
