using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MetricValue
{
    public PlayerMetrics metric;
    public float value;
}

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Create New Item")]
public class Item : ScriptableObject
{
    public string title;
    public Sprite icon;
    public float size = 1f;
    public List<MetricValue> metricValues;

    public virtual void Use()
    {
        foreach(var m in metricValues) {
            var metric = m.metric;
            var value = m.value;    
            PlayerMechanics.instance.stats[metric] += value;
        }

    }
}
