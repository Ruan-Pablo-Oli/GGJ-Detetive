using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class AccusationManager : MonoBehaviour
{
    public static AccusationManager Instance;

    [Header("Telas finais")]
    public GameObject painelVitoria;
    public GameObject painelDerrota;

    [Header("Elementos para preencher vitória")]
    public Image fotoAssassinonoVitoria;
    public TextMeshProUGUI nomeAssasssinoVitoria;


    [Header("Elementos para preencher derrota")]
    public Image fotoAssassinonoDerrota;
    public TextMeshProUGUI nomeAssasssinoDerrota;


    [Header("Configuração Visual")]
    public GameObject painelDeAcusacao;
    public TextMeshProUGUI textoResultado;

    [Header("O mistério")]
    public string nomeDoVerdadeiroAssassino;

    void Awake()
    {
        if(Instance == null) Instance = this;
    }


    public void AbrirPainel()
    {

        if(GameManager.Instance != null && GameManager.Instance.assassinoDaVez != null)
        {
            nomeDoVerdadeiroAssassino = GameManager.Instance.assassinoDaVez.nomeDoPersonagem;
        }

        painelDeAcusacao.SetActive(true);
        Time.timeScale = 0;
    }

    public void FecharPainel()
    {
        painelDeAcusacao.SetActive(false);
        Time.timeScale = 1;
    }

    public void Acusar(string nomeDoSuspeito)
    {
        
        SuspectData culpadoReal = GameManager.Instance.assassinoDaVez;
        painelDeAcusacao.SetActive(false);


        if(nomeDoSuspeito == culpadoReal.nomeDoPersonagem)
        {
            ProcessarVitoria(culpadoReal);
        }
        else
        {
            ProcessarDerrota(culpadoReal);

        }
    }

       public void ProcessarVitoria(SuspectData culpado)
    {
        painelVitoria.SetActive(true);

        if(fotoAssassinonoVitoria != null) fotoAssassinonoVitoria.sprite = culpado.fotoDoRosto;
        if(fotoAssassinonoVitoria != null) fotoAssassinonoVitoria.preserveAspect = true;
        if(nomeAssasssinoVitoria != null) nomeAssasssinoVitoria.text = "Você prendeu o assassino: " + culpado.nomeDoPersonagem;

        Time.timeScale = 0;

    }


    public void ProcessarDerrota(SuspectData culpado)
    {
        if(culpado == null) culpado = GameManager.Instance.assassinoDaVez;

        painelDerrota.SetActive(true);

        if(fotoAssassinonoDerrota != null) fotoAssassinonoDerrota.sprite = culpado.fotoDoRosto;  
        if(fotoAssassinonoDerrota != null) fotoAssassinonoDerrota.preserveAspect= true;
        if(nomeAssasssinoDerrota != null) nomeAssasssinoDerrota.text = "Você foi pego pelo assassino. Era: " + culpado.nomeDoPersonagem;

        Time.timeScale = 0; // Congela o jogo
    }

    public void ReiniciarFase()
    {
        painelVitoria.SetActive(false);
        painelDerrota.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void VoltarAoMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("CenaDeMenu");
    }


 


}
