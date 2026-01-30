using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections; 
using System.Collections.Generic;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("--- CONFIGURAÇÃO DO MUNDO ---")]
    public List<string> cenasDoJogo; 

    [Header("--- SISTEMA DE SUSPEITOS ---")]
    public List<SuspectData> todosOsSuspeitos;
    public SuspectData assassinoDaVez;

    [Header("--- PREFABS ---")]
    public GameObject prefabPistaGenerica;

    [Header("--- UI ---")]
    public Image barraDeSuspeitaUI;
    public float suspeitaAtual = 0f;
    public float suspeitaMaxima = 100f;

    [Header("--- MEMÓRIA ---")]
    public List<string> pistasColetadas = new List<string>();

    [System.Serializable]
    public class Agendamento
    {
        public bool ehPista;
        public ClueData dadosPista;
        public SuspectData dadosNPC;
        public string cenaDestino;
        public bool jaFoiSpawnado;
    }

    private List<Agendamento> listaDeEntregas = new List<Agendamento>();

    void Awake()
    {
        if (Instance == null) { Instance = this; DontDestroyOnLoad(gameObject); }
        else { Destroy(gameObject); }
    }

    void OnEnable() { SceneManager.sceneLoaded += AoEntrarEmNovaCena; }
    void OnDisable() { SceneManager.sceneLoaded -= AoEntrarEmNovaCena; }

    void Start() { InicializarJogo(); }

    public void InicializarJogo()
    {
        if (todosOsSuspeitos.Count == 0) return;

        foreach (var s in todosOsSuspeitos) s.ehOAssassinho = false;
        assassinoDaVez = todosOsSuspeitos[Random.Range(0, todosOsSuspeitos.Count)];
        assassinoDaVez.ehOAssassinho = true;
        
        listaDeEntregas.Clear();
        pistasColetadas.Clear();
        suspeitaAtual = 0;

        foreach (var pista in assassinoDaVez.pistasDoCrime)
        {
            string cena = cenasDoJogo[Random.Range(0, cenasDoJogo.Count)];
            CriarAgendamento(true, pista, null, cena);
        }

        foreach (var suspeito in todosOsSuspeitos)
        {
            string cena = cenasDoJogo[Random.Range(0, cenasDoJogo.Count)];
            CriarAgendamento(false, null, suspeito, cena);
        }

        AoEntrarEmNovaCena(SceneManager.GetActiveScene(), LoadSceneMode.Single);
    }

    void CriarAgendamento(bool ehPista, ClueData pista, SuspectData npc, string cena)
    {
        Agendamento novo = new Agendamento();
        novo.ehPista = ehPista;
        novo.dadosPista = pista;
        novo.dadosNPC = npc;
        novo.cenaDestino = cena;
        listaDeEntregas.Add(novo);
        Debug.Log($"[AGENDADO] {(ehPista ? pista.name : npc.nomeDoPersonagem)} vai para: {cena}");
    }

    void AoEntrarEmNovaCena(Scene cena, LoadSceneMode modo)
    {
        StartCoroutine(SpawnComDelay(cena.name));
    }

    IEnumerator SpawnComDelay(string nomeDaCena)
    {
        yield return null; 

        List<Agendamento> entregas = listaDeEntregas.Where(x => x.cenaDestino == nomeDaCena).ToList();

        if (entregas.Count > 0)
        {
            ClueSpawnPoint[] pPista = FindObjectsByType<ClueSpawnPoint>(FindObjectsSortMode.None);
            NPCSpawnPoint[] pNPC = FindObjectsByType<NPCSpawnPoint>(FindObjectsSortMode.None);

            if (pPista.Length == 0 && pNPC.Length == 0)
            {
                Debug.LogError($"ERRO: Cena '{nomeDaCena}' não tem SpawnPoints! Verifique se você colocou os scripts nos objetos vazios.");
            }
            else
            {
                //Embaralhar(pPista);
                //Embaralhar(pNPC);

                int iP = 0; 
                int iN = 0;

                foreach (var item in entregas)
                {
                    if (item.ehPista)
                    {
                        if (pistasColetadas.Contains(item.dadosPista.name)) continue;

                        if (iP < pPista.Length)
                        {
                            GameObject obj = Instantiate(prefabPistaGenerica, pPista[iP].transform.position, Quaternion.identity);
                            var script = obj.GetComponentInChildren<ClueObject>();
                            var sprite = obj.GetComponentInChildren<SpriteRenderer>();
                            
                            if (script) script.dadosDaPista = item.dadosPista;
                            if (sprite) { sprite.sprite = item.dadosPista.icone; sprite.sortingOrder = 10; }
                            iP++;
                        }
                    }
                    else // NPC
                    {
                        if (iN < pNPC.Length && item.dadosNPC.prefabDoNPC != null)
                        {
                            Instantiate(item.dadosNPC.prefabDoNPC, pNPC[iN].transform.position, Quaternion.identity);
                            iN++;
                        }
                    }
                }
            }
        }
    }

    public void RegistrarColeta(string id) { if (!pistasColetadas.Contains(id)) pistasColetadas.Add(id); }
    public bool VerificarSeJaPegou(string id) { return pistasColetadas.Contains(id); }
    public void AumentarSuspeita(float v) { suspeitaAtual += v; if(barraDeSuspeitaUI) barraDeSuspeitaUI.fillAmount = suspeitaAtual/suspeitaMaxima; }
    
    void Embaralhar<T>(T[] array)
    {
        for (int i = 0; i < array.Length; i++) {
            T temp = array[i]; int r = Random.Range(i, array.Length);
            array[i] = array[r]; array[r] = temp;
        }
    }
}