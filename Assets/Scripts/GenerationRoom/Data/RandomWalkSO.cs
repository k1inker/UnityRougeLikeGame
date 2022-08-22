using UnityEngine;

[CreateAssetMenu(menuName = "PSG/RandomWalkData", fileName = "RandomWalkParametrs_")] 
public class RandomWalkSO : ScriptableObject
{
    public int iteration = 10, walkLength = 10;
    public bool startRandomlyEachIteretion = true;
}
