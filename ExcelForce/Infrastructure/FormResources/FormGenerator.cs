using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ExcelForce.Infrastructure.FormResources
{
    public static class FormGenerator
    {
        private static Dictionary<string, Form> _formContainer;

        public static T GetForm<T>() where T : Form, new()
        {
            _formContainer = _formContainer ?? new Dictionary<string, Form>();

            var key = GetKeyName<T>();

            if (_formContainer.ContainsKey(key))
            {
                return _formContainer[key] as T;
            }

            var formInstance = new T();

            _formContainer.Add(key, formInstance);

            return _formContainer[key] as T;
        }

        public static bool ClearForm<T>() where T : Form, new()
        {
            if (_formContainer == null)
                return true;

            var key = GetKeyName<T>();

            try
            {
                _formContainer.Remove(key);

                return true;
            }
            catch (Exception ex)
            {
                //TODO:(Ritwik) :: To do logging here
                return false;
            }
        }

        private static string GetKeyName<T>() where T : Form
            => typeof(T).GetType().AssemblyQualifiedName;
    }
}
