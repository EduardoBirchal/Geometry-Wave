using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Flock : FuncoesGerais
{
    public int onda = 0, numDificuldades;
    public float tempoEspera, dificuldade, dificuldadeTotal, tamanhoMargem, alturaTela, larguraTela;
    public GameObject[] inimigosDif1, inimigosDif2, inimigosDif3;
    public AudioSource fonteAudio;
    private Vector2 distanciaMargem;
    private GameObject[][] listaInimigos;
    private GameObject texto;
    private FuncoesTexto funcoesTexto;
    private NetworkStart NetworkInfo;
    [SerializeField] private AudioClip[] efeitosOnda;




    public FlockAgent agentPrefab;
    List<FlockAgent> agents = new List<FlockAgent>();
    public FlockBehavior behavior;
    

    [Range(1f, 100f)]
    public float driveFactor = 10f;
    [Range(1f, 100f)]
    public float maxSpeed = 5f;
    [Range(1f, 10f)]
    public float neighborRadius = 1.5f;
    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f;

    float squareMaxSpeed;
    float squareNeighborRadius;
    float squareAvoidanceRadius;
    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }

    // Start is called before the first frame update
    void Start()
    {
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighborRadius = neighborRadius * neighborRadius;
        squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;


        NetworkInfo = GameObject.Find("NetworkManager").GetComponent<NetworkStart>();
        gameObject.name = "SpawnerInimigo";
        distanciaMargem = new Vector2(larguraTela - tamanhoMargem, alturaTela - tamanhoMargem);
        dificuldadeTotal *= dificuldade;
        listaInimigos = new GameObject[][] {inimigosDif1};
        
        texto = GameObject.Find("TextoGrandeMapa");
        funcoesTexto = texto.GetComponent<FuncoesTexto>();
        if(IsHost)
            IniciarChecaInimigosServerRpc();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (FlockAgent agent in agents)
        {
            List<Transform> context = GetNearbyObjects(agent);

            //FOR DEMO ONLY
            //agent.GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(Color.white, Color.red, context.Count / 6f);

            Vector2 move = behavior.CalculateMove(agent, context, this);
            move *= driveFactor;
            if (move.sqrMagnitude > squareMaxSpeed)
            {
                move = move.normalized * maxSpeed;
            }
            agent.Move(move);
        }
    }

    List<Transform> GetNearbyObjects(FlockAgent agent)
    {
        List<Transform> context = new List<Transform>();
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(agent.transform.position, neighborRadius);
        foreach (Collider2D collider in contextColliders)
        {   
            if(collider.transform == null){
                if (collider != agent.AgentCollider)
                {
                    context.Add(collider.transform);
                }
            }
            
        }
        return context;
    }

    public void removeAgents(FlockAgent a){
        
        agents.Remove(a);

    }

    IEnumerator ChecaInimigos() {
        // Procura todos os objetos com a tag "Inimigo". Se não tiver inimigos, cria uma nova onda
        GameObject[] inimigos = GameObject.FindGameObjectsWithTag("Inimigo");

        if(inimigos.Length == 0 && NetworkStart.gameStarted == true) {
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

    void SpawnaInimigo(int i) {
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
        FlockAgent novoInimigo = Instantiate(agentPrefab, posicaoSpawn, Quaternion.identity);
        novoInimigo.GetComponent<NetworkObject>().Spawn();

        novoInimigo.name = "Agent " + i;
        novoInimigo.Initialize(this);
        agents.Add(novoInimigo);

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

        if (boss) fonteAudio.PlayOneShot(efeitosOnda[1]);
        else fonteAudio.PlayOneShot(efeitosOnda[0]);
    }
    IEnumerator CriaOnda() {
        yield return new WaitForSeconds(2);
        int dificuldadeDisponivel = (int) dificuldadeTotal;
        onda++;

        MostrarOndaClientRpc(onda, false);
        
        int i = 0;
        while (dificuldadeDisponivel > 0) {
            // Escolhe o menor número entre numDificuldades e dificuldadeDisponivel
            int dificuldadeAtual = Random.Range(1, dificuldadeDisponivel > numDificuldades ? numDificuldades : dificuldadeDisponivel); 

            // Escolhe um inimigo aleatório na dificuldade escolhida
            int indexInimigo = Random.Range(0, listaInimigos[dificuldadeAtual-1].Length - 1);

            SpawnaInimigo(i);
            dificuldadeDisponivel -= dificuldadeAtual;

            i++;

            yield return new WaitForSeconds(0.5f);
        }   

        dificuldadeTotal++;
        StartCoroutine(ChecaInimigos());                                                                         
    }
}
