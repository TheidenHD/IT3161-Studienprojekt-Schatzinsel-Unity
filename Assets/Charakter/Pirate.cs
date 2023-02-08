using UnityEngine;

public class Pirate : MonoBehaviour
{
    private float speed;
    private float sicht;
    public Vector2[] path;
    public Animator animator;
    private int currentIndex;
    public Team team;
    public Map<Element> map;

    private Vector3 destination;

    void Start()
    {
        destination = transform.position;
        currentIndex = 0;
        team.check(0, 0);
        sicht = Random.Range(2, 5);
        speed = ((float)Random.Range(10, 21)) / 10;
        team.check((int)destination.x, (int)destination.y);
        animator.SetFloat("Speed", speed);
    }

    void Update()
    {
        if (transform.position != destination)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
        }
        else
        {
            for (int i = 0; i < sicht / 2; i++)
            {
                for (int j = 1; j <= (sicht - i) / 2; j++)
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
                Vector3 temp = destination - transform.position;
                animator.SetFloat("LEFT/RIGHT", temp.x);
                animator.SetFloat("UP/Down", temp.y);
            }
            else
            {
                this.Generate();
            }
        }
    }

    private void Generate()
    {
        currentIndex = 0;
        //Generate new Path
        path = new Vector2[0];
    }
}
