using System;
using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace ThreadSafeObject
{
    /// <summary>
    /// Making any call to a function thread safe
    /// Compatible .NET Standard 1.2
    /// Means .NET Framework>=4.5.1
    /// Means .NET Framework>=1.0
    /// Test and details at https://github.com/ignatandrei/ThreadSafeObject
    /// </summary>
    public class ThreadSafe: DynamicObject
    {
        private readonly object o;
        private readonly TypeInfo t;
        private readonly object myLock;

        /// <summary>
        /// Initializes a new instance of the <see cref="ThreadSafe"/> class.
        /// </summary>
        /// <param name="o">
        /// The wrapped object whose operations will be made thread-safe.
        /// </param>
        public ThreadSafe(object o)
        {
            this.o = o;
            t = o.GetType().GetTypeInfo();
            myLock = new object();
        }

        /// <inheritdoc />
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            var prop = t.GetDeclaredProperty(binder.Name);
            if (prop != null)
            {
                lock (myLock)
                {
                    prop.SetValue(o, value);
                }

                return true;
            }

            var field = t.GetDeclaredField(binder.Name);
            if (field != null)
            {
                lock (myLock)
                {
                    field.SetValue(o, value);
                }

                return true;
            }

            return false;
        }

        /// <inheritdoc />
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            var prop = t.GetDeclaredProperty(binder.Name);
            if (prop != null)
            {
                lock (myLock)
                {
                    result = prop.GetValue(o);
                }

                return true;
            }

            var field = t.GetDeclaredField(binder.Name);
            if (field != null)
            {
                lock (myLock)
                {
                    result = field.GetValue(o);
                }

                return true;
            }

            result = null;
            return false;
        }

        /// <summary>
        /// Attempts to find the indexer property.
        /// </summary>
        /// <returns>
        /// The <see cref="PropertyInfo"/> for the indexer property, if found;
        /// otherwise, <see langword="null"/>.
        /// </returns>
        private PropertyInfo GetIndexedProperty()
        {
            // TODO: Is there a better way to do this?

            var prop = t.GetDeclaredProperty("Item");
            if (prop == null || prop.GetIndexParameters().Length == 0)
            {
                prop = t.DeclaredProperties.FirstOrDefault(p => p.GetIndexParameters().Length != 0);
            }

            return prop;
        }

        /// <inheritdoc />
        public override bool TrySetIndex(SetIndexBinder binder, object[] indexes, object value)
        {
            var prop = GetIndexedProperty();
            if (prop != null)
            {
                lock (myLock)
                {
                    prop.SetValue(o, value, indexes);
                }

                return true;
            }

            return false;
        }

        /// <inheritdoc />
        public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object result)
        {
            var prop = GetIndexedProperty();
            if (prop != null)
            {
                lock (myLock)
                {
                    result = prop.GetValue(o, indexes);
                }

                return true;
            }

            result = null;
            return false;
        }

        /// <inheritdoc />
        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            var method = t.GetDeclaredMethod(binder.Name);
            if (method != null)
            {
                lock (myLock)
                {
                    result = method.Invoke(o, args);
                }

                return true;
            }

            result = null;
            return false;
        }
    }
}
