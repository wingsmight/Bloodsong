using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoryMenu
{
    public class BacktrackOnPrevMessageButton : StoryMenuButton
    {
        private static readonly Type[] messageNodeTypes =
        {
            typeof(MonologueNode),
            typeof(SplitMessageNode),
        };


        [SerializeField] private float specificNodeExecutionSeconds;
        [Space(12)]
        [SerializeField] private GameObject raycastBlock;
        [SerializeField] private DialogueGraphParser storyGraphParser;
        [SerializeField] private GameDayControl gameDayControl;
        [SerializeField] private StartMonologueNodeView monologueNodeView;
        [SerializeField] private StartSplitMessageNodeView splitMessageNodeView;
        [SerializeField] private LocationNodeView locationNodeView;
        [SerializeField] private ShowCharacterNodeView showCharacterNodeView;
        [SerializeField] private HideCharacterNodeView hideCharacterNodeView;


        private void Start()
        {
            EnableAction();
        }


        protected override void OnClick()
        {
            var graph = storyGraphParser.CurrentDialogue;
            if (graph == null || storyGraphParser.CurrentNode == null)
                return;

            var branchNodes = Storage.GetData<GameDayData>().branchNodes;
            if (branchNodes.Count <= 1 || (branchNodes.Count <= 2 && branchNodes.First is LocationNode))
                return;

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
                    if (node is LocationNode)
                    {
                        locationNodeView.ActWithoutProcessNext(graph, node as LocationNode);
                    }
                    else if (node is CharacterNode)
                    {
                        showCharacterNodeView.ActWithoutProcessNext(graph, node as CharacterNode);
                    }
                    else if (node is CharacterPositionNode)
                    {
                        hideCharacterNodeView.ActWithoutProcessNext(graph, node as CharacterPositionNode);
                    }

                    DisableAction();
                    DelayExecutor.Instance.Execute(specificNodeExecutionSeconds, EnableAction);
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

        private void EnableAction()
        {
            button.interactable = true;
            raycastBlock.SetActive(false);
        }
        private void DisableAction()
        {
            button.interactable = false;
            raycastBlock.SetActive(true);
        }
    }
}