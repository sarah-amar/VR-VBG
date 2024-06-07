using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class GetAllGameItems : MonoBehaviour
{
    private class GameItem
    {
        public int number;
        public string name;
        public bool isFound;
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

    void Start()
    {
        ListAllObjects(gameObject);
        gameItems = new List<GameItem>();

        List<GameObject> randomGameObjects = GetRandomGameItems(allObjects, 10);
        ApplyMaterial(material, randomGameObjects);
        SpawnGameItems(randomGameObjects, positions);

        for (int i = 0; i < randomGameObjects.Count; i++)
        {
            GameItem gameItem = new GameItem
            {
                number = i,
                name = randomGameObjects[i].name,
                isFound = false
            };
            gameItems.Add(gameItem);
        }

        foreach (var item in gameItems)
        {
            Debug.Log($"Number: {item.number}, Name: {item.name}, IsFound: {item.isFound}");
        }
    }

    void ListAllObjects(GameObject parent)
    {
        allObjects.Add(parent);
        foreach (Transform child in parent.transform)
        {
            ListAllObjects(child.gameObject);
        }
    }

    List<GameObject> GetRandomGameItems(List<GameObject> list, int count)
    {
        return list.OrderBy(x => Random.value).Take(count).ToList();
    }

    void ApplyMaterial(Material material, List<GameObject> objects)
    {
        foreach (GameObject obj in objects)
        {
            Renderer renderer = obj.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material = material;
            }
        }
    }

    void SpawnGameItems(List<GameObject> objects, Vector3[] positions)
    {
        for (int i = 0; i < objects.Count; i++)
        {
            Instantiate(objects[i], positions[i], Quaternion.identity);
        }
    }

    void DisplayGameItemToFind() {}
}