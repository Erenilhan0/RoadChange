using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPoint : MonoBehaviour
{
    public int maxChangeAmount;
    public int changeAmount;

    public void ChangePosition(bool upwards,float moveAmount)
    {
        if (upwards)
        {
            if(changeAmount >= maxChangeAmount) return;

            changeAmount++;
            transform.localPosition += new Vector3(0, moveAmount, 0);
        }
        else
        {
            if(changeAmount <= -maxChangeAmount) return;

            changeAmount--;
            transform.localPosition -= new Vector3(0, moveAmount, 0);
        }

    }
}
