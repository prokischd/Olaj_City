using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Trigger_Script : MonoBehaviour
{
    public GameObject uiObj;
    void OnTriggerEnter2D(Collider2D col){
        Debug.Log(col.name.ToString());
        if(col.gameObject.layer==8){
            Debug.Log("has entered col");
            uiObj.SetActive(true);
        }
    }
 void OnTriggerExit2D(Collider2D col){
        if(col.gameObject.layer==8){
            uiObj.SetActive(false);
        }
    }


}
