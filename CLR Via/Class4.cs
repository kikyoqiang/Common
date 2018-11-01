﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace CLR_Via
{
    class Class4
    {
        static void Main()
        {
            DiskFullException.TestException();
            Console.ReadKey();
        }
    }
    [Serializable]
    public sealed class DiskFullException : ExceptionArgs
    {
        private readonly string m_diskPath;

        public DiskFullException(string diskPath)
        {
            this.m_diskPath = diskPath;
        }

        public string DiskPath { get { return m_diskPath; } }

        public override string Message
        {
            get
            {
                return m_diskPath == null ? base.Message : "DiskPath = " + m_diskPath;
            }
        }

        public static void TestException()
        {
            try
            {
                throw new Exception<DiskFullException>(new DiskFullException(@"C:\"), "this disk is full");
            }
            catch (Exception<DiskFullException> ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
    public sealed class Exception<TExceptionArgs> : Exception, ISerializable where TExceptionArgs : ExceptionArgs
    {
        private const string c_args = "Args";
        private readonly TExceptionArgs m_args;

        public TExceptionArgs Args { get { return m_args; } }

        public Exception(string message = null, Exception innerException = null) : this(null, message, innerException)
        {

        }

        public Exception(TExceptionArgs args, string message = null, Exception innerException = null) : base(message, innerException)
        {
            m_args = args;
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        private Exception(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            m_args = (TExceptionArgs)info.GetValue(c_args, typeof(TExceptionArgs));
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(c_args, m_args);
            base.GetObjectData(info, context);
        }

        public override string Message
        {
            get
            {
                string baseMsg = base.Message;
                return (m_args == null) ? baseMsg : string.Format("{0} {1} ", baseMsg, m_args.Message);
            }
        }

        public override bool Equals(object obj)
        {
            Exception<TExceptionArgs> other = obj as Exception<TExceptionArgs>;
            if (other == null)
                return false;
            return object.Equals(m_args, other.m_args) && base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
    [Serializable]
    public abstract class ExceptionArgs
    {
        public virtual string Message { get { return string.Empty; } }
    }
}
