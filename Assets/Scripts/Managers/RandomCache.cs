using System.Collections.Generic;
using UnityEngine;

public static class RandomManager
{
    public static void Seed(int seed)
    {
        Random.InitState(seed);
    }

    public static int Range(int min, int max)
    {
        return Random.Range(min, max);
    }

    public static float Range(float min, float max){
        return Random.Range(min, max);
    }

    public static T GetFromList<T>(List<T> list)
    {
        if(list == null || list.Count == 0) return default;
        return list[Random.Range(0, list.Count)];
    }


}