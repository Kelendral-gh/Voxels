﻿using System;
using System.Collections.Generic;

namespace Tuntenfisch.Generics.Pool
{
    public class ObjectPool<T> where T : IPoolable
    {
        private readonly Stack<T> m_available;
        private readonly HashSet<T> m_inUse;
        private readonly Func<T> m_generator;

        public ObjectPool(Func<T> generator, int initialCapacity = 0, int initialCount = 0)
        {
            if (initialCapacity < initialCount)
            {
                throw new ArgumentOutOfRangeException(nameof(initialCapacity), initialCapacity, $"Parameter {nameof(initialCapacity)} must be at least as large parameter {nameof(initialCount)}.");
            }

            m_available = new Stack<T>(initialCapacity);
            m_inUse = new HashSet<T>();
            m_generator = generator;
            Populate(initialCount);
        }

        public T Acquire(Func<T, T> initializer)
        {
            T obj;

            if (m_available.Count == 0)
            {
                obj = m_generator();
            }
            else
            {
                obj = m_available.Pop();
            }
            m_inUse.Add(obj);
            obj.OnAcquire();
            obj = initializer(obj);

            return obj;
        }

        public void Release(T obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            if (!m_inUse.Remove(obj))
            {
                throw new ArgumentException($"Paremeter {nameof(obj)} doesn't belong to this pool.", nameof(obj));
            }

            obj.OnRelease();
            m_available.Push(obj);
        }

        public void Populate(int count)
        {
            for (int index = 0; index < count; index++)
            {
                T obj = m_generator();
                m_available.Push(obj);
            }
        }
    }
}