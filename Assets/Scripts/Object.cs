using System;
public class Object
{
    private String type;
    private String name;
    private String attribute;

    public Object(String type, String name, String attribute)
    {
        this.type = type;
        this.name = name;
        this.attribute = attribute;
    }

    public String getType()
    {
        return this.type;
    }

    public String getName()
    {
        return this.name;
    }

    public String getAttribute()
    {
        return this.attribute;
    }

    public void setType(String type)
    {
        this.type = type;
    }

    public void setName(String name)
    {
        this.name = name;
    }

    public void setAttribute(String attribute)
    {
        this.attribute = attribute;
    }
}
