using UnityEngine;

public class oscillator : MonoBehaviour
{
    Vector3 startPosition;
    Vector3 endPosition;
    [SerializeField] float speed = 1.0f;
    [SerializeField] Vector3 MovementVector;
    float movementFactor;
    void Start()
    {
        startPosition = transform.position;
        endPosition = startPosition + MovementVector;
    }


    void Update()
    {
        movementFactor = Mathf.PingPong(Time.time * speed, 1f);
        transform.position = Vector3.Lerp(startPosition, endPosition, movementFactor);
    }
}
