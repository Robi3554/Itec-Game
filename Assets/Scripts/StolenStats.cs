using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StolenStats", menuName = "StolenStats")]
public class StolenStats : ScriptableObject
{
    public float damage;
    public float speed;
    public float health;

    public int stolenCount;
}
