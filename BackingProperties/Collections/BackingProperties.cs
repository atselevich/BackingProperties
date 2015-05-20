// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BackingProperties.cs" company="">
//   
// </copyright>
// <summary>
//   The module settings base.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BackingProperties
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    /// <summary>
    ///     The module settings base.
    /// </summary>
    public class BackingProperties : ConcurrentDictionary<string, object>
    {
        #region Public Methods and Operators

        /// <summary>
        /// The get cached value.
        /// </summary>
        /// <param name="method">
        /// The method.
        /// </param>
        /// <param name="propertyName">
        /// The property name.
        /// </param>
        /// <typeparam name="T">
        /// The object
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public T GetPropertyValue<T>(Func<T> method, [CallerMemberName] string propertyName = null)
        {
            if (this.ContainsKey(propertyName))
            {
                // ReSharper disable once ExplicitCallerInfoArgument
                var value = this.GetPropertyValue<T>(propertyName);

                return value;
            }

            var newValue = method();
            this.SetPropertyValue(newValue, propertyName);

            return newValue;
        }

        /// <summary>
        /// The get property value.
        /// </summary>
        /// <param name="propertyName">
        /// The property name.
        /// </param>
        /// <typeparam name="T">
        /// The object
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if argument is null
        /// </exception>
        public T GetPropertyValue<T>([CallerMemberName] string propertyName = null)
        {
            if (propertyName == null)
            {
                throw new ArgumentNullException("propertyName");
            }

            object value;
            if (this.TryGetValue(propertyName, out value))
            {
                return (T)value;
            }

            return default(T);
        }

        /// <summary>
        /// The get cached value.
        /// </summary>
        /// <param name="method">
        /// The method.
        /// </param>
        /// <param name="defaultValue">
        /// The default Value.
        /// </param>
        /// <param name="propertyName">
        /// The property name.
        /// </param>
        /// <typeparam name="T">
        /// The object
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public T GetPropertyValueOrDefault<T>(
            Func<T> method, 
            T defaultValue, 
            [CallerMemberName] string propertyName = null)
        {
            // ReSharper disable ExplicitCallerInfoArgument
            if (this.ContainsKey(propertyName))
            {
                var value = this.GetPropertyValue<T>(propertyName);
                return value;
            }

            var newValue = method();
            if (newValue == null)
            {
                return defaultValue;
            }

            this.SetPropertyValue(newValue, propertyName);

            return newValue;
        }

        /// <summary>
        /// The set property value.
        /// </summary>
        /// <param name="newValue">
        /// The new value.
        /// </param>
        /// <param name="propertyName">
        /// The property name.
        /// </param>
        /// <typeparam name="T">
        /// The object
        /// </typeparam>
        /// <exception cref="ArgumentNullException">
        /// Thrown if argument is null
        /// </exception>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public T SetPropertyValue<T>(T newValue, [CallerMemberName] string propertyName = null)
        {
            if (propertyName == null)
            {
                throw new ArgumentNullException("propertyName");
            }

            if (!EqualityComparer<T>.Default.Equals(newValue, this.GetPropertyValue<T>(propertyName)))
            {
                this[propertyName] = newValue;
            }

            return newValue;
        }

        /// <summary>
        /// The remove.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool Remove(string key)
        {
            return ((IDictionary<string, object>)this).Remove(key);
        }

        #endregion
    }
}