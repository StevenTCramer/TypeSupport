﻿using System;
using System.Collections.Generic;
using System.Reflection;

namespace TypeSupport
{
    /// <summary>
    /// Discovers the attributes of a field and provides access to field metadata
    /// </summary>
    public class ExtendedField
    {
        private FieldInfo _fieldInfo;

        public FieldInfo FieldInfo
        {
            get { return _fieldInfo; }
        }

        /// <summary>
        /// Gets the name of the current member
        /// </summary>
        public string Name => _fieldInfo.Name;

        /// <summary>
        /// Gets the type of this field object
        /// </summary>
        public Type Type => _fieldInfo.FieldType;

        /// <summary>
        /// Gets the class object that was used to obtain this instance of MemberInfo
        /// </summary>
        public Type ReflectedType => _fieldInfo.ReflectedType;

        /// <summary>
        /// Gets a collection that contains this member's custom attributes
        /// </summary>
        IEnumerable<CustomAttributeData> CustomAttributes => _fieldInfo.CustomAttributes;

        /// <summary>
        /// True if this field backs an auto-property
        /// </summary>
        public bool IsBackingField { get; private set; }

        /// <summary>
        /// For backing fields, the property name it stores data for
        /// </summary>
        public string BackedPropertyName { get; private set; }

        /// <summary>
        /// Create an extended field
        /// </summary>
        /// <param name="fieldInfo"></param>
        /// <param name="value"></param>
        public ExtendedField(FieldInfo fieldInfo)
        {
            _fieldInfo = fieldInfo;
            if (fieldInfo.Name.Contains("k__BackingField"))
            {
                IsBackingField = true;
                var i = fieldInfo.Name.IndexOf("<");
                var end = fieldInfo.Name.IndexOf(">", i + 1);
                BackedPropertyName = fieldInfo.Name.Substring(i + 1, end - (i + 1)); 
            }
        }

        public static explicit operator ExtendedField(FieldInfo fieldInfo)
        {
            if (fieldInfo == null)
                return null;
            return new ExtendedField(fieldInfo);
        }

        public static explicit operator FieldInfo(ExtendedField extendedField)
        {
            if (extendedField == null)
                return null;
            return extendedField._fieldInfo;
        }

        public override string ToString()
        {
            return $"{ReflectedType}.{Name}";
        }
    }
}