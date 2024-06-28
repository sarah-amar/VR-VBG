using UnityEngine;
using UnityEngine.UI;

public class ObjectSelector : MonoBehaviour
{
    public float maxDistance = 100f;

    public LayerMask selectableLayer;


    private GameObject selectedObject;

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxDistance, selectableLayer))
        {
            GameObject hitObject = hit.collider.gameObject;

            if (hitObject != selectedObject)
            {
                selectedObject = hitObject;

                HighlightObject(selectedObject, true);

                hitObject.GetComponent<GetAllGameItems>().SetItemFound(hitObject);
            }
        }
        else
        {
            if (selectedObject != null)
            {
                HighlightObject(selectedObject, false);
                selectedObject = null;
            }
        }
    }

    void HighlightObject(GameObject obj, bool highlight)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            if (highlight)
            {
                renderer.material.color = Color.yellow; 
            }
            else
            {
                renderer.material.color = Color.white;
            }
        }
    }
}
