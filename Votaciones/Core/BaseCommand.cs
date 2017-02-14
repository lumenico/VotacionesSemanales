using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Votaciones.Core
{   
    public interface ResultWrapperValueAsObject
    {
        object valueAsObject();
    }

    public class CommandArray
    {
        public CommandArray()
        {
        }
        public BaseCommand<object>[] Commands { get; set; }    
    }

    public interface BaseCommand<out T> where T : class
    {
        T execute();
    }

    public class ResultWrapper<T> : ResultWrapperValueAsObject where T : struct
    {
        public T value
        {
            get { return m_value; }
            private set { m_value = value; }
        }

        private T m_value;

        public object valueAsObject()
        {
            return m_value;
        }

        public ResultWrapper(T value)
        {
            this.value = value;
        }

        public static implicit operator ResultWrapper<T>(T value)
        {
            return new ResultWrapper<T>(value);
        }

        public static implicit operator T(ResultWrapper<T> wrapper)
        {
            return wrapper.value;
        }
    }   
}