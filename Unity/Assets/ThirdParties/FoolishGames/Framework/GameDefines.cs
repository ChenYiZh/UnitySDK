using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.U2D;

[CreateAssetMenu(menuName = "Configs/GameDefines")]
public sealed partial class GameDefines : ScriptableObject
{
    /// <summary>
    /// 是否输出错误日志
    /// </summary>
    [SerializeField] private bool bLogError = true;

    public bool LogError
    {
        get { return bLogError; }
    }

    /// <summary>
    /// 是否输出所有错误日志
    /// </summary>
    [SerializeField] private bool bLogAll = true;

    public bool LogAll
    {
        get { return bLogAll; }
    }

    /// <summary>
    /// 是否只使用本地表
    /// </summary>
    public bool UseLocalTable = false;

    /// <summary>
    /// 读表路径
    /// </summary>
    [SerializeField] private string resourceTablePath = "Tables/";

    public string ResourceTablePath
    {
        get { return resourceTablePath; }
    }

    /// <summary>
    /// 可读写目录
    /// </summary>
    // [SerializeField] private string writableTablePath = Application.persistentDataPath;

    public string WritableTablePath
    {
        get { return Application.persistentDataPath; }
    }

    /// <summary>
    /// 在线Url
    /// </summary>
    public string OnlineBaseUrl;

    /// <summary>
    /// 在线校验表文件Url
    /// </summary>
    /// <returns></returns>
    public string GetOnlineIdxUrl()
    {
        string url = $"{OnlineBaseUrl}/assets.idx";
        return Util.CheckUrl(url);
    }

    /// <summary>
    /// 在线表Url
    /// </summary>
    /// <returns></returns>
    public string GetOnlineTablesUrl()
    {
        string url = $"{OnlineBaseUrl}{ResourceTablePath}";
        return Util.CheckUrl(url);
    }

    /// <summary>
    /// 图集列表
    /// </summary>
    [SerializeField] private List<SpriteAtlas> atlasDefines;

    /// <summary>
    /// panel动画曲线
    /// </summary>
    [SerializeField] private AnimationCurve panelRoundAnimCurve;

    public AnimationCurve PanelRoundAnimCurve
    {
        get { return panelRoundAnimCurve; }
    }

    /// <summary>
    /// panel动画曲线
    /// </summary>
    [SerializeField] private AnimationCurve panelOpacityAnimCurve;

    public AnimationCurve PanelOpacityAnimCurve
    {
        get { return panelOpacityAnimCurve; }
    }

    /// <summary>
    /// panel动画曲线
    /// </summary>
    [SerializeField] private AnimationCurve panelScaleAnimCurve;

    public AnimationCurve PanelScaleAnimCurve
    {
        get { return panelScaleAnimCurve; }
    }

    /// <summary>
    /// 音乐音量大小
    /// </summary>
    public float MusicVolume = 1.0f;

    /// <summary>
    /// 音效音量
    /// </summary>
    public float AudioVolume = 1.0f;

    /// <summary>
    /// 文本编码
    /// </summary>
    public Encoding UTF8Encoding = new UTF8Encoding(false);

    /// <summary>
    /// Panel的Resource路径
    /// </summary>
    [SerializeField] private string resourceUIPanelPath = "UIPanels/";

    public string ResourceUIPanelPath
    {
        get { return resourceUIPanelPath; }
    }

    /// <summary>
    /// Tips的Resource路径
    /// </summary>
    [SerializeField] private string resourceUITipsPath = "UITips/";

    public string ResourceUITipsPath
    {
        get { return resourceUITipsPath; }
    }

    /// <summary>
    /// 音效目录
    /// </summary>
    [SerializeField] private string resourceAudioPath = "Audio/";

    public string ResourceAudioPath
    {
        get { return resourceAudioPath; }
    }

    /// <summary>
    /// BGM目录
    /// </summary>
    [SerializeField] private string resourceMusicPath = "Music/";

    public string ResourceMusicPath
    {
        get { return resourceMusicPath; }
    }

    /// <summary>
    /// 时间格式化
    /// </summary>
    [SerializeField] private string timeFormat = "HH:mm:ss";

    public string TimeFormat
    {
        get { return timeFormat; }
    }

    /// <summary>
    /// 日期格式化
    /// </summary>
    [SerializeField] private string dateFormat = "yyyy-MM-dd";

    public string DateFormat
    {
        get { return dateFormat; }
    }

    public float UIWidth;

    public float UIHeight;

    public float UIAbsWidth;

    public float UIAbsHeight;

    /// <summary>
    /// 完整时间格式化
    /// </summary>
    [SerializeField] private string dateTimeFormat = "yyyy-MM-dd HH:mm:ss";

    public string DateTimeFormat
    {
        get { return dateTimeFormat; }
    }

    public Sprite GetSprite(string name)
    {
        foreach (var atlas in atlasDefines)
        {
            var sprite = atlas.GetSprite(name);
            if (sprite != null)
            {
                return sprite;
            }
        }

        return null;
    }
}