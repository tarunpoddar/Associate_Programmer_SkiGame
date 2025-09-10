using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagCheck : MonoBehaviour
{
    public enum Direction {Left, Right};
    public Direction passingDirecion;
    public Material passedFlagMat, failedFlagMat;

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            float dirCheck = transform.position.x + other.transform.position.x;

            if (passingDirecion == Direction.Left)
            {
                if (other.transform.position.x < transform.position.x)
                {
                    passSuccesful();
                }
                else
                {
                    passUnSuccesful();
                }
            }

            if (passingDirecion == Direction.Right)
            {
                if (other.transform.position.x > transform.position.x)
                {
                    passSuccesful();
                }
                else
                {
                    passUnSuccesful();
                }
            }
        }

    }

    private void passSuccesful()
    {
        GetComponent<MeshRenderer>().material = passedFlagMat;
    }

    private void passUnSuccesful()
    {
        GetComponent<MeshRenderer>().material = failedFlagMat;
        
        //If we miss the Flag add one second onto the race time
        RaceTimer.time += 1;
    }
}
