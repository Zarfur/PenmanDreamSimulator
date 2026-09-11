using System.Collections.Generic;
using UnityEngine;

public static class TimeCache
{
    private static Dictionary<float, WaitForSeconds> waitForSecondses = new Dictionary<float, WaitForSeconds>();

    public static WaitForSeconds Wait(float seconds)
    {
        if(!waitForSecondses.TryGetValue(seconds, out var waitForSeconds))
        {
            waitForSeconds = new WaitForSeconds(seconds);
            waitForSecondses.Add(seconds, waitForSeconds);
        }
        return waitForSeconds;
    }
}