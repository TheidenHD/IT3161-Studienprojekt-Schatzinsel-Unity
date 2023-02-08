using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pirate : MonoBehaviour
{
    public float speed = 2f;
    public int sicht = 2;
    public Vector2[] path;
    public int currentIndex;
    public Team team;

    private Vector3 destination;

    void Start()
    {
        destination = transform.position;
        currentIndex = 0;
        team.check(0, 0);
    }

    void Update()
    {
        if (transform.position != destination)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
        }
        else
        {
            team.check((int)destination.x, (int)destination.y);
            for (int i = 0; i < sicht; i++)
            {
                for (int j = 1; j <= sicht - i; j++)
                {
                    team.check((int)destination.x + i, (int)destination.y + j);
                    team.check((int)destination.x - i, (int)destination.y - j);
                    team.check((int)destination.x + j, (int)destination.y - i);
                    team.check((int)destination.x - j, (int)destination.y + i);
                }
            }
            if (currentIndex < path.Length)
            {
                destination = path[currentIndex];
                currentIndex++; 
            }
        }
    }
}
