using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cyberevolver;
using Cyberevolver.Unity;
using UnityEngine;
using System.Collections;
using UnityEditor;
using UObj = UnityEngine.Object;
using System.Reflection;
using UnityEditorInternal;
using System.Collections.ObjectModel;
using UnityEditor.AnimatedValues;

namespace Cyberevolver.EditorUnity
{
    public  class CyberAttributeException:CustomAttributeFormatException
    {
        public CyberAttributeException(Type attributeType,string message):base(message)
        {
            AttributeType = attributeType ?? throw new ArgumentNullException(nameof(attributeType));
        }

        public Type AttributeType { get; }
        

    }

    public sealed class CyberEdit 
    {
      
        private class GenericArrayComparer<T> : IEqualityComparer<T[]>
        {
            public bool Equals(T[] x, T[] y)
            {
                return StructuralComparisons.StructuralEqualityComparer.Equals(x, y);
            }

            public int GetHashCode(T[] obj)
            {
                return obj?.Sum(item => item.GetHashCode()) ?? 0;
            }
        }

        
        private CyberAttributeException error;
       
        private static bool lockSaving;

        public const BindingFlags SearchFlags = BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly;
        private readonly Dictionary<string, bool> ExpandedList = new Dictionary<string, bool>();
        private readonly Dictionary<string, AnimBool> FadedList = new Dictionary<string, AnimBool>();
        private readonly Dictionary<object[], CyberEdit> nested = new Dictionary<object[], CyberEdit>(new GenericArrayComparer<object>());
        private readonly Dictionary<string, int> toolbarSelected = new Dictionary<string, int>();
        private readonly Dictionary<string, string[]> toolbarElement = new Dictionary<string, string[]>();
        private Dictionary<string, IGrouping<string, MemberInfo>> groups;
    
        private readonly  Dictionary<(object, string), object> globalValues = new Dictionary<(object, string), object>();
        
    
        public ReadOnlyDictionary<string, string[]> ToolbarElements => toolbarElement.AsReadOnly();
        public SerializedProperty CurrentProp { get; private set; }
        public FieldInfo CurrentField { get; private set; }
        public MemberInfo CurrentInspectedMember { get; private set; }
        public static CyberEdit Current { get; private set; }

       
        private readonly object[] deepWay;
        private SerializedProperty script;
        public ReadOnlyCollection<object> DeepWay => Array.AsReadOnly(deepWay);
        public SerializedObject SerializedObject { get; }
        public UObj Target { get; }
        public Cint HorizontalStack { get; private set; }
        public bool IsHorizontal => HorizontalStack > 0;
        public bool DontDrawScriptInfo { get; set; }
        public bool IsBreakDown => error == null;
       
        public void PushHorizontalStack()
        {
            HorizontalStack++;
            
        }
        public void PopHorizontalStack()
        {
            HorizontalStack--;
        }
      
        
       
        private IEnumerable<FieldInfo> fields;
        private IEnumerable<MemberInfo> members;
      
        public CyberEdit(SerializedObject serializedObject, UObj target,params object[] deepWay)
        {
            SerializedObject = serializedObject;
            Target = target;
            this.deepWay = deepWay ?? new string[0];
        }
        public void RegisterAnimExpand(string folder)
        {
            var boo = new AnimBool();
            boo.valueChanged.AddListener(CyberInspector.Instance.Repaint);
            if (FadedList.ContainsKey(folder) == false)
                FadedList.Add(folder, boo);
        }
        public AnimBool GetFade(string folder)
        {
            return FadedList[folder];
        }
        public void SetGlobalValue(object id, string code, object value)
        {
            globalValues.AddOrSet((id, code), value);
        }
        public object GetGlobalValue(object field, string code)
        {
            return globalValues.GetOrSetDefualt((field, code));
        }
      
        public CyberEdit GetNested(object[] val)
        {
            if(nested.TryGetValue(val,out CyberEdit result))
            {
                return result;
            }
            else
            {
                CyberEdit propCyber = new CyberEdit(CyberEdit.Current.SerializedObject, CyberEdit.Current.Target,val);
                propCyber.Active();
                propCyber.DontDrawScriptInfo = true;
                nested.Add(val, propCyber);
                return propCyber;
            }
        }

        public int GetToolbarSelect(string id)
        {
           if(toolbarSelected.TryGetValue(id,out int val))
            {
                return val;
            }
           else
            {
                toolbarSelected.Add(id, 0);
                return 0;
            }
          
        }
        public void SetToolbarSelect(string id, int value)
        {
            toolbarSelected.AddOrSet(id, value);
        }
        public void SetToolbarElementCollection(string id, string[] elements)
        {
            toolbarElement.AddOrSet(id, elements);
        }
      
        public Type GetFinalTargetType()
        {
           Type type= this.Target.GetType();
            foreach(object element in deepWay)
            {
                if(element is int)
                {
                    type = type.GetElementType();
                }
                else
                {
                    FieldInfo nField = null;
                    while(nField==null)
                    {
                        nField = type.GetField(element.ToString(), BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                        type = type.BaseType;
                        if (type == null)
                            throw new Exception("Type has not found");
                    }
                    type = nField.FieldType;
                 
                }
                
            }
            return type;
        }
        public void Active()
        {


            Current = this;
            Type targetType = GetFinalTargetType();
            script = this.SerializedObject.FindProperty("m_Script");


            members =  GetTotalMembers(targetType);
            members =
                (from item in members
                 orderby (item is MethodInfo) ? 2 : 1 ascending
                 select item);
             fields = members.OfType<FieldInfo>();
           

          

            groups =
                (from item in members
                 let attributes = item.GetCustomAttributes<GroupAttribute>()
                 from attribute in attributes
                 where attribute != null
                 group item by attribute.Folder).ToDictionary();



            if (TheEditor.HasInit)
                InvokeOnEnableDrawers();
        }
        public void InvokeOnEnableDrawers()
        {

            try
            {
                foreach (var member in fields)
                    if (member is FieldInfo field)
                        TheEditor.DoOnAtr<IEnableInspectorDrawer>(field, (d, a) => d.DrawOnEnable(a, GetPropByName(field.Name), field));
                SerializedObject.ApplyModifiedProperties();
            }
            catch(CyberAttributeException exception)
            {
                error = exception;
                return;
            }
           
            
        }
      
      
        private IEnumerable<Type> GetAllTypeToSearch(Type t)
        {
            Type current = t;
           
            while (current != null)
            {
                yield return current;
                current = current.BaseType;
            }
        }
        private IEnumerable<MemberInfo> GetTotalMembers(Type type)
        {
           foreach(Type item in GetAllTypeToSearch(type).Reverse())
            {
                foreach (var member in item.GetMembers(SearchFlags))
                {
                    if (member is MethodInfo method && method.GetCustomAttributes<CyberAttribute>().Any())
                        yield return member;
                    else if (member is FieldInfo field &&( GetPropByName(field.Name) != null||field.GetCustomAttributes<CyberAttribute>().Any()))
                        yield return member;    
                }
                  
                
            }        
        }
        public void SetExpand(string key,bool val)
        {
         
            ExpandedList[key] = val;
        }
        public bool GetExpand(string key)
        {
           
            return ExpandedList[key];
        }
      
      
        public void DrawPreScript()
        {
            if (script != null)
            {
                GUI.enabled = false;
                EditorGUILayout.PropertyField(script);
                GUI.enabled = true;
            }
        }

        public void Restore()
        {
            Current = this;
        }

        public void DrawClass(Type type)
        {

            TheEditor.DrawAlwaysBefore(type);
            TheEditor.DrawAlwaysAfter(type);
            DrawMainClass(type);
        }
       
        public void DrawAll()
        {


            EditorGUI.BeginChangeCheck();
          
            

            if (SerializedObject == null || Target == null)
                return;
            SerializedObject.Update();
            Current = this;
            if (DontDrawScriptInfo == false)
                DrawPreScript();
            if (error != null)
            {
                CyberAttributeException error = (CyberAttributeException)this.error;
                EditorGUILayout.HelpBox($"Type:{error.AttributeType.Name}\n{error.Message}", UnityEditor.MessageType.Error);
                return;
            }
            DrawClass(Target.GetType());
          
            DrawElements(members,new string[0]);
            EditorGUI.EndChangeCheck();
            foreach (var item in FadedList.Where(item => ExpandedList.ContainsKey(item.Key)))
            {
                FadedList[item.Key].target = ExpandedList[item.Key];
            }
        }
        public void DrawElements(IEnumerable<MemberInfo> elements,string[] usedGroup)
        {
        
            HashSet<string> alreadyDrawerFolders = new HashSet<string>();
            foreach (MemberInfo member in elements)
            {

                try
                {
                    CurrentInspectedMember = member;
                    var groupElement = member.GetCustomAttributes<GroupAttribute>().FirstOrDefault
                        (element=> usedGroup.All(illegal=> illegal.Equals(element.Folder)==false));
                    if ((groupElement != null )
                        )
                    {


                    
                        if (alreadyDrawerFolders.Contains(groupElement.Folder))
                            continue;
                        alreadyDrawerFolders.Add(groupElement.Folder);
                        if (ExpandedList.ContainsKey(groupElement.Folder) == false)
                        {
                            ExpandedList.Add(groupElement.Folder, false);
                        }
                        IGrouping<string, MemberInfo> grouping = groups[groupElement.Folder];
                        DrawGroup(grouping,usedGroup,groupElement.Folder, null);
                    }
                    else
                        DrawMember(member);
                }
                catch (CyberAttributeException except)
                {
                    error = except;
                    return;
                }
                finally
                {
                    CurrentProp = null;
                    CurrentInspectedMember = null;
                    CurrentField = null;
                }



            }
        }
        public void DrawMember(MemberInfo member)
        {

            SetInspectedMember(member);
            if (member is FieldInfo field)
            {
                if (DrawProperty(field) == false)
                {
                    lockSaving = true;
                }
                
            }
            else if (member is MethodInfo method)
            {
                TheEditor.DrawMethod(method);
            }
            RestoreInspectedMember();
            
        }
        public bool DrawProperty(FieldInfo field,SerializedProperty customProperty=null)
        {
            SetInspectedMember(field,customProperty);
            bool result = TheEditor.DrawProperty(field, CurrentProp);
            RestoreInspectedMember();
            return result;
        }
        public void SetInspectedMember(MemberInfo member,SerializedProperty customProp=null)
        {

            if (member is FieldInfo field)
            {
                CurrentField = field;
                CurrentProp = customProp?? GetPropByName(field.Name);
            }
            CurrentInspectedMember = member;
          
        }
        public void RestoreInspectedMember()
        {
            CurrentProp = null;
                CurrentField = null;
            CurrentInspectedMember = null;
        }
        public void Save()
        {
            if (lockSaving == false)
                SerializedObject.ApplyModifiedProperties();
            lockSaving = false;

        }
        public SerializedProperty GetPropByName(string name)
        {
            if(deepWay.Length==0)
            {
                return SerializedObject.FindProperty(name);
            }
            else
            {
                SerializedProperty prop = SerializedObject.FindProperty(deepWay[0].ToString());
                for (int x = 1; x < deepWay.Length; x++)
                {

                    object element = deepWay[x];
                    if(element is int i)
                    {
                        prop = prop.GetArrayElementAtIndex(i);
                    }
                    else
                    {
                        prop = prop.FindPropertyRelative(deepWay[x].ToString());
                    }
                        
                 
                }
                return prop.FindPropertyRelative(name);
            }
        }
        public FieldInfo GetFieldByName(string name)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
           Type t= Target.GetType();
            foreach (object item in deepWay)
            {
                if (item is int)
                    break;
                t = t.GetField(item.ToString(), flag).FieldType;
            }
            return t.GetField(name, flag);
        }
        public  void DrawBasicGroup(IGrouping<string, MemberInfo> group, BackgroundMode mode, string[] usedGroup,bool drawPrefix=true
           )
        {
            
            
            if (mode != BackgroundMode.None)
                TheEditor.BeginVertical(mode);
            if (drawPrefix)
                EditorGUILayout.LabelField(group.Key, new GUIStyle(EditorStyles.boldLabel)); 
            DrawElements(group,usedGroup);
            if (mode != BackgroundMode.None)
                TheEditor.EndVertical(mode);
          
        }

        public void DrawGroup(IGrouping<string, MemberInfo> group, string[] usedFolder,string folder, MemberInfo toGet=null)
        {

            if(TheEditor.CanDrawGroup(CyberEdit.Current.GetFinalTargetType()))
            {
                TheEditor.DrawOnAttribute<IGroupDrawer>(toGet ?? group.First(),
                (d, a)
                =>
                {
                    if((a as GroupAttribute).Folder==folder)
                    {
                        d.DrawGroup(group, usedFolder.Append(folder).ToArray());
                        return true;
                    }
                    return false;
                 
                });
            }
           

        }
      
        public void DrawMainClass(Type type)
        {
            TheEditor.DoOnAtr<IClassDrawer>(type, (d, a) => d.DrawBeforeClass(a));
        }
    }
}
