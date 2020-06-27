using AutomationLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Automation;
using System.Windows.Forms;

namespace Inspector
{
    public partial class Form1 : Form
    {
        readonly AutomationLibrary.Inspector spy = new AutomationLibrary.Inspector();
        readonly AutomationHelper helper = new AutomationHelper();
        AutomationElement rootWindow;

        public Form1()
        {
            InitializeComponent();
            var types = helper.ListTypes();
            comboBox1.DataSource = types;
            comboBox1.DisplayMember = "ProgrammaticName";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            rootWindow = helper.FindWindowByCaption(textBox1.Text);

            textBox2.Lines = spy.InspectElement(rootWindow, ";").ToArray();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var filename = Path.Combine(desktop, "hello.csv");
            File.WriteAllText(filename, textBox2.Text);
            Process.Start(filename);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var items = rootWindow.GetByType(comboBox1.SelectedValue);

            var x = new List<string>();

            foreach (AutomationElement item in items)
            {
                x.AddRange(spy.InspectElement(item, ";"));
            }

            textBox2.Lines = x.ToArray();
        }
    }
}
