
// Define player property here
public class Player
{
    public string name { get; set; }
    public int activeWaypointIndex { get; set; }
    public float distanceToWaypoint { get; set; }

    public override string ToString()
    {
        return activeWaypointIndex + "__" + name;
    }
}
