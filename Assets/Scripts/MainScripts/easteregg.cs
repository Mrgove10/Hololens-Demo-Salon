//NYAN NYAN NYAN
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using HoloToolkit.Unity.InputModule;
using Debug = UnityEngine.Debug;

public class easteregg : MonoBehaviour
{
    public int numberin;

    public GameObject seautext;

    // Use this for initialization
    void Start ()
    {

        seautext = GameObject.Find("seautxt");
    }
	
	// Update is called once per frame
	void Update ()
	{
	    Debug.Log("currently cliding whit = " + numberin);
	}

    void OnTriggerEnter(Collider over)
    {
        numberin++;
    }

    void OnTriggerExit(Collider over)
    {
        numberin--;
    }
}
