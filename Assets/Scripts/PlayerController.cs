using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private WayManager _wayManager;

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (Input.GetMouseButtonDown(0))
        {
            var click = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            _wayManager.AddWayPoint(click);
        }
    }
}