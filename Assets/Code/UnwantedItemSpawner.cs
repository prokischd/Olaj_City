using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnwantedItemSpawner : MonoBehaviour
{
    public float timer = 0.0f;
    private float frequency = 10.0f;
    [HideInInspector]
    public bool hasNoItem;
    public GameObject unwantedItem;
    void Start()
    {
        hasNoItem = true;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= frequency && hasNoItem)
        {
            Vector3 pos = this.transform.position;
            GameObject go = Instantiate(unwantedItem, pos, Quaternion.identity);
            go.GetComponent<UnwantedItem>().spawner = this;
            hasNoItem = false;
        }
    }

}
