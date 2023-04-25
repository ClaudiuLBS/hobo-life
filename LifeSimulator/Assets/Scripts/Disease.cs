
using UnityEngine;

public class Disease : ScriptableObject
{
    public new string name;
    public int curePrice;
    public float damagePerDay;

    public Disease(string name, int curePrice, float damagePerDay)
    {
        this.name = name;
        this.curePrice = curePrice;
        this.damagePerDay = damagePerDay;
    }
}
