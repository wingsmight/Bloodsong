using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsetStatViews : MonoBehaviour
{
    [SerializeField] private UserStatView moneyView;
    [SerializeField] private UserStatView reputationView;
    [SerializeField] private CareerStatView careerView;
    [SerializeField] private UserStatView relationshipView;


    public void Start()
    {
        moneyView.Init(Storage.GetData<PlayerStats>().money);
        reputationView.Init(Storage.GetData<PlayerStats>().reputation);
        careerView.Init(Storage.GetData<PlayerStats>().career);
        relationshipView.Init(Storage.GetData<PlayerStats>().relationship);
    }
}
