using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Random_Tree_Sprite : MonoBehaviour
{

public Sprite[] allSprites;
private SpriteRenderer myRenderer;

    void Start()
    {
        myRenderer=GetComponent<SpriteRenderer>();
        myRenderer.sprite=allSprites[Random.Range(0,allSprites.Length-1)];
    }

    void Update()
    {
        
    }
}
