using UnityEngine;
using UnityEngine.InputSystem;

public class Madu : MonoBehaviour
{
    private Rigidbody madu;
    private Vector2 movement, looking;

    private float speed = 4f;
    private bool jump;

    public float sense = 3f;       
    
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        madu = GetComponent<Rigidbody>();
    }

    void Update()
{
    float mouseX = looking.x * sense;
    transform.Rotate(0f, mouseX, 0f);
}

    void FixedUpdate()
    {         
        Vector3 move = transform.right * movement.x + transform.forward * movement.y;

        Vector3 velocity = move * speed;
        velocity.y = madu.linearVelocity.y;

        madu.linearVelocity = velocity;
    }    

    public void Mover(InputAction.CallbackContext Value)
    {
        movement = Value.ReadValue<Vector2>();
    }

    public void Correr(InputAction.CallbackContext Value)
    {
        if (Value.performed)
        {
            speed = 10f;
        }
        else if (Value.canceled)
        {
            speed = 4f;
        }
    }

    public void Jump(InputAction.CallbackContext Value)
    {  
        if (Value.performed && jump)
        {
            Vector3 v = madu.linearVelocity;
            v.y = 3f;
            madu.linearVelocity = v;
            
            jump = false; 
        } 
    }

    public void look(InputAction.CallbackContext Value)
    {        
        looking = Value.ReadValue<Vector2>();    
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Chao"))
        {
            jump = true;
        }
    }
   

}
