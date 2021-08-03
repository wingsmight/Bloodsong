using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoryMenu
{
    public class BacktrackOnPrevMessageButton : StoryMenuButton
    {
        [SerializeField] private DialogueGraphParser storyGraphParser;
        [SerializeField] private GameDayControl gameDayControl;
        [SerializeField] private StartMonologueNodeView monologueNodeView;


        protected override void OnClick()
        {
            var graph = storyGraphParser.CurrentDialogue;
            if (graph == null || storyGraphParser.CurrentNode == null)
                return;

            var branchNodes = Storage.GetData<GameDayData>().branchNodes;
            NodeData node = branchNodes.Pop();
            storyGraphParser.StopNode(node);
            do
            {
                if (branchNodes.Count > 0)
                {
                    node = branchNodes.Pop();
                }
                else
                {
                    return;
                }

                if (BranchNodesStack.IsSpecificNode(node))
                {
                    storyGraphParser.Parse(graph, node);
                    branchNodes.Pop();
                }

                storyGraphParser.CurrentNode = node;
            } while (!(node is MonologueNode || node is SplitMessageNode));

            if (node is MonologueNode && gameDayControl.CurrentStoryPhraseIndex > 0)
            {
                monologueNodeView.Act(graph, node as MonologueNode, --gameDayControl.CurrentStoryPhraseIndex);
            }
            else
            {
                storyGraphParser.Parse(graph, node);
            }
        }
    }
}