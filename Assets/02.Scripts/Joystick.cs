using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private RectTransform rect_Background;
    [SerializeField] private RectTransform rect_Joystick;

    private float radius;
    public GameObject go_Player;
    [SerializeField] private float moveSpeed;


    private PlayerDie go_PlayerDie;
    private bool isTouch = false;
    private Vector3 movePosition;

    //움직임의 한계치 (벽을 통과하지 않기 위해)
    private float maxXpos = 8.6f;
    private float maxZpos = 8.6f;

    void Start()
    {
        go_PlayerDie = go_Player.GetComponent<PlayerDie>();
        radius = rect_Background.rect.width * 0.5f;
    }
    void Update()
    {
        //죽었을때 컨트롤 막아둠
        if (isTouch && go_PlayerDie.isDie == false)
        {
            go_Player.transform.LookAt(go_Player.transform.position + movePosition);
            go_Player.transform.position += movePosition;

            Vector3 clampVec = new Vector3(Mathf.Clamp(go_Player.transform.position.x, -maxXpos, maxXpos),
                                                go_Player.transform.position.y, Mathf.Clamp(go_Player.transform.position.z, -maxZpos, maxZpos));

            go_Player.transform.position = clampVec;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 value = eventData.position - (Vector2)rect_Background.position;
        value = Vector2.ClampMagnitude(value, radius);
        rect_Joystick.localPosition = value;

        float distance = Vector2.Distance(rect_Background.position, rect_Joystick.position) / radius;
        value = value.normalized;
        movePosition = new Vector3(distance* value.x* moveSpeed * Time.deltaTime, 0f, distance* value.y * moveSpeed * Time.deltaTime);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isTouch = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isTouch = false;
        rect_Joystick.localPosition = new Vector3(0, 0, 0);
        movePosition = Vector3.zero;
    }
}
