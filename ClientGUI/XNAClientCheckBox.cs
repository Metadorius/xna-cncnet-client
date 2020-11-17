using Rampastring.XNAUI.XNAControls;
using Rampastring.XNAUI;
using System;
using Rampastring.Tools;

namespace ClientGUI
{
    public class XNAClientCheckBox : XNACheckBox
    {
        public ToolTip ToolTip { get; set; }

        public XNAClientCheckBox(WindowManager windowManager) : base(windowManager)
        {
        }

        public override void Initialize()
        {
            CheckSoundEffect = new EnhancedSoundEffect("checkbox.wav");

            base.Initialize();

            ToolTip = new ToolTip(WindowManager, this);
        }

        public override void ParseAttributeFromINI(IniFile iniFile, string key, string value)
        {
            if (key == "ToolTip")
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
    }
}
