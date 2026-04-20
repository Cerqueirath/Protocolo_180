using UnityEngine;

public class PosicaoJogadorShader : MonoBehaviour
{
    public Material materialTransparente;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(materialTransparente != null)
        {
            Vector3 posAjustada = new Vector3(transform.position.x, transform.position.y, 0);
            materialTransparente.SetVector("_PlayerPos", posAjustada);
        }
        
    }
}
