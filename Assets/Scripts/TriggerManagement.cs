using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerManagement : MonoBehaviour {


    public GameObject GameController;

    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            GameController.GetComponent<LevelManagement>().generateNewPart();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log("Stay");
    }
}
