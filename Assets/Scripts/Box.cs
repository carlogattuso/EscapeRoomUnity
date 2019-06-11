using System;
using UnityEngine;

public class Box
{
    private Vector2 position;
    private string attribute;

    public Box(int positionX, int positionY, string attribute)
    {
        this.position.x = positionX;
        this.position.y = positionY;
        this.attribute = attribute;
    }

    public Vector2 getPosition()
    {
        return this.position;
    }

    public void setPosition(int positionX, int positionY)
    {
        this.position.x = positionX;
        this.position.y = positionY;
    }

    public string getAttribute()
    {
        return this.attribute;
    }
}
