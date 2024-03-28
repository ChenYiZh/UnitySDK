using System;
using System.Collections.Generic;
using UnityEngine;

public class CComponent : IComponent
{
    public virtual bool enabled { get; set; }

    public virtual void Initialize() { }

    public virtual void Awake() { }

    public virtual void OnEnable() { }

    public virtual void Start() { }

    public virtual void OnSecond() { }

    public virtual void OnUpdate() { }

    public virtual void OnLateUpdate() { }

    public virtual void OnFixedUpdate() { }

    public virtual void OnDisable() { }

    public virtual void OnDestroy() { }

    public virtual void Release() { }

    public virtual void Reset() { }
}
