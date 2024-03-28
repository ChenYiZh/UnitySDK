// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.PostProcessing;
//
// public class ResolutionManager : SystemBasis<ResolutionManager>
// {
//     //const uint kNumFrameTimings = 2;
//
//     public float maxResolutionScale = 1.0f;
//     public float minResolutionScale = 0.8f;
//
//     //FrameTiming[] frameTimings = new FrameTiming[3];
//
//     //double m_gpuFrameTime;
//     //double m_cpuFrameTime;
//
//     //int m_frameCount=0;
//
//     DateTime _lastFPSLogTime;
//     int _frameCount;
//
//     int _tickCount;
//
//     bool noticed = false;
//
//     Resolution Default;
//
//     public override void Initialize()
//     {
//         base.Initialize();
//         Default = Screen.currentResolution;
//         Screen.SetResolution((int)(Default.width * 0.8f), (int)(Default.height * 0.8f), true);
//         _lastFPSLogTime = DateTime.Now;
//     }
//
//     public override void ForceUpdate()
//     {
//         base.ForceUpdate();
//         if (noticed)
//         {
//             return;
//         }
//         if (!GameSystem.IsWorking)
//         {
//             _lastFPSLogTime = DateTime.Now;
//             _frameCount = 0;
//             _tickCount = 0;
//             return;
//         }
//         _frameCount++;
//
//         ////DetermineResolution();
//         //if (m_gpuFrameTime > 0 && m_cpuFrameTime > 0)
//         //{
//         //    float fps = (float)((m_cpuFrameTime + m_cpuFrameTime) / 2);
//         //    float rate = Mathf.Clamp01((fps - 20) / 10);
//         //    float scale = Mathf.Lerp(minResolutionScale, maxResolutionScale, rate);
//         //    Debug.LogError(scale);
//         //    ScalableBufferManager.ResizeBuffers(scale, scale);
//         //}
//     }
//
//     public override void Ticking()
//     {
//         base.Ticking();
//         if (noticed)
//         {
//             return;
//         }
//         if (GameSystem.Instance.TryAgain)
//         {
//             _tickCount = 0;
//             _lastFPSLogTime = DateTime.Now;
//             return;
//         }
//         _tickCount++;
//         if (_tickCount > 15)
//         {
//             _tickCount = 0;
//             float deltaTime = (float)(DateTime.Now - _lastFPSLogTime).TotalSeconds;
//             _lastFPSLogTime = DateTime.Now;
//             float fps = _frameCount / deltaTime;
//             _frameCount = 0;
//             if (!noticed && fps < 15)
//             {
//                 noticed = true;
//                 int count = ((Tips_MessageBox)Tips.Root.Panels[ETips.Tips_MessageBox]).Count;
//                 count++;
//                 //帧率过低的提示窗，只需要一个确认按钮，不需要取消
//                 Util.ShowMessageBox(EMsgType.Confirm, 30150, () =>
//                 {
//                     DecreaseDisplay();
//                     //GameObject.Destroy(PlayerCamera.Instance.ViewCamera.gameObject.GetComponent<PostProcessingBehaviour>());
//                 });
//                 GameRoot.Root.StartCoroutine(Delay2CloseMsg(count));
//             }
//             //float rate = Mathf.Clamp01((fps - 20) / 10);
//             //float scale = Mathf.Lerp(minResolutionScale, maxResolutionScale, rate);
//             //ScalableBufferManager.ResizeBuffers(scale, scale);
//         }
//     }
//
//     public void DecreaseDisplay()
//     {
//         QualitySettings.shadows = ShadowQuality.Disable;
//         Screen.SetResolution((int)(Default.width * 0.7f), (int)(Default.height * 0.7f), true);
//     }
//
//     IEnumerator Delay2CloseMsg(int vailedCount)
//     {
//         float time = 0;
//         int maxTime = 5;
//         var tipsPanel = ((Tips_MessageBox)Tips.Root.Panels[ETips.Tips_MessageBox]);
//         while (time < maxTime && tipsPanel.Count <= vailedCount && tipsPanel.Visible)
//         {
//             UnityEngine.UI.Text confirmTxt = tipsPanel.ConfirmTxt;
//             int deltaSeconds = Mathf.RoundToInt(maxTime - time);
//             confirmTxt.text = TableLanguage.Instance.GetText(30028).Replace(" ", "\u00A0") + $"\u00A0({deltaSeconds})";
//             if (!Tips.Root.Panels[ETips.Tips_MessageBox].Visible)
//             {
//                 yield break;
//             }
//             yield return null;
//             time += Time.deltaTime;
//         }
//         Tips.Root.Panels[ETips.Tips_MessageBox].Hide();
//         DecreaseDisplay();
//     }
//
//     public override void OnApplicationFocus(bool hasFocus)
//     {
//         base.OnApplicationFocus(hasFocus);
//         _lastFPSLogTime = DateTime.Now;
//         _frameCount = 0;
//         _tickCount = 0;
//     }
//     //// Estimate the next frame time and update the resolution scale if necessary.
//     //private void DetermineResolution()
//     //{
//     //    ++m_frameCount;
//     //    if (m_frameCount <= kNumFrameTimings)
//     //    {
//     //        return;
//     //    }
//     //    FrameTimingManager.CaptureFrameTimings();
//     //    FrameTimingManager.GetLatestTimings(kNumFrameTimings, frameTimings);
//     //    if (frameTimings.Length < kNumFrameTimings)
//     //    {
//     //        //Debug.LogFormat("Skipping frame {0}, didn't get enough frame timings.",
//     //        //    m_frameCount);
//
//     //        return;
//     //    }
//
//     //    m_gpuFrameTime = (double)frameTimings[0].gpuFrameTime;
//     //    m_cpuFrameTime = (double)frameTimings[0].cpuFrameTime;
//     //}
// }
