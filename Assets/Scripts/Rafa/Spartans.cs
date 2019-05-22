using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spartans : Army
{
    Rigidbody2D rb;

    private void Start() 
    {
        rb = GetComponent<Rigidbody2D>();    
    }

    private void FixedUpdate() 
    {
        
        UpdateMass();



    }
    
    private void UpdateMass()
    {
        rb.mass = nTroops * troopWeight * massModifier;
        
    }

}
