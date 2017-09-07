using System;
using System.Reflection;
using UnityEngine;

namespace UBootstrap
{
    static public partial class ReflectionHelper
    {
        // from http://answers.unity3d.com/questions/206665/typegettypestring-does-not-work-in-unity.html
        public static Type GetType (string typeName)
        {
            var type = Type.GetType (typeName);

            if (type != null)
                return type;

            var currentAssembly = Assembly.GetExecutingAssembly ();
            if (currentAssembly == null)
                return null;

            // If we still haven't found the proper type, we can enumerate all of the
            // loaded assemblies and see if any of them define the type

            var referencedAssemblies = currentAssembly.GetReferencedAssemblies ();
            foreach (var assemblyName in referencedAssemblies) {
                var assembly = Assembly.Load (assemblyName);
                if (assembly != null) {
                    type = assembly.GetType (typeName);
                    if (type != null) {
                        return type;
                    }
                }
            }

            // The type just couldn't be found...
            return null;
        }

        public static MethodInfo GetMethodRecursive (object o, string methodName)
        {
            Type type = o.GetType ();
            MethodInfo m = null;

            while (type != null) {
                m = type.GetMethod (methodName, BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic);

                if (m != null) {
                    return m;
                } else {
                    type = type.BaseType;
                }
            }

            return m;
        }

        public static object CreateDelegate (Type T, object o, string methodName)
        {
            MethodInfo m = GetMethodRecursive (o, methodName);

            if (m != null) {
                try {
                    return Delegate.CreateDelegate (T, o, m);
                } catch (Exception e) {
                    Debug.LogError ("CreateDelegate method not compatible " + methodName + " " + e.StackTrace);
                }
            }

            return null;
        }

        public static T CreateDelegate <T> (object o, string methodName) where T : class
        {
            MethodInfo m = GetMethodRecursive (o, methodName);

            if (m != null) {
                try {
                    return Delegate.CreateDelegate (typeof(T), o, m) as T;
                } catch (Exception e) {
                    Debug.LogError ("CreateDelegate method not compatible " + methodName + " " + e.StackTrace);
                }
            }

            return null;
        }


    }
}
