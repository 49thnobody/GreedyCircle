using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFieldController : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private Circle _circle;

    [SerializeField] private MyGameObject _coinPrefab;
    [SerializeField] private MyGameObject _thornPrefab;

    private List<MyGameObject> _myGameObjects;

    private Vector2 _borders;

    private void Awake()
    {
        _myGameObjects = new List<MyGameObject>();
    }

    private void Start()
    {
        _gameManager.OnGameStart += SpawnGameField;
        _circle.OnCoinCollected += Circle_OnCoinCollected;

        FindBoundries();
    }

    private void FindBoundries()
    {
        float width = 1 / (Camera.main.WorldToViewportPoint(Vector3.one).x - .5f);
        float height = 1 / (Camera.main.WorldToViewportPoint(Vector3.one).y - .5f);

        _borders = new Vector2(width / 2, height / 2);
    }

    private void Circle_OnCoinCollected(MyGameObject coin)
    {
        _myGameObjects.Remove(coin);
    }

    private void ClearField()
    {
        for (int i = _myGameObjects.Count - 1; i >= 0; i--)
        {
            Destroy(_myGameObjects[i].gameObject);
            _myGameObjects.RemoveAt(i);
        }
    }

    public void SpawnGameField(int objectsCount)
    {
        ClearField();

        StartCoroutine(Spawn(objectsCount));
    }

    private IEnumerator Spawn(int objectsCount)
    {
        for (int i = 0; i < objectsCount; i++)
        {
            SpawnObject(MyGameObjectType.Coin);
            yield return new WaitForEndOfFrame();
            SpawnObject(MyGameObjectType.Thorn);
            yield return new WaitForEndOfFrame();
        }
    }

    private void SpawnObject(MyGameObjectType type)
    {
        MyGameObject newObject;
        if (type == MyGameObjectType.Coin)
            newObject = Instantiate(_coinPrefab);
        else
            newObject = Instantiate(_thornPrefab);

        _myGameObjects.Add(newObject);
        Vector2 objectPos;
        bool overlapp;
        do
        {
            objectPos = GetRandomPosition();

            Collider2D[] colliders = Physics2D.OverlapCircleAll(objectPos, 0.28f);

            if (colliders.Length == 0)
                overlapp = false;
            else
                overlapp = true;

        } while (overlapp == true);

        newObject.Move(objectPos);
    }

    private Vector2 GetRandomPosition()
    {
        return new Vector2(Random.Range(-_borders.x + 0.2f, _borders.x - 0.2f),
                    Random.Range(-_borders.y + 0.2f, _borders.y - 0.5f));
    }
}