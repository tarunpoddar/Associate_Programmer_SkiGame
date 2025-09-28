using UnityEngine;

public class Snowball : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);
        //if (!collision.gameObject.CompareTag("Slope") )
        //{
        //    gameObject.SetActive(false);
        //}
    }
}
