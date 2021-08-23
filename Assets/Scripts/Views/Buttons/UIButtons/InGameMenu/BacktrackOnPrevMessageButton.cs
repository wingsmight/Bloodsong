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
        [SerializeField] private LocationNodeView locationNodeView;
        [SerializeField] private ShowCharacterNodeView showCharacterNodeView;
        [SerializeField] private HideCharacterNodeView hideCharacterNodeView;


        private Coroutine disableActionCoroutine;


        private void Start()
        {
            EnableAction();
        }
        private void FixedUpdate()
        {
            button.interactable = IsAvailable(gameDayControl.BranchNodes) && !raycastBlock.activeInHierarchy;
        }


        public void EnableAction()
        {
            if (disableActionCoroutine != null)
            {
                StopCoroutine(disableActionCoroutine);
            }

            button.interactable = true;
            raycastBlock.SetActive(false);
        }
        public void DisableAction(float duration)
        {
            DisableAction();

            if (disableActionCoroutine != null)
            {
                StopCoroutine(disableActionCoroutine);
            }
            disableActionCoroutine = StartCoroutine(DisableActionRoutine(duration));
        }
        public void DisableAction()
        {
            button.interactable = false;
            raycastBlock.SetActive(true);
        }

        protected override void OnClick()
        {
            var graph = storyGraphParser.CurrentDialogue;
            if (graph == null || storyGraphParser.CurrentNode == null)
                return;

            var branchNodes = gameDayControl.BranchNodes;
            if (!IsAvailable(branchNodes))
                return;

            NodeData node = branchNodes.Pop();
            storyGraphParser.StopNode(node);
            if (node is MonologueNode)
            {
                if (gameDayControl.CurrentStoryPhraseIndex > 0)
                {
                    branchNodes.Push(node);
                    monologueNodeView.Act(graph, node as MonologueNode, --gameDayControl.CurrentStoryPhraseIndex);

                    return;
                }
            }
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

                    DisableAction(specificNodeExecutionSeconds);
                }

                storyGraphParser.CurrentNode = node;
            } while (!(node is MonologueNode || node is SplitMessageNode));

            if (node is MonologueNode)
            {
                if (gameDayControl.CurrentStoryPhraseIndex == 0)
                {
                    branchNodes.Push(node);
                    gameDayControl.CurrentStoryPhraseIndex = (node as MonologueNode).texts.Count - 1;
                    monologueNodeView.Act(graph, node as MonologueNode, gameDayControl.CurrentStoryPhraseIndex);
                }
                else if (gameDayControl.CurrentStoryPhraseIndex > 0)
                {
                    branchNodes.Push(node);
                    monologueNodeView.Act(graph, node as MonologueNode, --gameDayControl.CurrentStoryPhraseIndex);
                }
                else
                {
                    Debug.LogError("ELSE");

                    var lastNode = branchNodes.Last;
                    if (lastNode is MonologueNode)
                    {
                        gameDayControl.CurrentStoryPhraseIndex = (lastNode as MonologueNode).texts.Count - 1;
                    }
                    branchNodes.Push(lastNode);
                    OnClick();
                }
            }
            else
            {
                storyGraphParser.Parse(graph, node);
            }
        }

        private IEnumerator DisableActionRoutine(float duration)
        {
            yield return new WaitForSeconds(duration);

            EnableAction();
        }
        private bool IsAvailable(BranchNodesStack branchNodes)
        {
            var a1 = branchNodes.Count > 1;
            var a2 = branchNodes.Last is MonologueNode && gameDayControl.CurrentStoryPhraseIndex > 0;
            var a3 = branchNodes.IsPrevNodesSpecific();

            return a1 && (!a3 || a2);
        }
    }
}