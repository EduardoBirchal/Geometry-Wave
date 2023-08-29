using System.Collections;
using Unity.Collections;
using UnityEngine;
using Unity.Netcode;

public class IniciaOnda : FuncoesGerais
{
    public int onda = 0, numDificuldades;
    public float tempoEspera, dificuldade, dificuldadeTotal, tamanhoMargem, alturaTela, larguraTela;
    public GameObject[] inimigosDif1, inimigosDif2, inimigosDif3;
    public AudioSource fonteAudio;

    private Vector2 distanciaMargem;
    private GameObject[][] listaInimigos;
    private GameObject texto;
    private FuncoesTexto funcoesTexto;
    [SerializeField] private AudioClip[] efeitosOnda;
    [SerializeField] private int ondasEntreBoss;
    
    // Start is called before the first frame update
    void Start()
    {
        gameObject.name = "SpawnerInimigo";
        distanciaMargem = new Vector2(larguraTela - tamanhoMargem, alturaTela - tamanhoMargem);
        dificuldadeTotal *= dificuldade;
        listaInimigos = new GameObject[][] {inimigosDif1};
        
        texto = GameObject.Find("TextoGrandeMapa");
        funcoesTexto = texto.GetComponent<FuncoesTexto>();
        if(IsHost)
            IniciarChecaInimigosServerRpc();
    }

    IEnumerator ChecaInimigos() {
        // Procura todos os objetos com a tag "Inimigo". Se não tiver inimigos, cria uma nova onda
        GameObject[] inimigos = GameObject.FindGameObjectsWithTag("Inimigo");

        if(inimigos.Length == 0 && NetStatus.gameStarted == true) {
            StartCoroutine(CriaOnda());
        }
        else {
            // Espera 1 segundo e executa a função de novo. Não está usando Update porque só precisa rodar 1 vez por segundo em vez de 1 vez por frame.
            // Não reinicia toda vez porque o CriaOnda() espera um tempo antes de spawnar os inimigos. Se fizesse a checagem durante esse tempo, ia 
            // criar várias ondas antes da primeira spawnar.
            yield return new WaitForSeconds(tempoEspera);
            StartCoroutine(ChecaInimigos());
        }
    }

    void SpawnaInimigo(GameObject inimigo) {
        float posX, posY;

        if(Random.Range(0, 2) < 1) { // 50% de chance de X ser amplo e Y ser restrito
            posX = SinalAleatorio() * Random.Range(0, larguraTela);
            posY = SinalAleatorio() * Random.Range(alturaTela - tamanhoMargem, alturaTela);
        }
        else {                       // E 50% de chance do contrário
            posX = SinalAleatorio() * Random.Range(larguraTela - tamanhoMargem, larguraTela);
            posY = SinalAleatorio() * Random.Range(0, alturaTela);
        }

        Vector3 posicaoSpawn = new Vector3 (posX, posY, 0);
        GameObject novoInimigo = Instantiate(inimigo, posicaoSpawn, Quaternion.identity);
        novoInimigo.GetComponent<NetworkObject>().Spawn();
    }

    [ServerRpc]
    void IniciarChecaInimigosServerRpc()
    {
        StartCoroutine(ChecaInimigos());
    }                                                    
    
    [ClientRpc]
    void MostrarOndaClientRpc(int onda, bool boss)
    {
        funcoesTexto.MostraFade(1.5f, 1.5f, "Onda " + onda);

        if (boss) fonteAudio.PlayOneShot(efeitosOnda[1], 1);
        else fonteAudio.PlayOneShot(efeitosOnda[0], 1);
    }

    IEnumerator CriaOnda() {
        yield return new WaitForSeconds(2);

        int dificuldadeDisponivel = (int) dificuldadeTotal;
        onda++;
        bool vaiSerBoss = (onda % ondasEntreBoss == 0);

        MostrarOndaClientRpc(onda, vaiSerBoss);

        while (dificuldadeDisponivel > 0) {
            // Escolhe o menor número entre numDificuldades e dificuldadeDisponivel
            int dificuldadeAtual = Random.Range(1, dificuldadeDisponivel > numDificuldades ? numDificuldades : dificuldadeDisponivel); 

            // Escolhe um inimigo aleatório na dificuldade escolhida
            int indexInimigo = Random.Range(0, listaInimigos[dificuldadeAtual-1].Length - 1);

            GameObject inimigoAtual = listaInimigos[dificuldadeAtual-1][indexInimigo];
            SpawnaInimigo(inimigoAtual);
            dificuldadeDisponivel -= dificuldadeAtual;

            yield return new WaitForSeconds(0.5f);
        }   

        dificuldadeTotal++;
        StartCoroutine(ChecaInimigos());                                                                         
    }                                                                   
}