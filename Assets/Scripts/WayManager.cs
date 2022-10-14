using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WayManager : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    [SerializeField] private Circle _circle;
    [SerializeField] private Queue<MyGameObject> _way;
    [SerializeField] private MyGameObject _wayPointPrefab;

    public Vector2? NextWayPoint => GetNextWayPoint();

    private Vector2? GetNextWayPoint()
    {
        if (_way == null || _way.Count == 0) return null;

        return _way.Peek().transform.position;
    }

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _way = new Queue<MyGameObject>();
    }

    private void Start()
    {
        _lineRenderer.positionCount = 1;
        _lineRenderer.SetPosition(0, _circle.transform.position);
        _circle.OnWayPointReach += Circle_OnPointReach;
        _circle.OnThornCollapsed += Circle_OnThornCollapsed;
    }

    private void Circle_OnThornCollapsed()
    {
        _way.Clear();
    }

    private void Circle_OnPointReach()
    {
        Destroy(_way.Peek().gameObject);
        _way.Dequeue();
    }

    public void AddWayPoint(Vector2 position)
    {
        var newWayPoint = Instantiate(_wayPointPrefab);
        newWayPoint.Move(position);
        _way.Enqueue(newWayPoint);
    }

    private void Update()
    {
        _lineRenderer.positionCount = _way.Count + 1;
        _lineRenderer.SetPosition(0, _circle.transform.position);

        var wayPositions = _way.ToList();

        for (int i = 0; i < wayPositions.Count; i++)
        {
            _lineRenderer.SetPosition(i + 1, wayPositions[i].transform.position);
        }
    }
}