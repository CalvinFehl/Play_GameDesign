using UnityEngine;

public class AnimalBehavior : MonoBehaviour
{
    private GameObject player;

    public bool hungry;
    public float crumbsNeeded;
    public float crumbsEaten;
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
        }
    }


    public bool EatFood(float foodAmount = 0f)
    {
        crumbsEaten += foodAmount; 
        
        if (crumbsNeeded <= crumbsEaten)
        {
            hungry = false;
            transform.Find("LoveParticles").gameObject.SetActive(true);
            return true;
        }

        return false;
    }

    private void MoveAnimal()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
    }
}
