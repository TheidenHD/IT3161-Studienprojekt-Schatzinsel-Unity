using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Optionen : MonoBehaviour
{
    private List<int> pirats = new List<int>();
    private List<int> teams = new List<int>();
    private bool hiden = true;
    private List<int> size = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 1; i < 5; i++)
        {
            pirats.Add(i);
            teams.Add(i);
            size.Add(i);
        }
    }

    public void Pirat()
    {
        int temp = pirats[0];
        pirats.RemoveAt(0);
        pirats.Add(temp);
        Generate.Pirates = pirats[0];
    }

    public void Team()
    {
        int temp = teams[0];
        teams.RemoveAt(0);
        teams.Add(temp);
        Generate.Teams = teams[0];
    }

    public void Hiden()
    {
        hiden = !hiden;
        Generate.hide = hiden;
    }

    public void Size()
    {
        int temp = size[0];
        size.RemoveAt(0);
        size.Add(temp);
        Generate.Height = size[0] * 10;
        Generate.Width = size[0] * 20;
    }
}
