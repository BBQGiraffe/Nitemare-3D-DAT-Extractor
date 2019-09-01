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
        bool hasPickedOutput = false;
        String folder = "";
        public Form1()
        {
            InitializeComponent();
        }
        OpenFileDialog ofd = new OpenFileDialog();
        private void Button1_Click(object sender, EventArgs e)
        {
            if (hasPickedOutput == false)
            {
                MessageBox.Show("Please pick an output folder!", "Please pick an output folder!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                ofd.Filter = "DAT|*.DAT";
                if (ofd.ShowDialog() == DialogResult.OK)
                {


                    //textBox1.Text = ofd.SafeFileName;
                    String directory = ofd.FileName;
                    String file = ofd.SafeFileName;
                    String output = "Successfully extracted to C:\\Nitemare!";
                    String whole = (directory);
                    textBox1.Text = whole;

                    FileReader.Extract(whole, folder, file);

                    MessageBox.Show(output, "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public void Button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                folder = dlg.SelectedPath;
                hasPickedOutput = true;
                textBox2.Text = folder;
            }
            else
            {
                // This prevents a crash when you close out of the window with nothing
            }
        }
    }
}
