using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map
{
    private string levelString;
    private int dimension;
    private List<Box> boxes = new List<Box>();

    public Map(string levelString, int dimension, List<Box> boxes)
    {
        this.levelString = levelString;
        this.boxes = boxes;
        this.dimension = dimension;
    }

    public string getLevelString()
    {
        return this.levelString;
    }

    public void setLevelString(string levelString)
    {
        this.levelString = levelString;
    }

    public List<Box> getBoxes()
    {
        return this.boxes;
    }

    public void setBoxes(List<Box> boxes)
    {
        this.boxes = boxes;
    }

    public int getDimension()
    {
        return this.dimension;
    }

    public void setDimension(int dimension)
    {
        this.dimension = dimension;
    }
}
