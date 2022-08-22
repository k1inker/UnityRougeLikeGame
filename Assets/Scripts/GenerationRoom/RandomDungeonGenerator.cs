using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class RandomDungeonGenerator : AbstractRandomGenerator
{
    [SerializeField] protected RandomWalkSO randomWalkParametrs;

    protected override void RunProceduralGeneration()
    {
        HashSet<Vector2Int> floorPosition = RunRandomWalk(randomWalkParametrs, startPosition);
        tileMapVisualizer.Clear();
        tileMapVisualizer.PaintFloorTiles(floorPosition);
        WallGenerator.CreateWalls(floorPosition, tileMapVisualizer);
    }

    protected HashSet<Vector2Int> RunRandomWalk(RandomWalkSO parameters, Vector2Int position)
    {
        var currentPosition = position;
        HashSet<Vector2Int> floorPosition = new HashSet<Vector2Int>();
        for (int i = 0; i < parameters.iteration; i++)
        {
            var path = ProceduralGenerationAlgorithm.SimpleRandomWalk(currentPosition, parameters.walkLength);
            floorPosition.UnionWith(path);
            if (parameters.startRandomlyEachIteretion)
                currentPosition = floorPosition.ElementAt(Random.Range(0, floorPosition.Count));
        }
        return floorPosition;   
    }
}
