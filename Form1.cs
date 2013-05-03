using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;

namespace FileMergeTest
{
    public partial class Form1 : Form
    {
        string localPersistentPath = null;
        string workingDir = Environment.CurrentDirectory;
        public Form1()
        {
            InitializeComponent();
        }
        private void btnMerge_Click(object sender, EventArgs e)
        {
            var localUniverse = File.ReadLines(localPersistentPath);
            var externalUniverse = File.ReadAllLines(workingDir + "\\tmp\\persistent.sfs");
            var onlyInFileA = localUniverse.Except(externalUniverse);
            File.WriteAllLines("\\tmp\\merged.sfs", externalUniverse.Concat(onlyInFileA));
        }

        public void Form1_Load(object sender, EventArgs e)
        {   
            
            MessageBox.Show(new Form1() { TopMost = true }, "Please select the KSP Saves folder that is or will be your local CloudJeb universe.");
            Directory.CreateDirectory(workingDir + "\\tmp");
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();

            localPersistentPath = fbd.SelectedPath + "\\persistent.sfs";
            MessageBox.Show("You have selected " + localPersistentPath + " as your CloudJeb universe. Click OK to continue.");
            
        }

        private void btnGetRemotePersistent_Click(object sender, EventArgs e)
        {
           //download remote universe           

            getfile gf1 = new getfile();
            gf1.S3get();

            
        //merge remote universe and local universe

            var localUniverse = File.ReadLines(localPersistentPath);
            var externalUniverse = File.ReadAllLines(workingDir + "\\tmp\\persistent.sfs");
            var onlyInFileA = localUniverse.Except(externalUniverse);
            File.WriteAllLines(workingDir + "\\tmp\\merged.sfs", externalUniverse.Concat(onlyInFileA));
            File.Copy(workingDir + "\\tmp\\merged.sfs", localPersistentPath, true);
            MessageBox.Show(new Form1() { TopMost = true }, "Your local universe is now sync'd with the CloudJeb universe.");
             
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
          //download remote universe. Incase someone has updated it since you downloaded it. 
            getfile gf1 = new getfile();
            gf1.S3get();
          //merge local and newly downloaded remote. 

            var localUniverse = File.ReadLines(localPersistentPath);
            var externalUniverse = File.ReadAllLines(workingDir + "\\tmp\\persistent.sfs");
            var onlyInFileA = localUniverse.Except(externalUniverse);
            //make dir
            Directory.CreateDirectory(workingDir + "\\merged");
            File.WriteAllLines(workingDir + "\\merged\\persistent.sfs", externalUniverse.Concat(onlyInFileA));

            //At thispoint persistent.sfs in the merged folder is the remote universe plus local play session time. Now upload persistent.sfs to ftp.
            getfile gf2 = new getfile();
            gf2.S3put();
            
            
            MessageBox.Show("Merged and uploaded your universe with the CloudJeb Universe.");

            

        }

       
    }
}