﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PoskusCiv2.Imagery;

namespace PoskusCiv2.Forms
{
    public partial class WorldMapForm : Civ2form
    {
        public MainCiv2Window mainCiv2Window;
        DoubleBufferedPanel MinimapPanel;

        public WorldMapForm(MainCiv2Window _mainCiv2Window)
        {
            InitializeComponent();
            mainCiv2Window = _mainCiv2Window;

            Paint += new PaintEventHandler(WorldMapForm_Paint);
            Size = new Size((int)((_mainCiv2Window.ClientSize.Width) * 0.1375), 148);    //-4 is experience setting

            //Minimap panel
            MinimapPanel = new DoubleBufferedPanel
            {
                Location = new Point(8, 35),
                Size = new Size(248, 107),  //correct this!
                BackColor = Color.Black,
                BorderStyle = BorderStyle.Fixed3D
            };
            Controls.Add(MinimapPanel);
            MinimapPanel.Paint += new PaintEventHandler(MinimapPanel_Paint);
        }

        private void WorldMapForm_Load(object sender, EventArgs e) { }

        private void WorldMapForm_Paint(object sender, PaintEventArgs e)
        {
            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;
            e.Graphics.DrawString("World", new Font("Times New Roman", 19), new SolidBrush(Color.Black), new Point(this.Width / 2 + 1, 20), sf);
            e.Graphics.DrawString("World", new Font("Times New Roman", 19), new SolidBrush(Color.FromArgb(135, 135, 135)), new Point(this.Width / 2, 19), sf);
            sf.Dispose();
        }

        private void MinimapPanel_Paint(object sender, PaintEventArgs e) { }

    }
}
