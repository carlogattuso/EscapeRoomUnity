using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;      //Tells Random to use the Unity Engine random number generator.

public class BoardManager : MonoBehaviour
{
    private int columns = 0;                                        
    private int rows = 0;   

    //Outer blocking walls
    public GameObject[] topOuterWalls;
    public GameObject[] leftOuterWalls;
    public GameObject[] rightOuterWalls;
    public GameObject[] bottomOuterWalls;
    public GameObject[] roundBottomOuterWalls;
    public GameObject bottomLeftCorner;
    public GameObject bottomRightCorner;
    public GameObject cornerRight;
    public GameObject cornerLeft;
    public GameObject roundCornerLeft;
    public GameObject roundCornerRight;
    public GameObject flagOuterWall;

    //Floors
    public GameObject[] floors;

    //Outermap
    public GameObject outerMap;

    //Items
    public GameObject blueDoor;
    public GameObject redDoor;
    public GameObject yellowDoor;
    public GameObject exit;
    public GameObject enemy;
    public GameObject candle;

    private Transform boardHolder;                                  
    private List<Vector3> gridPositions = new List<Vector3>();   

    void InitialiseList()
    {
        gridPositions.Clear();

        for (int x = 1; x < columns - 1; x++)
        {
            for (int y = 1; y < rows - 1; y++)
            {
                gridPositions.Add(new Vector3(x, y, 0f));
            }
        }
    }

    char[] RotateLevel(char[] level, int dimension)
    {
        char[] tmp = new char[level.Length];

        for (int i = 0, pos; i < tmp.Length; i++)
        {
            pos = dimension * (dimension - Convert.ToInt32(Math.Floor((double)(i / dimension))) - 1) + i % dimension;
            tmp[i] = level[pos];
        }

        return tmp;
    }

    void BoardSetupFromString(string levelString, int dimension)
    {
        boardHolder = new GameObject("Board").transform;
        GameObject toInstantiate;

        char[] level = RotateLevel(levelString.ToCharArray(),dimension);

        for (int i = 0; i < level.Length; i++)
        {
            switch (level[i])
            {
                //Blocking walls
                case 'T':
                    toInstantiate = topOuterWalls[Random.Range(0, topOuterWalls.Length)];
                    Instantiate(toInstantiate, dimension, i);
                    break;
                case 'L':
                    toInstantiate = leftOuterWalls[Random.Range(0, leftOuterWalls.Length)];
                    Instantiate(toInstantiate, dimension, i);
                    break;
                case 'R':
                    toInstantiate = rightOuterWalls[Random.Range(0, rightOuterWalls.Length)];
                    Instantiate(toInstantiate, dimension, i);
                    break;
                case 'B':
                    toInstantiate = bottomOuterWalls[Random.Range(0, bottomOuterWalls.Length)];
                    Instantiate(toInstantiate, dimension, i);
                    break;
                case 'w':
                    toInstantiate = roundBottomOuterWalls[Random.Range(0, roundBottomOuterWalls.Length)];
                    Instantiate(toInstantiate, dimension, i);
                    break;
                case 'l':
                    toInstantiate = bottomLeftCorner;
                    Instantiate(toInstantiate, dimension, i);
                    break;
                case 'r':
                    toInstantiate = bottomRightCorner;
                    Instantiate(toInstantiate, dimension, i);
                    break;
                case 'E':
                    toInstantiate = roundCornerRight;
                    Instantiate(toInstantiate, dimension, i);
                    break;
                case 'e':
                    toInstantiate = roundCornerLeft;
                    Instantiate(toInstantiate, dimension, i);
                    break;
                case 'C':
                    toInstantiate = cornerRight;
                    Instantiate(toInstantiate, dimension, i);
                    break;
                case 'c':
                    toInstantiate = cornerLeft;
                    Instantiate(toInstantiate, dimension, i);
                    break;
                case 'O':
                    toInstantiate = outerMap;
                    Instantiate(toInstantiate, dimension, i);
                    break;
                case 'F':
                    toInstantiate = floors[Random.Range(0, floors.Length)];
                    Instantiate(toInstantiate, dimension, i);
                    break;
                case 'f':
                    toInstantiate = flagOuterWall;
                    Instantiate(toInstantiate, dimension, i);
                    break;
                case 'b':
                    //Background
                    break;
                default:
                    toInstantiate = floors[Random.Range(0, floors.Length)];
                    Instantiate(toInstantiate, dimension, i);
                    break;
            }
            switch (level[i])
            {
                case '1':
                    toInstantiate = redDoor;
                    Instantiate(toInstantiate, dimension, i);
                    break;
                case '2':
                    toInstantiate = blueDoor;
                    Instantiate(toInstantiate, dimension, i);
                    break;
                case '3':
                    toInstantiate = yellowDoor;
                    Instantiate(toInstantiate, dimension, i);
                    break;
                case 'N':
                    toInstantiate = enemy;
                    Instantiate(toInstantiate, dimension, i);
                    break;
                case 'X':
                    toInstantiate = exit;
                    Instantiate(toInstantiate, dimension, i);
                    break;
                case 'V':
                    toInstantiate = candle;
                    Instantiate(toInstantiate, dimension, i);
                    break;
            }
        }
    }

    public void Instantiate(GameObject toInstantiate, float dimension, int i)
    {
        GameObject instance;
        instance = Instantiate(toInstantiate, new Vector3(i % dimension - 1, Convert.ToInt32(Math.Floor((double)(i / dimension))) - 1, 0f), Quaternion.identity) as GameObject;
        instance.transform.SetParent(boardHolder);
    }

    public void SetupScene(string level, int dimension)
    {
        //Reset our list of gridpositions.
        InitialiseList();

        BoardSetupFromString(level,dimension);
    }
}
