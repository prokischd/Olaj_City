using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Trigger_Script : MonoBehaviour
{
    public GameObject uiObj;
	public bool enabled = true;
    void OnTriggerEnter2D(Collider2D col){
        Debug.Log(col.name.ToString());
        if(col.gameObject.layer==8 && enabled){
            Debug.Log("has entered col");
            uiObj.SetActive(true);
			StartCoroutine("Wait");
        }
    }

	IEnumerator Wait()
	{
		yield return new WaitForSeconds(3.0f);
		if(uiObj != null)
		{
			uiObj.SetActive(false);
			enabled = false;
		}
	}
	void OnTriggerExit2D(Collider2D col){
        if(col.gameObject.layer==8){
            uiObj.SetActive(false);
			enabled = false;
		}
    }
}
