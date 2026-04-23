using UnityEngine;

public class Bilhete : MonoBehaviour
{
    //  VARIÁVEIS DE CONFIGURAÇÃO 
    [Header("Configurações do Bilhete")]
    public GameObject painelDaImagem;   // Arraste aqui a imagem/painel que deve aparecer na tela
    private bool estaLendo = false;     // Guarda a informação se o bilhete está aberto ou não
    private bool playerEstaPerto = false; // Guarda a informação se o jogador entrou na área do bilhete

    // O Update roda o tempo todo, verificando se você apertou o botão
    void Update()
    {
        // SE o jogador estiver perto E apertar a tecla "E"
        if (playerEstaPerto && Input.GetKeyDown(KeyCode.E))
        {
            // Se NÃO estiver lendo, ele abre. Se JÁ ESTIVER lendo, ele fecha.
            if (!estaLendo) 
            {
                AbrirBilhete();
            }
            else 
            {
                FecharBilhete();
            }
        }
    }

    // Função responsável por MOSTRAR o bilhete e TRAVAR o jogo
    void AbrirBilhete()
    {
        painelDaImagem.SetActive(true);    // Liga a imagem na tela
        estaLendo = true;                  // Avisa ao código que estamos lendo
        
        Madu.podeInteragir = false;        // TRAVA A MADU: Avisa ao script do player para parar de andar/olhar
        
        // Configurações do Mouse: Libera a seta para o jogador poder clicar se precisar
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Função responsável por ESCONDER o bilhete e DESTRAVAR o jogo
    void FecharBilhete()
    {
        painelDaImagem.SetActive(false);   // Desliga a imagem da tela
        estaLendo = false;                 // Avisa ao código que paramos de ler
        
        Madu.podeInteragir = true;         // DESTRAVA A MADU: Deixa o player andar e olhar de novo
        
        // Configurações do Mouse: Prende a seta de volta no meio da tela
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // SENSORES DE PROXIMIDADE (TRIGGER)

    // Executa quando o Player entra no quadrado verde (Collider) do bilhete
    private void OnTriggerEnter(Collider other) 
    { 
        if (other.CompareTag("Player")) 
        {
            playerEstaPerto = true; 
        }
    }

    // Executa quando o Player sai do quadrado verde (Collider) do bilhete
    private void OnTriggerExit(Collider other) 
    { 
        if (other.CompareTag("Player")) 
        {
            playerEstaPerto = false; 
            
            // Se o player se afastar enquanto lê, o bilhete fecha sozinho por segurança
            if(estaLendo) FecharBilhete(); 
        }
    }
}