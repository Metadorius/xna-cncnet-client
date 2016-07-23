﻿using Rampastring.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientCore.Settings
{
    /// <summary>
    /// A base class for an INI setting.
    /// </summary>
    public abstract class INISetting<T>
    {
        public INISetting(IniFile iniFile, string iniSection, string iniKey,
            T defaultValue)
        {
            IniFile = iniFile;
            IniSection = iniSection;
            IniKey = iniKey;
            DefaultValue = defaultValue;
        }

        protected IniFile IniFile { get; private set; }
        protected string IniSection { get; private set; }
        protected string IniKey { get; private set; }
        protected T DefaultValue { get; private set; }

        public T Value
        {
            get { return Get(); }
            set { Set(value); }
        }

        protected abstract T Get();

        protected abstract void Set(T value);
    }
}
