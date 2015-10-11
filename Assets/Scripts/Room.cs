using UnityEngine;

public class Room
{
    public int xPos; //x coordinate lower left tile of the room
    public int yPos; //y coordinate lower left tile of the room
    public int roomWidth; //width of the room in tiles;
    public int roomHeight; //height of the room in tiles;
    public Direction enteringCorridor; //direction of the previous corridor

    public void SetUpRoom(IntRange widthRange, IntRange heightRange, int columns, int rows) //setUp the first room
    {
        roomWidth = widthRange.Random;
        roomHeight = heightRange.Random;

        //set the x and y coordinates so that the first room is in the middle of the board
        xPos = Mathf.RoundToInt(columns / 2f - roomWidth / 2);
        yPos = Mathf.RoundToInt(rows / 2f - roomWidth / 2);
    }

    public void SetUpRoom(IntRange widthRange, IntRange heightRange, int columns, int rows, Corridor corridor)
    {
        //assign the enteringCorridor direction
        enteringCorridor = corridor.direction;

        //set random values for width and height
        roomWidth = widthRange.Random;
        roomHeight = heightRange.Random;

        switch(corridor.direction)
        {
            case Direction.North:
                roomHeight = Mathf.Clamp(roomHeight, 1, rows - corridor.EndPositionY);
                yPos = corridor.EndPositionY;
                xPos = Random.Range(corridor.EndPositionX - roomWidth + 1, corridor.EndPositionX);
                xPos = Mathf.Clamp(xPos, 0, columns - roomWidth);
                break;

            case Direction.East:
                roomWidth = Mathf.Clamp(roomWidth, 1, columns - corridor.EndPositionX);
                xPos = corridor.EndPositionX;
                yPos = Random.Range(corridor.EndPositionY - roomHeight + 1, corridor.EndPositionY);
                yPos = Mathf.Clamp(yPos, 0, rows - roomHeight);
                break;

            case Direction.South:
                roomHeight = Mathf.Clamp(roomHeight, 1, corridor.EndPositionY); // come b here to understand otherwise it goes to far
                yPos = corridor.EndPositionY - roomHeight + 1;
                xPos = Random.Range(corridor.EndPositionX - roomWidth + 1, corridor.EndPositionX);
                xPos = Mathf.Clamp(xPos, 0, columns - roomWidth);
                break;

            case Direction.West:
                roomWidth = Mathf.Clamp(roomWidth, 1, corridor.EndPositionX); // begin at origin 0 0 0
                xPos = corridor.EndPositionX - roomWidth;
                yPos = Random.Range(corridor.EndPositionY - roomHeight + 1, corridor.EndPositionY);
                yPos = Mathf.Clamp(yPos, 0, rows - roomHeight);
                break;
        }
    }
	
}
