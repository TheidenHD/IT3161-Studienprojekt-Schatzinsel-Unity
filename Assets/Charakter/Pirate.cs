using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static UnityEngine.GraphicsBuffer;

public class Pirate : MonoBehaviour
{
    private float speed;
    private float sicht;
    private bool suchen = true;
    public List<Vector2> path;
    public Animator animator;
    private int currentIndex;
    public Team team;
    public Map<Element> map;

    private Vector3 destination;

    void Start()
    {
        destination = transform.position;
        path.Add(destination);
        currentIndex = 0;
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
        else if (suchen || currentIndex < path.Count)
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
            if (currentIndex >= path.Count || team.gefunden)
            {
                this.Generate();
                currentIndex = 0;
            }
            destination = path[currentIndex];
            currentIndex++;
            Vector3 temp = destination - transform.position;
            animator.SetFloat("LEFT/RIGHT", temp.x);
            animator.SetFloat("UP/Down", temp.y);
            if (team[(int)destination.x, (int)destination.y] == TileStatus.Schatz)
            { 
                suchen = false;
                destination = transform.position;
            }

        }
        else 
        {
            animator.SetFloat("Speed", 0);
        }
    }

    private int checkSicht(int Width, int Height)
    {
        List<bool> ret = new List<bool>();
        for (int i = 0; i < sicht / 2; i++)
        {
            for (int j = 1; j <= (sicht - i) / 2; j++)
            {
                ret.Add(team.checkU(Width + i, Height + j));
                ret.Add(team.checkU(Width - i, Height - j));
                ret.Add(team.checkU(Width + j, Height - i));
                ret.Add(team.checkU(Width - j, Height + i));
            }
        }
        return ret.Count(x => x);
    }

    private void Generate()
    {
        foreach (Element e in map)
        {
            e.Cost = -1;
        }
        currentIndex = 0;
        //Generate new Path
        map[(int)destination.x, (int)destination.y].Cost = 0; 
        map[(int)destination.x, (int)destination.y].Belonung = this.checkSicht((int)destination.x, (int)destination.y);
        List< Vector2Int> temp = new List<Vector2Int>();
        temp.Add(new Vector2Int((int)destination.x, (int)destination.y));
        while (temp.Count > 0)
        {
            if (team[temp[0].x, temp[0].y] == TileStatus.Schatz)
            {
                map[temp[0].x, temp[0].y].Belonung = int.MaxValue;
                map[temp[0].x, temp[0].y].Cost = 1;
            }
            if (team.checkB(temp[0].x + 1, temp[0].y) && map[temp[0].x + 1, temp[0].y].Cost == -1)
            {
                map[temp[0].x + 1, temp[0].y].Cost = map[temp[0].x, temp[0].y].Cost + 1;
                map[temp[0].x + 1, temp[0].y].Belonung = this.checkSicht(temp[0].x + 1, temp[0].y);
                map[temp[0].x + 1, temp[0].y].Vorheriger = temp[0];
                temp.Add(new Vector2Int(temp[0].x + 1, temp[0].y));
            }
            if (team.checkB(temp[0].x - 1, temp[0].y) && map[temp[0].x - 1, temp[0].y].Cost == -1)
            {
                map[temp[0].x - 1, temp[0].y].Cost = map[temp[0].x, temp[0].y].Cost + 1;
                map[temp[0].x - 1, temp[0].y].Belonung = this.checkSicht(temp[0].x - 1, temp[0].y);
                map[temp[0].x - 1, temp[0].y].Vorheriger = temp[0];
                temp.Add(new Vector2Int(temp[0].x - 1, temp[0].y));
            }
            if (team.checkB(temp[0].x, temp[0].y + 1) && map[temp[0].x, temp[0].y + 1].Cost == -1)
            {
                map[temp[0].x, temp[0].y + 1].Cost = map[temp[0].x, temp[0].y].Cost + 1;
                map[temp[0].x, temp[0].y + 1].Belonung = this.checkSicht(temp[0].x, temp[0].y + 1);
                map[temp[0].x, temp[0].y + 1].Vorheriger = temp[0];
                temp.Add(new Vector2Int(temp[0].x, temp[0].y + 1));
            }
            if (team.checkB(temp[0].x, temp[0].y - 1) && map[temp[0].x, temp[0].y - 1].Cost == -1)
            {
                map[temp[0].x, temp[0].y - 1].Cost = map[temp[0].x, temp[0].y].Cost + 1;
                map[temp[0].x, temp[0].y - 1].Belonung = this.checkSicht(temp[0].x, temp[0].y - 1);
                map[temp[0].x, temp[0].y - 1].Vorheriger = temp[0];
                temp.Add(new Vector2Int(temp[0].x, temp[0].y - 1));
            }
            temp.RemoveAt(0);
        }
        int Gewin = 0;
        temp = new List<Vector2Int>();
        foreach (Element e in map)
        {
            if (e.Belonung > 0 && e.Belonung / e.Cost == Gewin)
            {
                temp.Add(e.pos);
            }
            else if (e.Belonung > 0 && e.Belonung / e.Cost > Gewin)
            {
                temp = new List<Vector2Int>();
                temp.Add(e.pos);
                Gewin = e.Belonung / e.Cost;
            }
        }
        Vector2Int target = temp[Random.Range(0, temp.Count)];
        path = new List<Vector2>();
        while (map[target.x, target.y].Cost > 0)
        {
            path.Insert(0, target);
            target = map[target.x, target.y].Vorheriger;
        }
    }
}
