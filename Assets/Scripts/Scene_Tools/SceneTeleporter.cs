using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTeleporter : MonoBehaviour,IIteractable
{
    [Header("Info teleporte")]
    public string nomeDaProximaCena;

    [Header("Local de nascimento do player")]
    public Vector3 posicaoDeSpawn;

    public void Interact()
    {
        DontDestroy.proximaPosicao = posicaoDeSpawn;
        DontDestroy.existeTeleportePendente = true;

        SceneManager.LoadScene(nomeDaProximaCena);
    }

}
