using UnityEngine;
using UnityEngine.InputSystem;

public class Madu : MonoBehaviour
{
    //  COMPONENTES E VARIÁVEIS DE MOVIMENTO 
    private Rigidbody madu;
    private Vector2 movement, looking;
    public float sense = 3f; // Sensibilidade do mouse

    // Variável estática que controla se o player pode se mexer (acessada por outros scripts)
    public static bool podeInteragir = true;     
    
    private void Awake()
    {
        // Trava o cursor no centro da tela ao iniciar
        Cursor.lockState = CursorLockMode.Locked;
        // Pega a referência do Rigidbody anexado ao objeto
        madu = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Verifica se a interação está liberada
        if (!podeInteragir) 
        {
            // Zera os valores de olhar para impedir que a câmera gire em menus
            looking = Vector2.zero;
            return; // Interrompe a execução do restante do Update
        }

        // Calcula e aplica a rotação horizontal do personagem baseada no mouse
        float mouseX = looking.x * sense;
        transform.Rotate(0f, mouseX, 0f);
    }

    void FixedUpdate()
    {         
        // Se houver uma interação ativa (Cofre/Bilhete), o personagem para imediatamente
        if (!podeInteragir)
        {
            // Mantém a gravidade (y), mas zera o movimento nos eixos X e Z
            madu.linearVelocity = new Vector3(0, madu.linearVelocity.y, 0);
            return;
        }

        // Calcula a direção do movimento baseada para onde o personagem está olhando
        Vector3 move = transform.right * movement.x + transform.forward * movement.y;
        Vector3 velocity = move * 5f; // Define a velocidade de caminhada
        
        // Aplica a velocidade ao Rigidbody mantendo a física de queda/pulo
        velocity.y = madu.linearVelocity.y;
        madu.linearVelocity = velocity;
    }    

    //  FUNÇÕES CHAMADAS PELO PLAYER INPUT COMPONENT 

    // Recebe os valores de movimento (WASD / Analógico)
    public void Mover(InputAction.CallbackContext Value)
    {
        // Se estiver interagindo, ignora o comando e zera o vetor
        if (!podeInteragir) { movement = Vector2.zero; return; }
        movement = Value.ReadValue<Vector2>();
    }

    // Recebe o comando de pulo (Botão de Espaço / Sul)
    public void Jump(InputAction.CallbackContext Value)
    {        
        // Impede o pulo caso um menu/cofre esteja aberto
        if (!podeInteragir) return; 

        // Aplica força vertical para o pulo
        Vector3 v = madu.linearVelocity;
        v.y = 7f;
        madu.linearVelocity = v;      
    }

    // Recebe os valores de rotação da câmera (Mouse / Analógico Direito)
    public void look(InputAction.CallbackContext Value)
    {        
        // Se estiver interagindo, ignora a movimentação da câmera
        if (!podeInteragir) { looking = Vector2.zero; return; }
        looking = Value.ReadValue<Vector2>();    
    }
}