using UnityEngine;

public enum Direction //the direction the corridor is heading;
{
    North, East, South, West
}

public class Corridor
{
    public int startXPos;  // x coordinate for the start of the corridor
    public int startYPos; //y coordinate for the start of the corridor
    public int corridorLength; //how long the corridor is;
    public Direction direction; // which direction the corridor is heading from its room;

    //Get the end position of the corridor based on its start position and which direction it's heading
    public int EndPositionX
    {
        get
        {
            if(direction == Direction.North || direction == Direction.South)
            {
                return startXPos;
            }
            if(direction == Direction.East)
            {
                return startXPos + corridorLength - 1;
            }
            else
            {
                return startXPos - corridorLength + 1;
            }
        }
    }
	
    public int EndPositionY
    {
        get
        {
            if(direction == Direction.East || direction == Direction.West)
            {
                return startYPos;
            }
            if(direction == Direction.North)
            {
                return startYPos + corridorLength - 1;
            }
            else
            {
                return startYPos - corridorLength + 1;
            }
        }
    }

    public void SetUpCorridor(Room room, IntRange length, IntRange roomWidth, IntRange roomHeight, int columns, int rows, bool firstCorridor)
    {
        //setUp a random Direction
        direction = (Direction)Random.Range(0, 4);

        //find the opposite direction of the one chosen previously
        Direction oppositeDirection = (Direction)(((int)room.enteringCorridor + 2) % 4);

        //change the direction by pi/2 if !firstCorridor and the direction is opposite to the previous corridor (we don 't want a dungeon which go back to back
        if(!firstCorridor && direction == oppositeDirection)
        {
            int directionInt = (int)direction;
            directionInt++;
            directionInt = directionInt % 4;
            direction = (Direction)directionInt;
        }

        //Set a random length
        corridorLength = length.Random;

        int maxLength = length.m_Max; //create a cap for how long the length can be

        switch(direction)
        {
            case Direction.North:
                startXPos = Random.Range(room.xPos, room.xPos + room.roomWidth - 1); //set the StartXpos randomly within the room(important last part)
                startYPos = room.yPos + room.roomHeight; // set the startYPos fixed at the top of the room;
                maxLength = rows - startYPos - roomHeight.m_Min; // max length ca be the height of the board - the top of the room  problem there room.roomHeight
                break;

            case Direction.East:
                startXPos = room.xPos + room.roomWidth;
                startYPos = Random.Range(room.yPos, room.yPos + room.roomHeight - 1);
                maxLength = columns - startXPos - roomWidth.m_Min;
                break;

            case Direction.South:
                startXPos = Random.Range(room.xPos, room.xPos + room.roomWidth);
                startYPos = room.yPos;
                maxLength = startYPos - roomHeight.m_Min;
                break;

            case Direction.West:
                startXPos = room.xPos;
                startYPos = Random.Range(room.yPos, room.yPos + room.roomHeight);
                maxLength = startXPos - roomWidth.m_Min;
                break;
        }

        corridorLength = Mathf.Clamp(corridorLength, 1, maxLength);  //make sure the corridor don t go offboard
    }
}
