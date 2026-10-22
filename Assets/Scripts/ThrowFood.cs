using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ThrowFood : MonoBehaviour
{
    public float throwCooldown;
    public float currentCooldownRemaining;

    public GameObject food;

    public GameObject currentClosestAnimal;
    public List<GameObject> animalsInRange = new List<GameObject>();

    void Start()
    {
        
    }

    void Update()
    {
        if (currentCooldownRemaining > 0)
            currentCooldownRemaining -= Time.deltaTime;

        OrderAnimals();

        if (currentClosestAnimal != null)
        {
            if (currentCooldownRemaining > 0)
                return;

            StartCoroutine(ThrowingFood());
            currentCooldownRemaining = throwCooldown;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Animal") && collision.GetComponent<AnimalBehavior>().hungry)
            animalsInRange.Add(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Animal"))
        {
            animalsInRange.Remove(collision.gameObject);
            
            if (collision.gameObject == currentClosestAnimal)
                currentClosestAnimal = null;
        }
    }

    private void OrderAnimals()
    {
        animalsInRange = animalsInRange.Where(item => item != null).ToList();
        animalsInRange = animalsInRange.OrderBy(item => item.GetComponent<AnimalBehavior>().distanceToPlayer).ToList();

        if (animalsInRange.Count > 0 )
            currentClosestAnimal = animalsInRange[0];
    }

    private IEnumerator ThrowingFood()
    {
        GameObject currentTarget = currentClosestAnimal;

        Vector2 startPos = transform.position;
        Vector2 targetPos = currentTarget.transform.position;
        float z = transform.position.z;

        GameObject foodItem = Instantiate(food, startPos, Quaternion.identity);

        float elapsed = 0f;
        float throwDuration = 0.5f;
        float arcHeight = 2f;

        while (elapsed < throwDuration)
        {
            if (foodItem == null) yield break;

            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / throwDuration);

            Vector2 flatPos = Vector2.Lerp(startPos, targetPos, t);
            float heightOffset = Mathf.Sin(t * Mathf.PI) * arcHeight;

            foodItem.transform.position = new Vector3(flatPos.x, flatPos.y + heightOffset, z);

            yield return null;
        }

        if (foodItem != null)
        {
            foodItem.transform.position = new Vector3(targetPos.x, targetPos.y, z);

            Destroy(foodItem);

            AnimalBehavior animalBehavior = currentTarget.GetComponent<AnimalBehavior>();
            animalBehavior.crumbsEaten++;
            if (!animalBehavior.hungry)
            {
                animalsInRange.Remove(currentTarget);
                currentClosestAnimal = null;
            }
        }
    }
}
