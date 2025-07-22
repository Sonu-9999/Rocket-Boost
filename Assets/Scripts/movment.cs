using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class movment : MonoBehaviour
{
    [SerializeField] InputAction thrust; //It detects a single continuous input action, like a button press or joystick movement
    [SerializeField] InputAction rotation;
    [SerializeField] AudioClip mainengine; //Audio clip for the main engine sound
    [SerializeField] float thrustspeed = 10f; //Speed of the object
    [SerializeField] float rotationspeed = 10f; //Speed of the object
    [SerializeField] ParticleSystem mainthrustparticle;
    [SerializeField] ParticleSystem leftthrustparticle;
    [SerializeField] ParticleSystem rightthrustparticle;
    Rigidbody rb;
    AudioSource audioSource;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }
    void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
        

    }
    void FixedUpdate()
    {
        processthrust();
        processrotation();
    }

    private void processthrust()
    {
        if (thrust.IsPressed())
        {

            rb.AddRelativeForce(Vector3.up * thrustspeed * Time.fixedDeltaTime);
            //AddRelativeForce applies a force to the Rigidbody in the direction of its local forward vector

            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainengine);
            }
            
            if(!mainthrustparticle.isPlaying)
            {
                mainthrustparticle.Play();
            }
            
            

        }
        else
        {
            audioSource.Stop();
            mainthrustparticle.Stop();
        }

    }
    private void processrotation()
    {
        float rinput = rotation.ReadValue<float>();
        rb.freezeRotation = true; //Freezes the rotation of the Rigidbody, preventing it from rotating due to physics forces
        if (rinput < 0)
        {
            transform.Rotate(Vector3.forward * rotationspeed * Time.fixedDeltaTime);
            if (!leftthrustparticle.isPlaying)
            {
                leftthrustparticle.Play();
            }
        }
        else if (rinput > 0)
        {
            transform.Rotate(-Vector3.forward * rotationspeed * Time.fixedDeltaTime);
            if (!rightthrustparticle.isPlaying)
            {
                rightthrustparticle.Play();
            }
            
        }
        else
        {
            leftthrustparticle.Stop();
            rightthrustparticle.Stop();
        }
        rb.freezeRotation = false; //Unfreezes the rotation of the Rigidbody, allowing it to rotate again
    }
}
