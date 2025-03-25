using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [HideInInspector]
    public float speed;

    private Rigidbody2D myBody;


    // Start is called before the first frame update
    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        myBody.linearVelocity = new Vector2(speed, myBody.linearVelocity.y);

    }
}//class
