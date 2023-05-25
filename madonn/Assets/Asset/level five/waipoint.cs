using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waipoint : MonoBehaviour
{
    public List<GameObject> Waypoints;
    public float speed = 2;

    void Start()
    {
        
    }

    private void Update()
    {
        int index = 0;
        Vector3 newPos = Vector3.MoveTowards(transform.position, Waypoints[index].transform.position, speed * Time.deltaTime);
        transform.position = newPos;

    }

}
