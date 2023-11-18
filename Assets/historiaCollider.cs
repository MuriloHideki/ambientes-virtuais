using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JogoController : MonoBehaviour
{
    [System.Serializable]
    public class PerguntaResposta
    {
        [System.Serializable]
        public class Resposta
        {
            public string texto;
            public bool certa;
        }

        public string pergunta;
        public List<Resposta> respostas;
    }

    public GameObject objetoPrefab; // Referência ao prefab do objeto
    public List<GameObject> colunasDeRespostas; // Lista de objetos de referência
    public float alturaDeSpawn = 1.0f; // Altura acima do objeto de referência para o spawn

    public TextMeshPro perguntaTMP; // Referência ao componente TextMeshPro para a pergunta
    public GameObject objetoReferenciaSpawn; // Objeto de referência para determinar a posição de spawn

    public List<PerguntaResposta> perguntasRespostas; // Lista de perguntas e respostas
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Certo") || collision.gameObject.CompareTag("Errado"))
        {
            if (collision.gameObject.CompareTag("Certo"))
            {
                GlobalVariables.acertosHistoria++;
                Debug.Log("CERTO " + GlobalVariables.acertosHistoria);
            }
            else
            {
                Debug.Log("ERRADO");
            }
            Destroy(gameObject);

            if (objetoReferenciaSpawn != null)
            {
                Vector3 posicaoDeSpawn = objetoReferenciaSpawn.transform.position + Vector3.up * alturaDeSpawn;

                GameObject novoObjeto = Instantiate(objetoPrefab, posicaoDeSpawn, Quaternion.identity);

                AtivarScripts(novoObjeto);

                ProximaPergunta();
            }
            else
            {
                Debug.LogError("Nenhum objeto de referência de spawn atribuído!");
            }
        }
    }

    void AtivarScripts(GameObject objeto)
    {
        MonoBehaviour[] scripts = objeto.GetComponents<MonoBehaviour>();
        foreach (var script in scripts)
        {
            script.enabled = true;
        }
    }

    void ProximaPergunta()
    {
        Debug.Log("Antes da incrementação: " + GlobalVariables.indicePerguntaAtual);
        GlobalVariables.indicePerguntaAtual++;
        Debug.Log("Após a incrementação: " + GlobalVariables.indicePerguntaAtual);

        AtualizarUI();
    }


    void AtualizarUI()
    {
        Debug.Log("Atualizando a UI");

        // Verifica se há perguntas disponíveis
        if (GlobalVariables.indicePerguntaAtual < perguntasRespostas.Count)
        {
            // Atualiza o texto da pergunta
            if (perguntaTMP != null)
            {
                perguntaTMP.text = perguntasRespostas[GlobalVariables.indicePerguntaAtual].pergunta;
            }

            // Atualiza os textos das respostas nos quadrados
            for (int i = 0; i < colunasDeRespostas.Count; i++)
            {
                TextMeshPro textoResposta = colunasDeRespostas[i].GetComponentInChildren<TextMeshPro>();

                // Verifica se há respostas suficientes
                if (i < perguntasRespostas[GlobalVariables.indicePerguntaAtual].respostas.Count && textoResposta != null)
                {
                    textoResposta.text = perguntasRespostas[GlobalVariables.indicePerguntaAtual].respostas[i].texto;

                    // Define a tag da colunaDeRespostas com base no valor de certa
                    if (perguntasRespostas[GlobalVariables.indicePerguntaAtual].respostas[i].certa)
                    {
                        colunasDeRespostas[i].tag = "Certo";
                    }
                    else
                    {
                        colunasDeRespostas[i].tag = "Errado";
                    }
                }
            }
        }
        else
        {
            foreach (var coluna in colunasDeRespostas)
            {
                TextMeshPro textoResposta = coluna.GetComponentInChildren<TextMeshPro>();
                if (textoResposta != null)
                {
                    textoResposta.text = "";
                }
                coluna.tag = "Untagged";
            }
            perguntaTMP.text = "Parabéns, você acertou " + GlobalVariables.acertosHistoria+"/"+perguntasRespostas.Count;
        }
    }
}