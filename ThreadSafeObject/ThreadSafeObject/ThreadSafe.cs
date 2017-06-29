using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ThreadSafeObject
{
    /// <summary>
    /// Making any call to a function thread safe
    /// Compatible .NET Standard 1.2
    /// Means .NET Framework>=4.5.1
    /// Means .NET Framework>=1.0
    /// Test and details at https://github.com/ignatandrei/ThreadSafeObject
    /// </summary>
    public class ThreadSafe: DynamicObject,IDynamicMetaObjectProvider
    {
        private object o;
        TypeInfo t;
        object myLock;
        /// <summary>
        /// constructor
        /// pass the object that you want to make
        /// functions thread safe
        /// </summary>
        /// <param name="o"></param>
        public ThreadSafe(object o)
        {
            this.o = o;
            t = o.GetType().GetTypeInfo();
            myLock = new object();
        }
        /// <summary>
        /// redirects to original object
        /// </summary>
        /// <param name="binder"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
           
            var field = t.GetDeclaredField(binder.Name);

            lock (myLock)
            {
                field.SetValue(o, value);
            }
            return true;
        }
        /// <summary>
        /// redirect to original object
        /// </summary>
        /// <param name="binder"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            
            var field = t.GetDeclaredField(binder.Name);
            
            lock (myLock)
            {
                result = field.GetValue(o);
            }
            return true;
        }
        /// <summary>
        /// redirects to original object
        /// </summary>
        /// <param name="binder"></param>
        /// <param name="args"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            var method = t.GetDeclaredMethod(binder.Name);

            lock (myLock)
            {
                result = method.Invoke(this.o, args);
            }
            return true;
            //return base.TryInvokeMember(binder, args, out result);
        }
    }
}
