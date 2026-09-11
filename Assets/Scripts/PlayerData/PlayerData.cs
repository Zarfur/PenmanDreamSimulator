using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/PlayerData")]

public class PlayerData : ScriptableObject
{

    private int maxHP;

    public int getMaxHP() => maxHP;
}