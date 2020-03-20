using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax1 : MonoBehaviour
{
    public float scrollSpeed;
    public GameObject martin;
    Vector2 startPos;
    

    void Start()
    {
        
    }

    void Update()
    {
        startPos = martin.transform.position;
        float newpos = Mathf.Repeat(Time.time * scrollSpeed, 16);
        transform.position = startPos + Vector2.right * newpos;
    }
}
