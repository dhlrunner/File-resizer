using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp9
{
   
    public partial class Form1 : Form
    {
        string filename = "";
        long filebyte = 0;
        long targetfilebyte = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            
            filename = openFileDialog1.FileName;
            var info = new FileInfo(filename);
            label1.Text = Path.GetFileName(filename);
            filebyte = info.Length;
            label4.Text = filebyte.ToString()+" Bytes";
            if(int.Parse(textBox1.Text) < filebyte)
            {
                textBox1.Text = filebyte.ToString();
            }
            button2.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(filebyte > targetfilebyte)
            {
                MessageBox.Show("맞출 크기는 원본 파일 용량보다 작을 수 없습니다.");
            }
            else
            {
                
                BinaryReader fileread = new BinaryReader(File.Open(filename, FileMode.Open));
                List<byte> newfile = new List<byte>();
                byte[] raw = new byte[filebyte];
                raw = fileread.ReadBytes(Convert.ToInt32(filebyte));
                fileread.Close();
                newfile = raw.ToList();
                for (int i = 0; i < (targetfilebyte - filebyte); i++)
                {
                    newfile.Add(0x00);
                }
                var info = new FileInfo(filename);
                if (checkBox1.Checked)
                {
                    info.CopyTo(filename + ".bak");
                }
                info.Delete();
                byte[] newnewfile = new byte[filebyte + (targetfilebyte - filebyte)];
                newnewfile = newfile.ToArray();
                BinaryWriter filewrite = new BinaryWriter(File.Open(filename, FileMode.Create));
                filewrite.Write(newnewfile);
                filewrite.Close();
                MessageBox.Show("성공");
                button2.Enabled = false;
            }
          

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(textBox1.Text != "")
             targetfilebyte = long.Parse(textBox1.Text);
        }
    }
}
