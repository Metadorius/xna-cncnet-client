﻿using ClientGUI;
using Rampastring.XNAUI.XNAControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rampastring.XNAUI;
using Microsoft.Xna.Framework;

namespace DTAClient.DXGUI.Multiplayer.CnCNet
{
    public class PasswordRequestWindow : XNAWindow
    {
        private XNATextBox tbPassword;

        public PasswordRequestWindow(WindowManager windowManager) : base(windowManager)
        {
        }

        public override void Initialize()
        {
            Name = "PasswordRequestWindow";
            ClientRectangle = new Rectangle(0, 0, 150, 90);

            var lblDescription = new XNALabel(WindowManager);
            lblDescription.Name = "lblDescription";
            lblDescription.ClientRectangle = new Rectangle(12, 12, 0, 0);
            lblDescription.Text = "Please enter the password below and click OK.";

            tbPassword = new XNATextBox(WindowManager);
            tbPassword.Name = "tbPassword";
            tbPassword.ClientRectangle = new Rectangle(lblDescription.ClientRectangle.X,
                lblDescription.ClientRectangle.Bottom + 12, ClientRectangle.Width - 24, 21);

            var btnOK = new XNAClientButton(WindowManager);
            btnOK.Name = "btnOK";
            btnOK.ClientRectangle = new Rectangle(lblDescription.ClientRectangle.X,
                ClientRectangle.Bottom - 35, 92, 23);

            var btnCancel = new XNAClientButton(WindowManager);
            btnCancel.Name = "btnCancel";
            btnCancel.ClientRectangle = new Rectangle(ClientRectangle.Width - 104,
                btnOK.ClientRectangle.Y, 92, 23);

            AddChild(lblDescription);
            AddChild(tbPassword);
            AddChild(btnOK);
            AddChild(btnCancel);

            base.Initialize();
        }
    }
}