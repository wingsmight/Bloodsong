using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDayOrder : MonoBehaviour
{
    [SerializeField] private List<Story> stories;


    public List<Story> Stories => stories;
}
