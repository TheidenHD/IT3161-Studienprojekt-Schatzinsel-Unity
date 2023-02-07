using Unity.VisualScripting;
using static Map;

public class Team : Map
{
    private Map map;

    public Team(int Width, int Height, Map map) : base(Width, Height)
    {
        this.map = map;
    }

    public void check(int Width, int Height)
    {
        this[Width, Height] = map[Width, Height];
    }
}
