using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Army : MonoBehaviour
{

    protected EnumArmyStates currentState;
    [SerializeField] protected int nTroops;
    [SerializeField] protected float speed;

    // How much mass is each soldier worth
    [SerializeField] protected float troopWeight;


    protected float massModifier;

}
