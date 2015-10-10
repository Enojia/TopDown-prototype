using UnityEngine;
using System.Collections;

public class BoardSetUp : MonoBehaviour
{
    public enum TileType
    {
        Wall, Floor,
    }

    public int columns = 100;    //width of the board
    public int rows = 100;      //height of the board
    public IntRange numRooms = new IntRange(10, 20); // pseudoRandom Number of Rooms
    public IntRange roomWidth = new IntRange(3, 10); // pseudoRandom width of a room
    public IntRange corridorLength = new IntRange(3, 10); // pseudoRandom length of a corridor

    public GameObject[] floorTiles;  //prefabs array
    public GameObject[] wallTiles;
    public GameObject[] outerWallTiles;
    public GameObject[] player;       //prefab array;

    private TileType[][] tiles;//jagged array of wall or floor
    private Room[] rooms; // all rooms for this boards
    private Corridor[] corridors; // all corridors for this boards
    private GameObject boardHolder; //container for all tiles  

	// Use this for initialization
	void Start ()
    {
	    
	}
	
	void SetupTilesArray() //init the tiles jagged array
    {
        tiles = new TileType[columns][]; //set the tiles to the right width

        for (int i = 0; i < tiles.Length; i ++)
        {
            tiles[i] = new TileType[rows]; //set the array in the array
        }
    }

    void CreateRoomsAndCorridors()
    {
        rooms = new Room(numRooms.Random); //setup the rooms array;

        corridors = new Corridor(rooms.Length - 1); // one less corridor than the rooms so that we didn't finish on a corridor;

        rooms[0] = new Room();
        corridors[0] = new Corridor(); //first room and first corridor


    }
}
