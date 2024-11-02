﻿/****************************************************************************
THIS FILE IS PART OF Foolish Server PROJECT
THIS PROGRAM IS FREE SOFTWARE, IS LICENSED UNDER MIT

Copyright (c) 2022-2030 ChenYiZh
https://space.bilibili.com/9308172

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
****************************************************************************/
using FoolishGames.IO;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoolishGames.Net
{
    /// <summary>
    /// 套接字消息发送的接口定义
    /// </summary>
    public interface ISender
    {
        ///// <summary>
        ///// 开始执行发送
        ///// </summary>
        ///// <returns>是否维持等待状态</returns>
        //bool BeginSend();
        /// <summary>
        /// 消息Id
        /// </summary>
        long MessageNumber { get; set; }
        /// <summary>
        /// 数据发送<c>异步</c>
        /// </summary>
        /// <param name="message">大宋的消息</param>
        /// <returns>判断有没有发送出去</returns>
        void Send(IMessageWriter message);
        /// <summary>
        /// 立即发送消息，会打乱消息顺序。只有类似心跳包这种及时的需要用到。一般使用Send就满足使用
        /// </summary>
        /// <param name="message">发送的消息</param>
        [Obsolete("Only used in important message. This method will confuse the message queue. You can use 'Send' instead.", false)]
        void SendImmediately(IMessageWriter message);
        ///// <summary>
        ///// 内部函数，直接传bytes，会影响数据解析
        ///// </summary>
        //void SendBytes(byte[] data);
        ///// <summary>
        ///// 内部函数，直接传bytes，会影响数据解析，以及解析顺序
        ///// </summary>
        //void SendBytesImmediately(byte[] data);
        ///// <summary>
        ///// 消息发送处理
        ///// </summary>
        //bool ProcessSend();
    }
}
