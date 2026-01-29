using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Configuração da máscara")]
    public Image[] slotsDaMascara;

    public Color corRevelada = Color.white;
    public Color corDoFlash = Color.purple;
    private int indiceMascara = 0;

    [Header("Configuração do diálogo")]
    public GameObject painelDialogo;
    public TMP_Text textoDialogo;
    public float tempoDeLeitura = 4.0f;

    private Coroutine corrotinaDoDialogo;

    [Header("Dados personagem no diálogo")]
    public Image imagemRetrato;
    public Image moldura;
    public TextMeshProUGUI textoNome;


    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;

        }
        else
        {
            Destroy(gameObject);
        }
        if(painelDialogo != null) painelDialogo.SetActive(false);
    }



    public void AdicionarPistaNaMascara(Sprite iconeDaPista)
    {
        if (iconeDaPista == null) return;
        if (indiceMascara >= slotsDaMascara.Length) return;

        Image slotAtual = slotsDaMascara[indiceMascara];
        
        // Em vez de mudar direto, chamamos a animação
        StartCoroutine(AnimarRevelacao(slotAtual, iconeDaPista));

        indiceMascara++;
    }


    IEnumerator AnimarRevelacao(Image slot, Sprite novoIcone)
    {
        float duration = 0.4f;
        Vector3 escalaOriginal = Vector3.one;
        Vector3 escalaGrande = Vector3.one * 1.3f;

        slot.transform.localScale = escalaGrande;
        slot.color = corDoFlash;

        yield return new WaitForSeconds(0.3f);
        
        slot.sprite = novoIcone;
        slot.type = Image.Type.Simple;
        slot.SetNativeSize();
        slot.color = corRevelada;

        float time = 0;

        while(time < duration)
        {
            slot.transform.localScale = Vector3.Lerp(escalaGrande,escalaOriginal,time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        slot.transform.localScale = escalaOriginal;

    }



    public void MostrarDialogo(string mensagem)
    {
        if(painelDialogo == null)return;
        painelDialogo.SetActive(true);
        textoDialogo.text = mensagem;

        if(imagemRetrato != null)
        {
            imagemRetrato.gameObject.SetActive(false);
            moldura.gameObject.SetActive(false);
        }
        if(textoNome != null) textoNome.text = "PISTA";

        if (corrotinaDoDialogo != null) 
        {
            StopCoroutine(corrotinaDoDialogo);
        }
        
        // Iniciamos o novo timer e guardamos a referência
        corrotinaDoDialogo = StartCoroutine(FecharDialogoAposTempo());
    }

    public void MostrarDialogoNPC(SuspectData suspeito)
    {
        painelDialogo.SetActive(true);
        textoDialogo.text = suspeito.falaPadrao;

        if(textoNome != null)
        {
            textoNome.gameObject.SetActive(true);
            textoNome.text = suspeito.nomeDoPersonagem;
        }

        if(imagemRetrato != null && suspeito.fotoDoRosto != null)
        {
            imagemRetrato.gameObject.SetActive(true);
            moldura.gameObject.SetActive(true);
            imagemRetrato.sprite = suspeito.fotoDoRosto;
            imagemRetrato.preserveAspect = true;
        }
    }


    IEnumerator FecharDialogoAposTempo()
    {
        yield return new WaitForSeconds(tempoDeLeitura);
        painelDialogo.SetActive(false);
    }

    public void FecharDialogo() { painelDialogo.SetActive(false); }
}
