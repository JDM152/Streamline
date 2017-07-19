using System.Reflection;

namespace SeniorDesign.FrontEnd.Components.AttributeEditors
{
    /// <summary>
    ///     A wrapper that can discern between Field and Method info for use
    ///     with custom attributes and attribute editors
    /// </summary>
    public class WrappedAttributeInfo
    {
        /// <summary>
        ///     If this wraps a field
        /// </summary>
        public bool IsField { get { return _finfo != null; } }

        /// <summary>
        ///     If this wraps a Property
        /// </summary>
        public bool IsProperty { get { return _pinfo != null; } }

        /// <summary>
        ///     The field Info that this holds (if any)
        /// </summary>
        private FieldInfo _finfo;

        /// <summary>
        ///     The property info that this holds (if any)
        /// </summary>
        private PropertyInfo _pinfo;

        /// <summary>
        ///     Wraps Field Info for use with attributes
        /// </summary>
        /// <param name="f">The FieldInfo to wrap</param>
        public WrappedAttributeInfo(FieldInfo f)
        {
            _finfo = f;
        }

        /// <summary>
        ///     Wraps Property Info for use with attribtues
        /// </summary>
        /// <param name="p">The Property Info to wrap</param>
        public WrappedAttributeInfo(PropertyInfo p)
        {
            _pinfo = p;
        }

        /// <summary>
        ///     Sets the value of this wrapped attribute on a specific object
        /// </summary>
        /// <param name="owner">The object to set the value on</param>
        /// <param name="value">The value to set</param>
        public void SetValue(object owner, object value)
        {
            if (_finfo != null)
                _finfo.SetValue(owner, value);
            else
                _pinfo.SetValue(owner, value);
        }

        /// <summary>
        ///     Gets the value of this wrapped attribute on a specific object
        /// </summary>
        /// <param name="owner">The object to get the value of</param>
        /// <returns>The value of this attribute on the object</returns>
        public object GetValue(object owner)
        {
            if (_finfo != null)
                return _finfo.GetValue(owner);
            else
                return _pinfo.GetValue(owner);
        }
    }
}
