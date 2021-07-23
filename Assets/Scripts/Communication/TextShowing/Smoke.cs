using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private List<RectTransform> subTransforms;


    public void Appear(Vector2 position)
    {
        subTransforms.ForEach(x => x.position = new Vector3(position.x, position.y, x.position.z));
        animator.Play("Appear", 0, 0);
    }
}
