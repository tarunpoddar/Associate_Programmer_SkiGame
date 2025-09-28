using Assets.Scripts;
using UnityEngine;

public class SnowmanThrow : MonoBehaviour
{
    public float throwDistance;
    public int throwSpeed;

    private GameObject target;
    private Vector3 throwY;
    private bool justThown = false;
    private int frameInterval = 5;

    private ObjectPoolManager pool;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player");
        throwY = new Vector3(0, 0.33f, 0);
        pool = GameObject.Find("GameManager").GetComponent<ObjectPoolManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.frameCount % frameInterval == 0)
        {
            float distanceToTarget = Vector3.Distance(target.transform.position, transform.position);

            if (distanceToTarget < throwDistance && !justThown)
            {
                justThown = true;
                
                GameObject tempSnowBall = pool.GetObject();
                tempSnowBall.transform.position = transform.position;
                tempSnowBall.transform.rotation = transform.rotation;
                
                Rigidbody tempRb = tempSnowBall.GetComponent<Rigidbody>();
                Vector3 targetDirection = Vector3.Normalize(target.transform.position - transform.position);

                //Add a small throw angle
                targetDirection += throwY;
                tempRb.AddForce(targetDirection * throwSpeed);
                Invoke("ThrowOver", 0.1f);
            }
        }
    }

    void ThrowOver()
    {
        justThown = false;
    }
}
