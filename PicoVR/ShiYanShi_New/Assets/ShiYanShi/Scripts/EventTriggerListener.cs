﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public class EventTriggerListener : UnityEngine.EventSystems.EventTrigger
{
    public delegate void VoidDelegate();
    public delegate void BoolDelegate(bool state);
    public delegate void FloatDelegate(float delta);
    public delegate void VectorDelegate(Vector2 delta);
    public delegate void ObjectDelegate(GameObject obj);
    public delegate void KeyCodeDelegate(KeyCode key);

    public ObjectDelegate onClick;
    public VoidDelegate onDown;
    public ObjectDelegate onEnter;
    public VoidDelegate onExit;
    public VoidDelegate onUp;
    public VoidDelegate onSelect;
    public VoidDelegate onUpdateSelect;


    static public EventTriggerListener Get(GameObject go)
    {
        EventTriggerListener listener = go.GetComponent<EventTriggerListener>();
        if (listener == null)
            listener = go.AddComponent<EventTriggerListener>();
        return listener;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (onClick != null)
            onClick(eventData.pointerEnter.gameObject);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (onDown != null)
            onDown();
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (onEnter != null)
            onEnter(eventData.pointerEnter.gameObject);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        if (onExit != null)
            onExit();
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        if (onUp != null)
            onUp();
    }

    public override void OnSelect(BaseEventData eventData)
    {
        if (onSelect != null)
            onSelect();
    }

    public override void OnUpdateSelected(BaseEventData eventData)
    {
        if (onUpdateSelect != null)
            onUpdateSelect();
    }
}  
