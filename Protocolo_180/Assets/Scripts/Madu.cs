using UnityEngine;
using UnityEngine.InputSystem;

public class Madu : MonoBehaviour
{
    private Rigidbody madu;
    private Vector2 movement, looking;

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

        Vector3 velocity = move * 5f;
        velocity.y = madu.linearVelocity.y;

        madu.linearVelocity = velocity;
    }    

    public void Mover(InputAction.CallbackContext Value)
    {
        movement = Value.ReadValue<Vector2>();
    }

    public void Jump(InputAction.CallbackContext Value)
    {        
        Vector3 v = madu.linearVelocity;
        v.y = 7f;
        madu.linearVelocity = v;      
    }

    public void look(InputAction.CallbackContext Value)
    {        
        looking = Value.ReadValue<Vector2>();    
    }
   

}
