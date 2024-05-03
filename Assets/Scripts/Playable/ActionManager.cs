using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SabberStoneCore.Tasks.PlayerTasks;
using UnityEngine;

namespace Playable
{
    public class ActionManager : MonoBehaviour
    {
        public static ActionManager instance = null;
        private Queue<Action> _queue;

        private void Awake()
        {
            if (instance == null)
            {
                _queue = new Queue<Action>();
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            StartCoroutine(QueueUpdate());
        }


        public static void AddToQueue( Action func)
        {
            instance._queue.Enqueue(func);
            Debug.Log($"Queue Count {instance._queue.Count}");
        }


        private IEnumerator QueueUpdate()
        {
            while (true)
            {
                yield return null;
                if (_queue.Count > 0)
                {
                   Action routine = _queue.Peek();
                   _queue.Dequeue();
                   yield return new WaitForSeconds(1.0f);
                   routine.Invoke();

                }
            }
        }
    }
}