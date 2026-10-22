using UnityEngine;

public class AnimalBehavior : MonoBehaviour
{
    private GameObject player;

    public bool hungry;
    public int crumbsNeeded;
    public int crumbsEaten;
    public float moveSpeed;

    public float distanceToPlayer;
    public float detectionRange;

    void Start()
    {
        player = FindAnyObjectByType<PlayerMovement>().gameObject;
        hungry = true;
    }

    void Update()
    {
        if (hungry)
        {
            distanceToPlayer = Vector2.Distance(player.transform.position, transform.position);

            if (distanceToPlayer < detectionRange)
                MoveAnimal();

            if (crumbsNeeded <= crumbsEaten)
            {
                hungry = false;
                transform.Find("LoveParticles").gameObject.SetActive(true);
            }
        }
    }

    private void MoveAnimal()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
    }
}
