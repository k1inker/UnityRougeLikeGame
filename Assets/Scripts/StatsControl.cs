using UnityEngine;

public class StatsControl : MonoBehaviour
{
    [Header("Stats")]
    public float velocity;
    public float rotateSpeed;
    public float health;
    public int coin;
    public int[] bigPotion = new int[4] { 0, 0, 0, 0 };
    public int[] smallPotion = new int[4] { 0, 0, 0, 0 };
}
