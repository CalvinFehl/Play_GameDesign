using Unity.VisualScripting;
using UnityEngine;

public class AnimalBehavior : MonoBehaviour
{
    private GameObject player;

    public bool hungry;
    public int crumbsNeeded;
    public float moveSpeed;

    public float distanceToPlayer;
    public float detectionRange;

    void Start()
    {
        hungry = true;

        player = FindAnyObjectByType<PlayerMovement>().gameObject;
    }

    void Update()
    {
        distanceToPlayer = Vector2.Distance(player.transform.position, transform.position);

        if (distanceToPlayer < detectionRange)
            MoveAnimal();
    }

    private void MoveAnimal()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
    }
}
