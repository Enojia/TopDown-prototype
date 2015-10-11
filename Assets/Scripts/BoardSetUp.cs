using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
    public IntRange roomHeight = new IntRange(3, 10);
    public IntRange corridorLength = new IntRange(6, 10); // pseudoRandom length of a corridor
    public IntRange numEnemies = new IntRange(1, 5);

    public GameObject[] floorTiles;  //prefabs array
    public GameObject[] wallTiles;
    public GameObject[] ennemies;
    //public GameObject[] outerWallTiles;  implement later if necessary
    public GameObject player;       //prefab 
    public GameObject Exit;
    public List<Vector3> possiblePos;

    private TileType[][] tiles;//jagged array of wall or floor
    private Room[] rooms; // all rooms for this boards
    private Corridor[] corridors; // all corridors for this boards
    private GameObject boardHolder; //container for all tiles  

    // Use this for initialization
    public void SetUpScene()
    {
        boardHolder = new GameObject("BoardHolder");

        SetupTilesArray(); //init the jagged array

        CreateRoomsAndCorridors(); //create all the rooms and corridors

        SetTilesValuesForRoom(); //set floor type to all the tiles in a room

        setTileValueForCorridors(); //set floor type to all the tiles in corridors

        instantiateTiles();

        instantiateEnemy(ennemies);
    }

    void SetupTilesArray() //init the tiles jagged array
    {
        tiles = new TileType[columns][]; //set the tiles to the right width

        for (int i = 0; i < tiles.Length; i++)
        {
            tiles[i] = new TileType[rows]; //set the array in the array
        }
    }

    void CreateRoomsAndCorridors()
    {
        rooms = new Room[numRooms.Random]; //setup the rooms array;

        corridors = new Corridor[rooms.Length - 1]; // one less corridor than the rooms so that we didn't finish on a corridor;

        rooms[0] = new Room();
        corridors[0] = new Corridor(); //first room and first corridor

        //setUp first room
        rooms[0].SetUpRoom(roomWidth, roomHeight, columns, rows);

        //setUp the first corridor using the first room
        corridors[0].SetUpCorridor(rooms[0], corridorLength, roomWidth, roomHeight, columns, rows, true);

        int RandomRoomIndex = Random.Range(1, rooms.Length);
        int RandomIndexExit = (RandomRoomIndex + 3) % rooms.Length;

        //setUp the other rooms and corridors
        for (int i = 1; i < rooms.Length; i++)
        {
            //create a room
            rooms[i] = new Room();

            //setUp the room based on the previous corridor
            rooms[i].SetUpRoom(roomWidth, roomHeight, columns, rows, corridors[i - 1]);

            if (i < corridors.Length)
            {
                corridors[i] = new Corridor();

                corridors[i].SetUpCorridor(rooms[i], corridorLength, roomWidth, roomHeight, columns, rows, false);
            }

            if (i == RandomRoomIndex) //better computation with  *
            {
                Vector3 playerPos = new Vector3(rooms[i].xPos, rooms[i].yPos, 0);
                Instantiate(player, playerPos, Quaternion.identity);
            }

            if (i == RandomIndexExit)
            {
                IntRange randomPosX = new IntRange(rooms[i].xPos, rooms[i].xPos + rooms[i].roomWidth - 1);
                IntRange randomPosY = new IntRange(rooms[i].yPos, rooms[i].yPos + rooms[i].roomHeight - 1);
                Vector3 ExitPos = new Vector3(randomPosX.Random, randomPosY.Random, 0);
                Instantiate(Exit, ExitPos, Quaternion.identity);
            }



        }
    }

    void SetTilesValuesForRoom()
    {
        for (int i = 0; i < rooms.Length; i++)
        {
            Room currentRoom = rooms[i];

            for (int j = 0; j < currentRoom.roomWidth; j++)
            {
                int xCoord = currentRoom.xPos + j;
                for (int k = 0; k < currentRoom.roomHeight; k++)
                {
                    int yCoord = currentRoom.yPos + k;

                    tiles[xCoord][yCoord] = TileType.Floor;

                    possiblePos.Add(new Vector3(xCoord, yCoord, 0));
                }
            }
        }
    }

    void setTileValueForCorridors()
    {
        for (int i = 0; i < corridors.Length; i++)
        {
            Corridor currentCorridor = corridors[i];

            for (int j = 0; j < currentCorridor.corridorLength; j++)
            {
                int xCoord = currentCorridor.startXPos;
                int yCoord = currentCorridor.startYPos;

                switch (currentCorridor.direction) // one of the four certainly
                {
                    case Direction.North:
                        yCoord += j;
                        break;

                    case Direction.East:
                        xCoord += j;
                        break;

                    case Direction.South:
                        yCoord -= j;
                        break;

                    case Direction.West:
                        xCoord -= j;
                        break;
                }

                tiles[xCoord][yCoord] = TileType.Floor;
            }
        }
    }

    void instantiateTiles() //in all the tiles jagged array
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            for (int j = 0; j < tiles.Length; j++)
            {
                instantiateFromArray(floorTiles, i, j);  //fill all with floor

                if (tiles[i][j] == TileType.Wall)  //fill all walls with walls
                {
                    instantiateFromArray(wallTiles, i, j);
                }
            }
        }
    }

    void instantiateFromArray(GameObject[] prefabs, float xCoord, float yCoord)
    {
        int randomIndex = Random.Range(0, prefabs.Length);

        Vector3 position = new Vector3(xCoord, yCoord, 0);
        GameObject tileInstance = (GameObject)Instantiate(prefabs[randomIndex], position, Quaternion.identity);
        tileInstance.transform.SetParent(boardHolder.transform);
    }

    void instantiateEnemy(GameObject[] enemies)
    {
        int randomNum = numEnemies.Random; // number of enemies
        for (int i = 0; i<randomNum; i ++)
        {
            int randomPosIndex = Random.Range(0, possiblePos.Count);
            Vector3 currentVect = possiblePos[randomPosIndex];

            if (player.transform.position == currentVect)
            {
                possiblePos.Remove(currentVect);
            }

            else
            {
                instantiateFromArray(enemies, currentVect.x, currentVect.y);
                possiblePos.Remove(currentVect);
                Debug.Log("been instantiated");
            }
        }


    }
}
