using UnityEngine;

public class DetectorProximidade : MonoBehaviour
{
    public Portao portao; // arraste o portão principal aqui no Inspector

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            portao.DefinirProximidade(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            portao.DefinirProximidade(false);
        }
    }
}
