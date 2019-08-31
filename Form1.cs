using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using fileReader;

/*
 * I'm not super good at organization, and this is the first time I have used the file explorer so sorry if it's a little messy :(
 * the only windows dependent stuff is the forms UI so you could easily port it to Linux or MacOS if you wanted to,
 * I hope you enjoy the software!
 * 
 *  --Alex AKA BBQGiraffe
 */


namespace N3D_UIFExtractor
{
    
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }
        OpenFileDialog ofd = new OpenFileDialog();
        private void Button1_Click(object sender, EventArgs e)
        {
            
            ofd.Filter = "DAT|*.DAT";
            if(ofd.ShowDialog() == DialogResult.OK)
            {
                
                //textBox1.Text = ofd.SafeFileName;
                String directory = ofd.FileName;
                String file = ofd.SafeFileName;
                String output = "Successfully extracted to C:\\Nitemare!";
                String whole = (directory);
                textBox1.Text = whole;
                FileReader.Extract(whole, @"C:\Nitemare", file);
                
                MessageBox.Show(output, "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
