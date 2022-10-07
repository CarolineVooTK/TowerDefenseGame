using System.Collections;
using UnityEngine;

public class Sumo : Enemy
{
    public Sumo()
    {
        startingHunger *= 4;
        speed *= 0.4;
        tokensDropped *= 4;
    }
}
