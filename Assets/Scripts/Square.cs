using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Square : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Color CustomColor;
    private Image _image;
    private LineRenderer _lineRenderer;
    private Canvas _canvas;
    private bool _isDragStarted = false;
    private SquareMode _squareMode;
    public bool IsSuccess = false;



    private void Awake()
    {
        _image = GetComponent<Image>();
        _lineRenderer = GetComponent<LineRenderer>();
        _canvas = GetComponentInParent<Canvas>();
        _squareMode = GetComponentInParent<SquareMode>();
    }



    private void Update()
    {
        
        if (_isDragStarted)
        {
            Vector2 movePos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _canvas.transform as RectTransform,
                Input.mousePosition, 
                _canvas.worldCamera,
                out movePos);

            
            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, _canvas.transform.TransformPoint(movePos));
        }

        else
        {
            if (!IsSuccess)
            {
                _lineRenderer.SetPosition(0, Vector3.zero);
                _lineRenderer.SetPosition(1, Vector3.zero);
            }
        }

        bool isHovered = RectTransformUtility.RectangleContainsScreenPoint(transform as RectTransform, Input.mousePosition, _canvas.worldCamera);

        if (isHovered)
        {
           _squareMode.CurrentHoveredSquare = this;
        }
        
    

    }


    public void SetColor(Color color)
    {
        _image.color = color;
        _lineRenderer.startColor = color;
        _lineRenderer.endColor = color;
        CustomColor = color;
    }


    public void OnDrag(PointerEventData eventData)
    {

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (IsSuccess) { return; }

        _isDragStarted = true;
        _squareMode.CurrentDraggeSquare = this;
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        if (_squareMode.CurrentHoveredSquare != this)
        {
            if (_squareMode.CurrentHoveredSquare.CustomColor == CustomColor)
            {
                IsSuccess = true;
                _squareMode.CurrentHoveredSquare.IsSuccess = true;

            }
        }


        _isDragStarted = false;
        _squareMode.CurrentHoveredSquare = null;

    }
}
