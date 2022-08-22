using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class CorridorDungeonGenerator : RandomDungeonGenerator
{
    [SerializeField] private int corridorLength = 10, corridorCount = 5;
    [SerializeField, Range(0.1f, 1f)] private float roomPercent = 0.8f;

    protected override void RunProceduralGeneration()
    {
        CorridorFirstGeneration();
    }

    private void CorridorFirstGeneration()
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialRoomPosition = new HashSet<Vector2Int>();

        CreateCoridors(floorPositions, potentialRoomPosition);

        HashSet<Vector2Int> roomPositions = CreateRooms(potentialRoomPosition);

        List<Vector2Int> deadEnds = FindDeadEnds(floorPositions);
        CreateRoomAtDeadEnd(deadEnds, roomPositions);

        floorPositions.UnionWith(roomPositions);

        tileMapVisualizer.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, tileMapVisualizer);
    }

    private void CreateRoomAtDeadEnd(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomFloors)
    {
        foreach(var position in deadEnds)
        {
            if(roomFloors.Contains(position) == false)
            {
                var room = RunRandomWalk(randomWalkParametrs,position);
                roomFloors.UnionWith(room);
            }
        }
    }

    private List<Vector2Int> FindDeadEnds(HashSet<Vector2Int> floorPositions)
    {
        List<Vector2Int> deadEnds = new List<Vector2Int>();
        foreach(var position in floorPositions)
        {
            int neighbourCount = 0;
            foreach(var direction in Direction2D.cardinalDirectionList)
            {
                if(floorPositions.Contains(position + direction))
                    neighbourCount++;
            }
            if(neighbourCount == 1)
                deadEnds.Add(position);
        }
        return deadEnds;
    }

    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPosition)
    {
        HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();
        int roomToCreateCount = Mathf.RoundToInt(potentialRoomPosition.Count * roomPercent);

        List<Vector2Int> roomsToCreate = potentialRoomPosition.OrderBy(x => Guid.NewGuid()).Take(roomToCreateCount).ToList();

        foreach(var roomPosition in roomsToCreate)
        {
            var roomFloor = RunRandomWalk(randomWalkParametrs, roomPosition);
            roomPositions.UnionWith(roomFloor);
        }
        return roomPositions;
    }

    private void CreateCoridors(HashSet<Vector2Int> floorPosition, HashSet<Vector2Int> potentialRoomPosition)
    {
        var curentPosition = startPosition;
        potentialRoomPosition.Add(curentPosition);

        for(int i = 0; i < corridorCount; i++)
        {
            var corridor = ProceduralGenerationAlgorithm.RandomWalkCorridor(curentPosition,corridorLength);
            curentPosition = corridor[corridor.Count - 1];
            potentialRoomPosition.Add(curentPosition);
            floorPosition.UnionWith(corridor);
        }
    }
}
