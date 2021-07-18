using System;

[Serializable]
public class NodeLinkData
{
    public string sourceNodeGUID;
    public string sourcePortName;
    public string destinationNodeGUID;


    public NodeLinkData()
    {

    }
    public NodeLinkData(string sourceNodeGUID, string sourcePortName, string destinationNodeGUID)
    {
        this.sourceNodeGUID = sourceNodeGUID;
        this.sourcePortName = sourcePortName;
        this.destinationNodeGUID = destinationNodeGUID;
    }
}