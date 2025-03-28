using UnityEngine;

public class Point : MonoBehaviour
{
    [SerializeField]
    private int pointValue = 10;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Pacman"))
        {
            // Add points to the score via GameManager
            GameManager.Instance.AddPoints(pointValue);

            Debug.Log("Pacman ate a point worth " + pointValue + " points!");
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
