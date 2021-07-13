using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class DialogueGraphData : ScriptableObject
{
    [SerializeField] private string entryGUID;
    [SerializeReference] private List<NodeData> nodeDatas = new List<NodeData>();
    [SerializeReference] private List<NodeLinkData> nodeLinks = new List<NodeLinkData>();
    [SerializeField] private List<ExposedVariable> exposedVariables = new List<ExposedVariable>();
    [SerializeField] private List<CommentBlockData> commentBlockDatas = new List<CommentBlockData>();


    public void Init(string entryGUID, List<NodeData> nodeDatas, List<NodeLinkData> nodeLinks, List<ExposedVariable> exposedVariables, List<CommentBlockData> commentBlockDatas)
    {
        this.entryGUID = entryGUID;
        this.nodeDatas = nodeDatas;
        this.nodeLinks = nodeLinks;
        this.exposedVariables = exposedVariables;
        this.commentBlockDatas = commentBlockDatas;
    }
    public List<NodeData> GetNextNodes(NodeData nodeData)
    {
        List<NodeData> outputNodeDatas = new List<NodeData>();

        var outputNodeLinks = nodeLinks.Where(x => x.sourceNodeGUID == nodeData.GUID).ToList();
        foreach (var outputNodeLink in outputNodeLinks)
        {
            NodeData outputNodeData = nodeDatas.Find(x => x.GUID == outputNodeLink.destinationNodeGUID);
            if (outputNodeData != null)
            {
                outputNodeDatas.Add(outputNodeData);
            }
        }

        return outputNodeDatas;
    }
    public List<NodeData> GetNextPortNodes(NodeData nodeData, string portName)
    {
        List<NodeData> outputNodeDatas = new List<NodeData>();

        var outputNodeLinks = nodeLinks.Where(x => x.sourceNodeGUID == nodeData.GUID && x.sourcePortName == portName).ToList();
        foreach (var outputNodeLink in outputNodeLinks)
        {
            NodeData outputNodeData = nodeDatas.Find(x => x.GUID == outputNodeLink.destinationNodeGUID);
            if (outputNodeData != null)
            {
                outputNodeDatas.Add(outputNodeData);
            }
        }

        return outputNodeDatas;
    }
    public NodeData GetNodeByGUID(string guid)
    {
        return nodeDatas.FirstOrDefault(x => x.GUID == guid);
    }


    public NodeData FirstNode
    {
        get
        {
            try
            {
                return nodeDatas.First(x => x.GUID == entryGUID);
            }
            catch
            {
                return nodeDatas.Find(x => x is EntryNode);
            }
        }
    }
    public List<NodeData> NodeDatas => nodeDatas;
    public List<NodeLinkData> NodeLinks => nodeLinks;
    public List<ExposedVariable> ExposedVariables => exposedVariables;
    public List<CommentBlockData> CommentBlockDatas => commentBlockDatas;
}