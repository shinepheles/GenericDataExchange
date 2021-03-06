﻿using Sequencer.Entities;
using Sequencer.EventContext;
using Sequencer.Events;
using Sequencer.Interface;
using Sequencer.Peristaltic;
using System;
using System.Collections.Concurrent;
using System.Threading;

namespace Sequencer
{
    /**
     * Shine
     * 2019-9-5  
     * 线程绑定器
     * 使用<see cref="IBindContext"/>公共接口，于外部配置线程所必须的类型绑定及事件处理方法绑定
     * 于外部初始化方法中一次性调用<see cref="SequencerInitialization.PeristalticStart(IPeristalticConfiguration)"/>
     * 以启动线程
     * 
     **/

    /// <summary>
    /// 线程初始化类，具体说明参照接口注释
    /// </summary>
    internal class BindContext : IBindContext
    {
        public WaitHandle[] LoaderSignal { get; }
        public ConcurrentDictionary<Guid, WaitHandle[]> ResultSignal { get; }
        public ConcurrentQueue<QueueModel> Troops { get; }
        public ConcurrentDictionary<string, int> ExecuterDefault { get; set; }
        public ConcurrentDictionary<string, Action<WaitHandle[], ConcurrentQueue<QueueModel>>> Actions { get; }
        public ConcurrentDictionary<string, bool> Register { get; }
        public QueueAttacher Attacher { get; }
        public QueueResulter Resulter { get; }
        public BindContext()
        {
            LoaderSignal = new WaitHandle[2] { new AutoResetEvent(false), new ManualResetEvent(false)};
            Troops = new ConcurrentQueue<QueueModel>();
            ResultSignal = new ConcurrentDictionary<Guid, WaitHandle[]>();
            ExecuterDefault = new ConcurrentDictionary<string, int>();
            Actions = new ConcurrentDictionary<string, Action<WaitHandle[], ConcurrentQueue<QueueModel>>>();
            Register = new ConcurrentDictionary<string, bool>();
            Attacher = new QueueAttacher(Troops, LoaderSignal);
            Resulter = new QueueResulter();
        }
        public void Bind<T>(string name, Action<T> method, int d = 1)
        {
            Actions.TryAdd(name, (signal, queue) =>
            {
                new QueueExecuter<T>
                (
                    signal,
                    queue,
                    new AxEventContext<T>
                    (
                        new PeristalticEventProvider<T>(method)
                    ).Active
                ).Execute();
            });
            ExecuterDefault.TryAdd(name, d);
        }
        public void BindAsync<T>(string name, Action<T> method, AsyncCallback callback, int d= 1)
        {
            Actions.TryAdd(name, (signal, queue) =>
            {
                new QueueExecuter<T>
                (
                    signal,
                    queue,
                    new AxEventContext<T>
                    (
                        new PeristalticEventProvider<T>(method),
                        callback
                    ).Active
                ).Execute();
            });
            ExecuterDefault.TryAdd(name, d);
        }
        public void Bind<T, R>(string name, Func<T, R> method, int d = 1)
        {
            Actions.TryAdd(name, (signal, queue) =>
            {
                new QueueExecuter<T>
                (
                    signal,
                    queue,
                    new FxEventContext<T, R>
                    (
                        new PeristalticEventProvider<T, R>(method),
                        Resulter
                    ).Active
                ).Execute();
            });
            ExecuterDefault.TryAdd(name, d);
        }
        //绑定异步线程
        public void BindAsync<T, R>(string name, Func<T, R> method, AsyncCallback callback, int d = 1)
        {
            Actions.TryAdd(name, (signal, queue) =>
            {
                new QueueExecuter<T>
                 (
                     signal,
                     queue,
                     new FxEventContext<T, R>
                     (
                         new PeristalticEventProvider<T, R>(method),
                         Resulter,
                         callback
                     ).Active
                 ).Execute();
            });
            ExecuterDefault.TryAdd(name, d);
        }
    }
}
