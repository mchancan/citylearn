using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class CityLearnAcademy : Academy
{
    public int num_i = 4;
    
    public override void AcademyReset(){
        num_i = (int)resetParameters["num_imgs"];
    }
}
