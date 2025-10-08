using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Test : MonoBehaviour
{

    private Vector3 startPos;
    private Vector3 targetPos;
    private bool goUp;
    private float speed = 0.5f;
    public GameObject fruit;
    public Vector3 spawnPos;

    void Start()
    {
        startPos = transform.position;
        targetPos = startPos + new Vector3(0, 0.2f, 0); 
        goUp = true;
    }

    void Update()
    {
        Vector3 currentTarget = goUp ? targetPos : startPos;

        transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);

        if (transform.position == currentTarget)
        {
            goUp = !goUp;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(fruit, spawnPos, Quaternion.identity);
        }
    }
}
