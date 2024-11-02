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
using FoolishGames.Collections;
using FoolishGames.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace FoolishGames.Common
{
    /// <summary>
    /// 程序集管理类
    /// </summary>
    public static class AssemblyService
    {
        private static ThreadSafeDictionary<string, Assembly> assemblies = new ThreadSafeDictionary<string, Assembly>();
        /// <summary>
        /// 已加载的程序集
        /// </summary>
        public static IEnumerable<Assembly> Assemblies { get { return assemblies.Values; } }
        /// <summary>
        /// 读取程序集
        /// </summary>
        /// <param name="dllPath"></param>
        /// <returns></returns>
        public static Assembly Load(string dllPath)
        {
            if (!dllPath.EndsWith(".dll")) return null;
            string dllName = Path.GetFileNameWithoutExtension(dllPath);
            if (assemblies.ContainsKey(dllName))
            {
                return assemblies[dllName];
            }
            Assembly assembly = Assembly.LoadFile(FPath.GetFullPath(dllPath));
            if (assembly != null)
            {
                assemblies.Add(dllName, assembly);
            }
            return assembly;
        }
    }
}
