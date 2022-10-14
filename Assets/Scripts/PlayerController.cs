using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private WayManager _wayManager;

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        // проверка тапает ли пользователь по интерфейсу
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.touches[0].fingerId))
                return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            var click = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            _wayManager.AddWayPoint(click);
        }
    }
}