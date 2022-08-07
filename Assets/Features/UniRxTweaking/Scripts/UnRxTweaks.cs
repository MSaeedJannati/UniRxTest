using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Sirenix.OdinInspector;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class UnRxTweaks : MonoBehaviour
{
    #region Fields

    private IDisposable _counter;
    #endregion

    #region Monobehaviour callbacks

    [SerializeField] private int _topNum;


    private void Awake()
    {
        this.OnDisableAsObservable().Subscribe(
            next => { print("Disabled"); });
        this.OnEnableAsObservable().Subscribe(
            _ => { print("Enabled"); }
        );
    }

    void Start()
    {
      
      
        
        
    }

    [Button]
    void StartCounting()
    {
        _counter = Counter(_topNum).Subscribe(
            i => print(i)
            , _ => print("Error!"),
            () => print($"Kaboom!"));
    }

    [Button]
    void CancelCoutning()
    {
        _counter.Dispose();
    }

    #endregion

    #region Methods

    IObservable<int> Counter(int topNum)
    {
        return Observable.Create<int>(observer =>
            {
                if (topNum < 1)
                    observer.OnError(new Exception());
                for (int i = 0; i < topNum; i++)
                {
                    observer.OnNext(i);
                    Thread.Sleep(1000);
                }

                observer.OnCompleted();
                return Disposable.Empty;
            }
        );
    }

    #endregion
}