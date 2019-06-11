using System;
public class Object
{
    private String type;
    private String attribute;

    public Object(String type, String attribute)
    {
        this.type = type;
        this.attribute = attribute;
    }

    public String getType()
    {
        return this.type;
    }

    public String getAttribute()
    {
        return this.attribute;
    }

    public void setType(String type)
    {
        this.type = type;
    }

    public void setAttribute(String attribute)
    {
        this.attribute = attribute;
    }
}
