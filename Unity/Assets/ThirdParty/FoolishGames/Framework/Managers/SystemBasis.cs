using FoolishGames.Common;

public abstract class SystemBasis : ISystemBasis, IUpdate
{
    public bool enabled { get; set; }

    public virtual bool UpdateBeforeInitialze
    {
        get { return false; }
    }

    public virtual bool Ready
    {
        get { return true; }
    }

    public virtual void Prepare()
    {
    }

    public virtual void OnReady()
    {
    }

    #region 游戏生命周期

    //public virtual void BeginPlay(GamePlayParam param)
    public virtual void BeginPlay()
    {
    }

    public virtual void OnPlay()
    {
    }

    public virtual void OnPause()
    {
    }

    public virtual void OnContinue()
    {
    }

    public virtual void OnExit()
    {
    }

    #endregion

    public virtual void OnFixedUpdate()
    {
    }

    /// <summary>
    /// 每帧执行
    /// </summary>
    public virtual void OnLateUpdate()
    {
    }

    /// <summary>
    /// 每帧执行
    /// </summary>
    public virtual void OnUpdate()
    {
    }

    public virtual void Initialize()
    {
    }

    public virtual void OnApplicationFocus(bool hasFocus)
    {
    }

    public virtual void OnApplicationPause(bool pauseStatus)
    {
    }

    public virtual void OnApplicationQuit()
    {
    }

    public virtual void OnLogin()
    {
    }

    public virtual void OnLogout()
    {
    }
}

public abstract class SystemBasis<T> : SystemBasis where T : SystemBasis<T>, new()
{
    public static T Instance
    {
        get { return Singleton<T>.Instance; }
    }
}