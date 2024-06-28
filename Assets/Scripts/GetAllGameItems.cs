using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;
using UnityEngine.SceneManagement;

public class GetAllGameItems : MonoBehaviour
{
    private class GameItem
    {
        public int number;
        public string name;
        public bool isFound;
        public GameObject gameObject;
        public BoxCollider addedCollider;
    }

    private readonly List<GameObject> allObjects = new List<GameObject>();
    private List<GameItem> gameItems;
    public Material material;
    public Vector3[] positions = new Vector3[] {
        new Vector3(10, 0, 5),
        new Vector3(20, 0, 15),
        new Vector3(30, 0, 25),
        new Vector3(40, 0, 35),
        new Vector3(50, 0, 45),
        new Vector3(60, 0, 55),
        new Vector3(70, 0, 65),
        new Vector3(80, 0, 75),
        new Vector3(90, 0, 85),
        new Vector3(100, 0, 95)
    };

    private int currentGameItemIndex = 0;
    private int collectedItemsCount = 0; // Compteur pour les objets collectés
    public Canvas canvas;
    public GameObject gameObjectInCanvas;
    public AnimationClip rotateAnimation;
    public BoxCollider parentCollider;

    public Canvas livesCanvas;

    public WinActionsManager winCanvas;

    public float currentIndexScene;

    void Start()
    {
        ListAllObjects(gameObject);
        gameItems = new List<GameItem>();

        List<GameObject> randomGameObjects = GetRandomGameItems(allObjects, 10);
        SpawnGameItems(randomGameObjects, positions);

        livesCanvas = GameObject.Find("LivesCanvas").GetComponent<Canvas>();
        winCanvas.HideWin();


        for (int i = 0; i < randomGameObjects.Count; i++)
        {
            GameItem gameItem = new GameItem
            {
                number = i,
                name = randomGameObjects[i].name,
                isFound = false,
                gameObject = randomGameObjects[i]
            };
            gameItems.Add(gameItem);
        }

        StartCoroutine(DisplayAndFindNextGameItem());
        currentIndexScene = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("new game");
    }

    void ListAllObjects(GameObject parent)
    {
        BoxCollider addedCollider = null;
        if (parent.GetComponent<Collider>() == null)
        {
            addedCollider = parent.AddComponent<BoxCollider>();
        }

        XRSimpleInteractable simpleInteractable = parent.GetComponent<XRSimpleInteractable>();
        if (simpleInteractable == null)
        {
            simpleInteractable = parent.AddComponent<XRSimpleInteractable>();
            simpleInteractable.selectEntered.AddListener(OnSelectEntered);
            simpleInteractable.hoverEntered.AddListener(OnHoverEntered);
        }

        allObjects.Add(parent);

        GameItem gameItem = new GameItem
        {
            gameObject = parent,
            addedCollider = addedCollider
        };

        foreach (Transform child in parent.transform)
        {
            ListAllObjects(child.gameObject);
        }
    }

    List<GameObject> GetRandomGameItems(List<GameObject> list, int count)
    {
        return list.OrderBy(x => Random.value).Take(count).ToList();
    }

    void SpawnGameItems(List<GameObject> gameItems, Vector3[] positions)
    {
        // for (int i = 0; i < gameItems.Count; i++)
        // {
        //     Instantiate(gameItems[i], positions[i], Quaternion.identity);
        // }
    }

    IEnumerator DisplayAndFindNextGameItem()
    {
        while (currentGameItemIndex < gameItems.Count)
        {
            GameItem currentGameItem = gameItems[currentGameItemIndex];
            Debug.Log("Displaying: " + currentGameItem.name);
            DisplayGameItem(currentGameItem);
            yield return new WaitForSeconds(5);
            HideCanvas();

            yield return new WaitUntil(() => currentGameItem.isFound);
            currentGameItemIndex++;
        }
    }

    void DisplayGameItem(GameItem gameItem)
    {
        if (canvas != null && gameObjectInCanvas != null && gameItem.gameObject != null)
        {
            foreach (Transform child in gameObjectInCanvas.transform)
            {
                Destroy(child.gameObject);
            }

            canvas.gameObject.SetActive(true);
            GameObject instantiatedObject = Instantiate(gameItem.gameObject, gameObjectInCanvas.transform);
            instantiatedObject.transform.localScale = Vector3.one * 200f;
            if (rotateAnimation != null)
            {
                Animation animation = instantiatedObject.GetComponent<Animation>();
                if (animation == null)
                {
                    animation = instantiatedObject.AddComponent<Animation>();
                }

                animation.AddClip(rotateAnimation, "RotateAnimation");
                animation.Play("RotateAnimation");
            }
        }
    }

    void HideCanvas()
    {
        canvas.gameObject.SetActive(false);
    }

    void OnSelectEntered(SelectEnterEventArgs args)
    {
        GameObject selectedObject = args.interactableObject.transform.gameObject;
        if (selectedObject == gameItems[currentGameItemIndex].gameObject)
        {
            SetItemFound(selectedObject);
        }
        else
        {
            StartCoroutine(TurnObjectRed(selectedObject));
            livesCanvas.GetComponent<PlayerLivesManager>().LoseLife();
        }
    }

    void OnHoverEntered(HoverEnterEventArgs args)
    {
        GameObject selectedObject = args.interactableObject.transform.gameObject;

        //add white material
    }

    public void SetItemFound(GameObject foundObject)
    {
        var gameItem = gameItems.FirstOrDefault(item => item.gameObject == foundObject);

        if (gameItem != null)
        {
            gameItem.isFound = true;
            collectedItemsCount++;
            if (collectedItemsCount >= 10)
            {
                if (SceneManager.GetActiveScene().buildIndex == 2) {
                    winCanvas.ShowWin(); 
                }
                else {
                    SceneManager.LoadScene(2);
                }
            }
        }
    }

    public void ResetCollectedItemsCount()
    {
        collectedItemsCount = 0;
        currentGameItemIndex = 0;
        foreach (var gameItem in gameItems)
        {
            gameItem.isFound = false;
        }
    }

    IEnumerator TurnObjectRed(GameObject obj)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        Material originalMaterial = renderer.material;

        Material redMaterial = new Material(originalMaterial);
        redMaterial.color = Color.red;

        renderer.material = redMaterial;

        yield return new WaitForSeconds(1f);
        renderer.material = originalMaterial;
    }
}
