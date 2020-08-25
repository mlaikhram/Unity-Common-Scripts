using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mlaikhram.Common
{
    public class ObjectPool<T> where T : Component
    {
        /// <summary>
        /// Determines where the pooled objects will be stored while inactive.
        /// </summary>
        public static Vector3 poolPosition = new Vector3(0, -100, 0);

        /// <summary>
        /// The object used to create this pool.
        /// </summary>
        public T Object { get; }

        private readonly List<T> pool;

        /// <summary>
        /// Determines how many objects are in this pool.
        /// </summary>
        public int Count => pool.Count;

        private int poolIndex;

        /// <summary>
        /// Creates an ObjectPool.
        /// </summary>
        /// <param name="objectTemplate">The object to use for the pool.</param>
        /// <param name="poolSize">How many copies of the object the pool should contain.</param>
        public ObjectPool(T objectTemplate, uint poolSize)
        {
            Object = objectTemplate;
            pool = new List<T>();
            for (int i = 0; i < poolSize; ++i)
            {
                T newObject = UnityEngine.Object.Instantiate(objectTemplate, poolPosition, Quaternion.identity);
                newObject.gameObject.SetActive(false);
                pool.Add(newObject);
            }
            poolIndex = 0;
        }

        /// <summary>
        /// Gets the oldest object in the pool.
        /// </summary>
        /// <param name="position">The position to spawn the object.</param>
        public T GetObject(Vector3 position)
        {
            T target = pool[poolIndex];
            if (target.gameObject.activeSelf)
            {
                target.gameObject.SetActive(false);
            }
            target.transform.position = position;
            target.gameObject.SetActive(true);
            poolIndex = (poolIndex + 1) % pool.Count;
            return target;
        }
    }
}