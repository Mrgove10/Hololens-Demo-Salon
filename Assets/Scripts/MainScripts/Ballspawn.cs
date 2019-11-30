using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballspawn : MonoBehaviour
{

    public GameObject spawnableball;
    public int timebetweenspwint = 5;

    private bool canspawn;
    // Use this for initialization
    void Start ()
    {
        canspawn = true;
        if (canspawn)
        {
            StartCoroutine(pop(timebetweenspwint));
        }
    }
	
	// Update is called once per frame
	void Update () {
    }

    IEnumerator pop(int time)
    {
        Instantiate(spawnableball);
        yield return new WaitForSeconds(time);
        StartCoroutine(pop(timebetweenspwint));

    }
}
