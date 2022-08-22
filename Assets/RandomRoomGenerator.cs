using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomRoomGenerator : MonoBehaviour
{
    [SerializeField] private List<GameObject> _rooms;
    [SerializeField] private GameObject _initRoom;
    [SerializeField] private int _offset = 30;
    [SerializeField] private int _countRoom = 2;
    [SerializeField] private LayerMask _room;
    private bool _isPlaced = false;
    private void Awake()
    {
        RoomsRandomPlace();
    }

    private void RoomsRandomPlace()
    {
        HashSet<BoundsInt> areaRoomsInstatiate = new HashSet<BoundsInt>();
        Instantiate(_initRoom);
        areaRoomsInstatiate.Add(new BoundsInt(Vector3Int.RoundToInt(_initRoom.transform.position),Vector3Int.RoundToInt(_initRoom.GetComponent<BoxCollider2D>().size + new Vector2Int(_offset, _offset))));
        while(_countRoom != 0)
        {
            areaRoomsInstatiate.UnionWith(CreateRoomRangeFrom(areaRoomsInstatiate));
        }
    }

    private HashSet<BoundsInt> CreateRoomRangeFrom(HashSet<BoundsInt> areaPlaceRooms)
    {
        _isPlaced = false;
        HashSet<BoundsInt> areaRoomInstatiate = new HashSet<BoundsInt>();
        while(!_isPlaced)
        {
            BoundsInt areaPoint = areaPlaceRooms.ElementAt(0);
            var randomID = Random.Range(0, _rooms.Count);
            var randomPointY = Random.Range(areaPoint.yMin, areaPoint.yMax);
            var randomPointX = Random.Range(areaPoint.xMin, areaPoint.xMax);
            var currentRoom = _rooms[randomID];
            BoxCollider2D checkRoom = GetComponent<BoxCollider2D>();
            checkRoom.size = currentRoom.GetComponent<BoxCollider2D>().size;
            if(checkRoom.IsTouchingLayers(_room))
            {
                break;
            }
            Instantiate(currentRoom, new Vector2(randomPointX, randomPointY), Quaternion.identity);
            _isPlaced = true;
        }
        return areaRoomInstatiate;
    }
}
