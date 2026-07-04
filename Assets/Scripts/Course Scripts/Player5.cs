using UnityEngine;

public class Player5 : MonoBehaviour
{
    [SerializeField] private Vector3 _targetDestination;
    [SerializeField] private float _speed = 5f;

    void Start()
    {
        _targetDestination = transform.position;
    }

    void Update()
    {
        // Move towards target position

        /*
        // Opcja 1:
        float step = _speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, _targetDestination, step);

        // Check if the position of the player and destination are approximately equal.
        if (Vector3.Distance(transform.position, _targetDestination) < 0.001f)
        {
            // Reset the target position to the original object position.
            _targetDestination = transform.position;
        }
        */

        // Opcja 2:
        float distance = Vector3.Distance(_targetDestination, transform.position);
        if (distance > 0.02f)
        {
            Vector3 direction = _targetDestination - transform.position;
            direction.Normalize();
            transform.Translate(direction * Time.deltaTime * _speed);
        }


    }

    public void SetNewDestination(Vector3 destination)
    {
        _targetDestination = destination;
        Debug.Log("Heading to new destination");
    }
}
