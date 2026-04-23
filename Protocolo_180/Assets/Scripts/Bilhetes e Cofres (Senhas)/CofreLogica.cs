using UnityEngine;
using TMPro; // Necessário para usar o componente de texto moderno (TextMeshPro)

public class CofreLogica : MonoBehaviour
{
    [Header("Configurações do Cofre")]
    public string senhaCorreta = "1505"; // Defina a senha aqui no Inspector
    public GameObject painelCofre;      // Arraste o objeto da UI que deve aparecer
    public TMP_InputField inputField;   // Arraste o campo de digitação aqui

    private bool perto = false;         // Diz se o player está encostado no cofre
    private bool aberto = false;        // Diz se a janelinha de senha está aberta agora
    private bool jaResolveu = false;    // Trava para o cofre não abrir mais depois de acertar

    // O Start prepara o jogo assim que ele começa
    void Start()
    {
        // Desliga o painel de senha para ele não começar na tela
        if (painelCofre != null) 
            painelCofre.SetActive(false);
        
        // Garante que a personagem comece podendo andar e o mouse fique preso
        Madu.podeInteragir = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Cria um "ouvinte": toda vez que o jogador digitar algo, o código chama a função de verificar
        inputField.onValueChanged.AddListener(delegate { VerificarSenha(); });
    }

    void Update()
    {
        // SÓ tenta abrir se: estiver perto E NÃO tiver resolvido E apertar "E"
        if (perto && !jaResolveu && Input.GetKeyDown(KeyCode.E))
        {
            if (!aberto) Abrir();
            else Fechar();
        }

        // Se o painel estiver aberto e o jogador apertar "Esc", ele fecha
        if (aberto && Input.GetKeyDown(KeyCode.Escape))
        {
            Fechar();
        }
    }

    // Função que trava o mundo e mostra o campo de senha
    void Abrir()
    {
        aberto = true;
        painelCofre.SetActive(true);    // Mostra o campo de texto
        Madu.podeInteragir = false;     // TRAVA A MADU (Personagem para de andar)
        
        Cursor.lockState = CursorLockMode.None; // Libera o mouse
        Cursor.visible = true;
        
        inputField.text = "";           // Limpa o campo para começar do zero
        inputField.ActivateInputField(); // Já deixa o cursor piscando pronto para digitar
    }

    // Função que esconde o campo e libera o personagem
    public void Fechar()
    {
        aberto = false;
        painelCofre.SetActive(false);   // Esconde o campo de texto
        Madu.podeInteragir = true;      // DESTRAVA A MADU (Personagem volta a andar)
        
        Cursor.lockState = CursorLockMode.Locked; // Prende o mouse de novo
        Cursor.visible = false;
        
        inputField.text = "";           // Limpa o texto por segurança
    }

    // Função que checa se o que foi digitado bate com a senha correta
    void VerificarSenha()
    {
        // Se o jogador terminou de digitar os 4 números...
        if (inputField.text.Length == 4)
        {
            if (inputField.text == senhaCorreta) 
            {
                Debug.Log("ACERTOU! O puzzle foi resolvido.");
                jaResolveu = true;      // Ativa a trava para nunca mais pedir a senha
                SucessoBrilhante();     // Chama a função de vitória
            } 
            else 
            {
                Debug.Log("SENHA ERRADA!");
                inputField.text = "";   // Limpa o campo para ele tentar de novo
            }
        }
    }

    // O que acontece exatamente no momento do acerto
    void SucessoBrilhante()
    {
        Fechar(); 
        // Se quiser que uma porta suma, você pode colocar: porta.SetActive(false); aqui.
    }

    //  SENSORES DE PROXIMIDADE 

    private void OnTriggerEnter(Collider other) 
    { 
        // Se o que entrou no sensor tiver a etiqueta "Player", ele pode interagir
        if (other.CompareTag("Player")) perto = true;
    }

    private void OnTriggerExit(Collider other) 
    { 
        // Se o Player se afastar, ele perde a permissão de interagir
        if (other.CompareTag("Player")) 
        {
            perto = false;
            if(aberto) Fechar(); // Se ele se afastar com o painel aberto, fecha sozinho
        }
    }
}