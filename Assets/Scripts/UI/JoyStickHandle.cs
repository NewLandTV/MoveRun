using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStickHandle : MonoBehaviour, IPointerDownHandler ,IPointerUpHandler, IDragHandler
{
    [SerializeField]
    private RectTransform background;
    [SerializeField]
    private RectTransform joystickHandle;

    public Vector3 Direction
    {
        get;
        private set;
    }

    private float radius;

    private void Awake()
    {
        radius = background.rect.width * 0.5f;
    }
#if UNITY_EDITOR
    private void Update(){Direction=new Vector3(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"),0f);}
#endif
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 value = eventData.position - (Vector2)background.position;

        Direction = value.normalized;

        value = Vector2.ClampMagnitude(value, radius);

        joystickHandle.localPosition = value;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        joystickHandle.localPosition = Vector3.zero;

        Direction = Vector3.zero;
    }

    public void OnPointerDown(PointerEventData eventData) { }
}
