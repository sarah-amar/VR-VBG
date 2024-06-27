using UnityEngine;

public class SmoothnessSetter : MonoBehaviour
{
    void Start()
    {
        Renderer[] renderers = FindObjectsOfType<Renderer>();

        foreach (Renderer renderer in renderers)
        {
            foreach (Material mat in renderer.materials)
            {
                if (mat.HasProperty("_Glossiness"))
                {
                    mat.SetFloat("_Glossiness", 0.0f);
                }
            }
        }
    }
}
