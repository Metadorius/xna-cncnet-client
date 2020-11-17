using Rampastring.XNAUI.XNAControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rampastring.XNAUI;
using Rampastring.Tools;
using System.Diagnostics;

namespace ClientGUI
{
    public class XNALinkButton : XNAClientButton
    {
        public XNALinkButton(WindowManager windowManager) : base(windowManager)
        {
        }

        public string URL { get; set; }

        public ToolTip ToolTip { get; set; }

        public override void Initialize()
        {
            base.Initialize();

            ToolTip = new ToolTip(WindowManager, this);
        }

        public override void ParseAttributeFromINI(IniFile iniFile, string key, string value)
        {
            if (key == "URL")
            {
                URL = value;
                return;
            }
            else if (key == "ToolTip")
            {
                ToolTip.Text = value.Replace("@", Environment.NewLine);
                return;
            }

            base.ParseAttributeFromINI(iniFile, key, value);
        }

        public override void ParseLocalizedAttributes()
        {
            base.ParseLocalizedAttributes();

            string value = LocaleProvider.GetLocalizedAttributeValue(Parent?.Name, Name, nameof(ToolTip),
                ToolTip?.Text?.Replace(Environment.NewLine, "@") ?? string.Empty, true);
            ToolTip.Text = value?.Replace("@", Environment.NewLine) ?? string.Empty;
        }

        public override void OnLeftClick()
        {
            Process.Start(URL);
            base.OnLeftClick();
        }
    }
}
