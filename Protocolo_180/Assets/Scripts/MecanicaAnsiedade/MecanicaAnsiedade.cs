using UnityEngine;
using System.Collections;

public class MecanicaAnsiedade : MonoBehaviour
{

public float speed = 5f;// Velocidade de movimento do objeto
private bool Acertou = false, JogoAtivo = false;// Variáveis para controlar o estado do jogo e se o jogador acertou a barra de acerto
public GameObject AnsiedadeDeMadu, Ponteiro;// Referência ao objeto que representa a ansiedade de Madu
private Rigidbody Rb;// Referência ao Rigidbody do objeto para controlar a física
public MonoBehaviour scriptMadu;// Referência ao script de controle do personagem Madu

public Transform Player;// Referência ao transform do jogador para posicionar a ansiedade de Madu em relação a ele

    void Awake()// Inicializa as variáveis e configurações iniciais
    {
        Rb = Ponteiro.GetComponent<Rigidbody>(); // Obtém a referência ao Rigidbody do objeto Ponteiro
        JogoAtivo = true; // Inicializa o jogo como ativo
        scriptMadu.enabled = false; // Desativa o script de controle do personagem Madu para impedir que ele se mova
    }

    void FixedUpdate()// Atualiza a física do objeto a cada frame fixo
    {
        if (JogoAtivo) // Verifica se o jogo está ativo
        {
           Rb.linearVelocity = new Vector3(speed, 0, 0); // Move o objeto na direção horizontal com a velocidade definida
        }
    }

    void Update()// Verifica a entrada do jogador a cada frame
    {
        if(!JogoAtivo) // Verifica se o jogo não está ativo
        {
            return; // Interrompe a execução do Update para evitar que o jogador possa interagir
        }

        if (Input.GetKeyDown(KeyCode.E) && Acertou && JogoAtivo) // Verifica se a barra de acerto foi acertada e a tecla de espaço foi pressionada
        {
            Debug.Log("Acertou a barra de acerto!"); // Exibe uma mensagem de acerto no console
            StartCoroutine(DesativarAnsiedadeDeMadu()); // Inicia a coroutine para Desativar a ansiedade de Madu
        }
        if (Input.GetKeyDown(KeyCode.E) && !Acertou && JogoAtivo) // Verifica se a barra de acerto não foi acertada e a tecla de espaço foi pressionada
        {
            Debug.Log("Errou a barra de acerto!"); // Exibe uma mensagem de erro no console
        }
    }

    private IEnumerator DesativarAnsiedadeDeMadu()// Coroutine para desativar a ansiedade de Madu por 60 segundos
    {
        AnsiedadeDeMadu.SetActive(false); // Desativa o objeto AnsiedadeDeMadu
        JogoAtivo = false; // Finaliza o jogo
        scriptMadu.enabled = true; // Ativa o script de controle do personagem Madu para permitir que ele se mova 

        yield return new WaitForSeconds(3f); // Aguarda 60 segundos

        AnsiedadeDeMadu.SetActive(true); // Ativa o objeto AnsiedadeDeMadu
        transform.position = Player.position; // Posiciona a ansiedade de Madu na posição do jogador
        JogoAtivo = true; // Reinicia o jogo
        scriptMadu.enabled = false; // Impede que o personagem se mova novamente
        speed = Mathf.Abs(speed);// Garante que a velocidade seja positiva para o próximo ciclo do jogo
    }

    private void OnTriggerEnter(Collider collision)// Verifica se o jogador colidiu com a barra de acerto ou com o limite
    {
        if (collision.CompareTag("LimiteDeBarra"))// Verifica se o jogador colidiu com o limite da barra de acerto
        {
            speed *= -1f; // Inverte a direção do movimento
        }

        if (collision.CompareTag("BarraVitoria"))// Verifica se o jogador colidiu com a barra de acerto
        {
            Acertou = true; // Define que o jogador acertou a barra de acerto
        }
    }

    private void OnTriggerExit(Collider collision)// Verifica se o jogador saiu da barra de acerto
    {
        if (collision.CompareTag("BarraVitoria"))// Verifica se o jogador saiu da barra de acerto
        {
            Acertou = false; // Define que o jogador não acertou a barra de acerto
        }
    }
}
