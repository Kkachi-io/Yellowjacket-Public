using System.Collections.Generic;
using UnityEngine;

namespace Kkachi
{
    public static class TypeExtensions
    {

        #region List<T>

            /// <summary> Takes the first item in a list, returns the item, and then places it at the bottom of the list.
            /// </summary>
            public static T FirstFromPool<T>(this List<T> list)
            {
                var item = list[0];
                list.Remove(item);
                list.Insert(list.Count, item);
                return item;
            }

            /// <summary> Takes the first item in a list, returns the item, and removes it from the list.
            /// </summary>
            public static T FirstFromPoolAndRemove<T>(this List<T> list)
            {
                var item = list[0];
                list.Remove(item);
                return item;
            }

            /// <summary> Takes the last item in a list, places it at the zero index. Then returns the item.
            /// </summary>
            public static T LastFromPool<T>(this List<T> list)
            {
                var item = list[list.Count - 1];
                list.Remove(item);
                list.Insert(0, item);
                return item;
            }


        #endregion

        #region GameObject

            public static List<T> GetComponentsInDirectChildren<T>(this GameObject gameObject) where T : Component
            {
                int length = gameObject.transform.childCount;
                List<T> components = new List<T>(length);
                for (int i = 0; i < length; i++)
                {
                    T comp = gameObject.transform.GetChild(i).GetComponent<T>();
                    if (comp != null) components.Add(comp);
                }
                return components;
            }

        #endregion

        #region Transform
        
            public static List<T> GetComponentsInDirectChildren<T>(this Transform tf) where T : Component
            {
                int length = tf.transform.childCount;
                List<T> components = new List<T>(length);
                for (int i = 0; i < length; i++)
                {
                    T comp = tf.transform.GetChild(i).GetComponent<T>();
                    if (comp != null) components.Add(comp);
                }
                return components;
            }

            public static void MatchExact(this Transform tf, Transform toMatch)
            {
                tf.position = toMatch.position;
                tf.rotation = toMatch.rotation;
            }

            public static void LerpPositionAndRotation(this Transform tf, Transform toMatch, float t)
            {
                tf.position = Vector3.Lerp(tf.position, toMatch.position, t);
                tf.rotation = Quaternion.Lerp(tf.rotation, toMatch.rotation, t);
            }

        #endregion

        #region String

            /// <summary> Will copy the string to the system's clipboard.
            /// </summary>
            public static void CopyToClipboard(this string str)
            {
                GUIUtility.systemCopyBuffer = str;
            }

        #endregion
        
        #region Vector2
            
            /// <summary> Clamps all values of the vector between zero and the given parameters.
            /// Leave off the last parameters use clampMaxX for all of them.
            /// </summary>
            public static void Clamp(ref this Vector2 vector, float clampMinX, float clampMaxX,
                                                                float clampMinY = 1.1f, float clampMaxY = -1.1f)
            {
                var x = Mathf.Clamp(vector.x, clampMinX, clampMaxX);

                if(clampMinY == 1.1f) clampMinY = clampMinX;
                if(clampMaxY == -1.1f) clampMaxY = clampMaxX;

                var y = Mathf.Clamp(vector.y, clampMinY, clampMaxY);

                vector = new Vector2(x, y);

            }

            /// <summary> Clamps all values of the vector between zero and the given parameters.
            /// Leave off the last parameters use clampMaxX for all of them.
            /// </summary>
            public static void ClampFromZero(ref this Vector2 vector, float clampMaxX, float clampMaxY = -1)
            {
                var x = Mathf.Clamp(vector.x, 0, clampMaxX);

                if(clampMaxY == -1) clampMaxY = clampMaxX;

                var y = Mathf.Clamp(vector.y, 0, clampMaxY);

                vector = new Vector2(x, y);

            }

            /// <summary> Clamps all values of the vector between the given parameters (max) and their negative value (min).
            /// Leave off the last parameters use clampMaxX for all of them.
            /// </summary>
            public static void ClampFromInverse(ref this Vector2 vector, float clampMaxX, float clampMaxY = -1)
            {
                var x = Mathf.Clamp(vector.x, -clampMaxX, clampMaxX);

                if(clampMaxY == -1) clampMaxY = clampMaxX;

                var y = Mathf.Clamp(vector.y, -clampMaxY, clampMaxY);

                vector = new Vector2(x, y);

            }

        #endregion

        #region Vector3

            /// <summary> Clamps all values of the vector between zero and the given parameters.
            /// Leave off the last parameters use clampMaxX for all of them.
            /// </summary>
            public static void Clamp(ref this Vector3 vector, float clampMinX, float clampMaxX,
                                                                float clampMinY = 1.1f, float clampMaxY = -1.1f,
                                                                float clampMinZ = 1.1f, float clampMaxZ = -1.1f)
            {
                var x = Mathf.Clamp(vector.x, clampMinX, clampMaxX);

                if(clampMinY == 1.1f) clampMinY = clampMinX;
                if(clampMaxY == -1.1f) clampMaxY = clampMaxX;
                if(clampMinZ == 1.1f) clampMinZ = clampMinX;
                if(clampMaxZ == -1.1f) clampMaxZ = clampMaxX;

                var y = Mathf.Clamp(vector.y, clampMinY, clampMaxY);
                var z = Mathf.Clamp(vector.z, clampMinZ, clampMaxZ);

                vector = new Vector3(x, y, z);

            }

            /// <summary> Clamps all values of the vector between zero and the given parameters.
            /// Leave off the last parameters use clampMaxX for all of them.
            /// </summary>
            public static void ClampFromZero(ref this Vector3 vector, float clampMaxX, float clampMaxY = -1, float clampMaxZ = -1)
            {
                var x = Mathf.Clamp(vector.x, 0, clampMaxX);

                if(clampMaxY == -1) clampMaxY = clampMaxX;
                if(clampMaxZ == -1) clampMaxZ = clampMaxX;

                var y = Mathf.Clamp(vector.y, 0, clampMaxY);
                var z = Mathf.Clamp(vector.z, 0, clampMaxZ);

                vector = new Vector3(x, y, z);

            }

            /// <summary> Clamps all values of the vector between the given parameters (max) and their negative value (min).
            /// Leave off the last parameters use clampMaxX for all of them.
            /// </summary>
            public static void ClampFromInverse(ref this Vector3 vector, float clampMaxX, float clampMaxY = -1, float clampMaxZ = -1)
            {
                var x = Mathf.Clamp(vector.x, -clampMaxX, clampMaxX);

                if(clampMaxY == -1) clampMaxY = clampMaxX;
                if(clampMaxZ == -1) clampMaxZ = clampMaxX;

                var y = Mathf.Clamp(vector.y, -clampMaxY, clampMaxY);
                var z = Mathf.Clamp(vector.z, -clampMaxZ, clampMaxZ);

                vector = new Vector3(x, y, z);

            }

        #endregion


    }

}
