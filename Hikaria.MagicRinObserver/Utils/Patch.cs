using System;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;

namespace MagicRinObserver.Utils
{
    internal abstract class Patch
    {
        public virtual void Initialize()
        {
        }

        protected internal Harmony Harmony { get; set; }

        public virtual bool Enabled
        {
            get
            {
                return true;
            }
        }

        public abstract string Name { get; }

        public abstract void Execute();

        public void PatchConstructor<TClass>(PatchType patchType, string prefixMethodName = null, string postfixMethodName = null) where TClass : class
        {
            PatchConstructor<TClass>(null, patchType, prefixMethodName, postfixMethodName);
        }

        public void PatchConstructor<TClass>(Type[] parameters, PatchType patchType, string prefixMethodName = null, string postfixMethodName = null) where TClass : class
        {
            ConstructorInfo methodBase = AccessTools.Constructor(typeof(TClass), parameters, false);
            PatchMethod<TClass>(methodBase, patchType, prefixMethodName, postfixMethodName);
        }

        public void PatchMethod<TClass>(string methodName, PatchType patchType, Type[] generics = null, string prefixMethodName = null, string postfixMethodName = null) where TClass : class
        {
            PatchMethod<TClass>(methodName, null, patchType, generics, prefixMethodName, postfixMethodName);
        }

        public void PatchMethod<TClass>(string methodName, Type[] parameters, PatchType patchType, Type[] generics = null, string prefixMethodName = null, string postfixMethodName = null) where TClass : class
        {
            MethodInfo methodBase = AccessTools.Method(typeof(TClass), methodName, parameters, generics);
            PatchMethod<TClass>(methodBase, patchType, prefixMethodName, postfixMethodName);
        }

        public void PatchMethod<TClass>(MethodBase methodBase, PatchType patchType, string prefixMethodName = null, string postfixMethodName = null) where TClass : class
        {
            PatchMethod(typeof(TClass), methodBase, patchType, prefixMethodName, postfixMethodName);
        }

        public void PatchMethod(Type classType, string methodName, PatchType patchType, Type[] generics = null, string prefixMethodName = null, string postfixMethodName = null)
        {
            PatchMethod(classType, methodName, null, patchType, generics, prefixMethodName, postfixMethodName);
        }

        public void PatchMethod(Type classType, string methodName, Type[] parameters, PatchType patchType, Type[] generics = null, string prefixMethodName = null, string postfixMethodName = null)
        {
            MethodInfo methodBase = AccessTools.Method(classType, methodName, parameters, generics);
            PatchMethod(classType, methodBase, patchType, prefixMethodName, postfixMethodName);
        }

        public void PatchMethod(Type classType, MethodBase methodBase, PatchType patchType, string prefixMethodName = null, string postfixMethodName = null)
        {
            string text = classType.Name.Replace("`", "__");
            string text2 = methodBase.ToString();
            string text3 = (methodBase.IsConstructor ? "ctor" : methodBase.Name);
            MethodInfo methodInfo = null;
            MethodInfo methodInfo2 = null;
            if ((patchType & PatchType.Prefix) > 0)
            {
                try
                {
                    methodInfo2 = AccessTools.Method(GetType(), prefixMethodName ?? (text + "__" + text3 + "__Prefix"), null, null);
                }
                catch (Exception ex)
                {
                    Logs.LogError(("未能获得 ({0}) 的前缀补丁方法: {1}", text2, ex));
                }
            }
            if ((patchType & PatchType.Postfix) > 0)
            {
                try
                {
                    methodInfo = AccessTools.Method(GetType(), postfixMethodName ?? (text + "__" + text3 + "__Postfix"), null, null);
                }
                catch (Exception ex2)
                {
                    Logs.LogError(("未能获得 ({0}) 的后缀补丁方法: {1}", text2, ex2));
                }
            }
            try
            {
                if (methodInfo2 != null && methodInfo != null)
                {
                    Harmony.Patch(methodBase, new HarmonyMethod(methodInfo2), new HarmonyMethod(methodInfo), null, null, null);
                }
                else if (methodInfo2 != null)
                {
                    Harmony.Patch(methodBase, new HarmonyMethod(methodInfo2), null, null, null, null);
                }
                else if (methodInfo != null)
                {
                    Harmony.Patch(methodBase, null, new HarmonyMethod(methodInfo), null, null, null);
                }
            }
            catch (Exception ex3)
            {
                Logs.LogError(("无法为 {0} 方法打补丁: {1}", text2, ex3));
            }
        }

        public static void UnpatchSelf()
        {
            _Harmony.UnpatchSelf();
            Logs.LogError("Harmony: UnpatchSelf!");
        }

        [Flags]
        public enum PatchType : byte
        {
            Prefix = 1,
            Postfix = 2,
            Both = 3
        }

        public static void RegisterPatch<T>() where T : Patch, new()
        {
            if (_Harmony == null)
            {
                _Harmony = new Harmony(PluginInfo.PLUGIN_GUID);
            }
            if (RegisteredPatches.ContainsKey(typeof(T)))
            {
                Logs.LogMessage(string.Format(EntryPoint.Settings.Language.IGNORE_REPEAT_PATCH, typeof(T).Name));
                return;
            }
            T t = Activator.CreateInstance<T>();
            t.Harmony = _Harmony;
            T t2 = t;
            t2.Initialize();
            if (t2.Enabled)
            {
                Logs.LogMessage(string.Format(EntryPoint.Settings.Language.PATCHING, t2.Name));
                t2.Execute();
            }
            RegisteredPatches[typeof(T)] = t2;
        }

        private protected static readonly Dictionary<Type, Patch> RegisteredPatches = new();

        private protected static Harmony _Harmony;
    }
}