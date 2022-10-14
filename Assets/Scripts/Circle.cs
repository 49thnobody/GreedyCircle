using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private WayManager _wayManager;
    [SerializeField] private float _speed = 3;

    public delegate void WayPointReach();
    public event WayPointReach OnWayPointReach;

    public delegate void CoinCollected(MyGameObject coin);
    public event CoinCollected OnCoinCollected;

    public delegate void ThornCollapsed();
    public event ThornCollapsed OnThornCollapsed;

    private void Start()
    {
        _gameManager.OnGameStart += GameManager_OnGameStart;
    }

    private void GameManager_OnGameStart(int difficulty)
    {
        transform.position = Vector2.zero;
    }

    private void FixedUpdate()
    {
        if (_wayManager.NextWayPoint == null) return;

        float step = _speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, (Vector2)_wayManager.NextWayPoint, step);

        if (_wayManager.NextWayPoint == (Vector2)transform.position)
            OnWayPointReach?.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var mgo = collision.gameObject.GetComponent<MyGameObject>();
        if (mgo == null) return;

        if (mgo.Type == MyGameObjectType.Coin)
        {
            OnCoinCollected?.Invoke(mgo);
            Destroy(mgo.gameObject);
        }
        else if (mgo.Type == MyGameObjectType.Thorn)
            OnThornCollapsed?.Invoke();
    }
}