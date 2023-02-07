using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pirate : MonoBehaviour
{
    public float speed = 2f;
    public Vector2[] path;
    public int currentIndex;
    public Team team;

    private Vector3 destination;

    void Start()
    {
        destination = transform.position;
        currentIndex = 0;
    }

    void Update()
    {
        if (transform.position != destination)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
        }
        else
        {
            if (currentIndex < path.Length)
            {
                destination = path[currentIndex];
                currentIndex++; 
            }
        }
    }
}
