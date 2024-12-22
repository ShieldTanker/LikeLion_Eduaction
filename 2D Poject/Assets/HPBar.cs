using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class HPBar : MonoBehaviour
{
    Camera camera;
    Camera uiCamera;

    Transform owner;

    private void Start()
    {
        camera = Camera.main;
    }
    private void LateUpdate()
    {
        if (owner != null && camera != null)
        {
            // ������ ���� ��ǥ�� ��ũ�� ��ǥ�� ��ȯ
            Vector3 screenPoint = camera.WorldToScreenPoint(owner.position);

            Vector2 localPoint = new Vector2();
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                transform.parent.GetComponent<RectTransform>(), screenPoint, uiCamera, out localPoint);

            transform.localPosition = localPoint;
        }
    }

    public void UpdateOwner(Transform owner, Camera uiCamera)
    {
        this.owner = owner;
        this.uiCamera = uiCamera;
    }
}
