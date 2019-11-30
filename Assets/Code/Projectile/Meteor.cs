using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject meteor;
    private MeteorArea meteorArea;
    private GameState gs;
    void Start()
    {
        meteorArea = GetComponentInChildren<MeteorArea>();
        meteor = transform.Find("Meteor").gameObject;
        gs = GameState.GetGameState();
    }

    // Update is called once per frame
    void Update()
    {
        if (!meteorArea.destroyed)
        {
            meteor.transform.Translate(0, -0.15f * Time.deltaTime * gs.spawnerProjectileForce, 0);
        }
    }
}
