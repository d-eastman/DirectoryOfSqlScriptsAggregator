using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SqlScriptAggregator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string dir = txtDir.Text;
            if (Directory.Exists(dir))
            {
                string[] files = Directory.GetFiles(dir, "*.sql").OrderBy(x => x).ToArray();
                if (files != null && files.Length > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (string f in files)
                    {
                        using (StreamReader reader = new StreamReader(f))
                        {
                            sb.AppendLine("/*");
                            sb.AppendLine(" *");
                            sb.AppendLine(" * Begin File: " + f);
                            sb.AppendLine(" *");
                            sb.AppendLine(" */");
                            sb.AppendLine(reader.ReadToEnd());
                            sb.AppendLine("/*");
                            sb.AppendLine(" *");
                            sb.AppendLine(" * End File: " + f);
                            sb.AppendLine(" *");
                            sb.AppendLine(" */");
                        }
                    }
                    Clipboard.SetText(sb.ToString());
                    MessageBox.Show(String.Format("{1} .sql files in {0} aggregated to the clipboard", dir, files.Length), "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Directory '" + dir + "' does not contain any .sql files!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Directory '" + dir + "' does not exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
