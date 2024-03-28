public interface ISystemBasis : IInitialize
{
    void Prepare();

    #region 游戏生命周期
    void BeginPlay();

    /// <summary>
    /// Unity已经开始游戏
    /// </summary>
    void OnPlay();

    /// <summary>
    /// 游戏暂停Ex.完成;暂停
    /// </summary>
    void OnPause();

    /// <summary>
    /// 继续游戏
    /// </summary>
    void OnContinue();

    /// <summary>
    /// 退出游戏
    /// </summary>
    void OnExit();
    #endregion

    void OnLogin();

    void OnLogout();


    void OnApplicationQuit();


    void OnApplicationFocus(bool hasFocus);

    void OnApplicationPause(bool pauseStatus);
}