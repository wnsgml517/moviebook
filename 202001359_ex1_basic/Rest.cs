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

namespace _202001359_ex1_basic
{
    public partial class Rest : Form
    {
        int sel=100;
        Dictionary<string, string> dic = new Dictionary<string, string>();
        List<string> url = new List<string>();
        public Rest()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            wmp2.Hide();
            string line;
            string[] temp = new string[2];            
            OpenFileDialog ofd = new OpenFileDialog();
            if(ofd.ShowDialog() == DialogResult.OK)
            {
                StreamReader sr = new StreamReader(ofd.FileName);
                while ((line = sr.ReadLine()) != null)
                {
                    temp = line.Split(' ');
                    dic.Add(temp[0], temp[1]);
                }
            }
            foreach (KeyValuePair<string, string >kvp in dic)
            {
                listBox1.Items.Add(kvp.Value);
                url.Add(kvp.Key);
            }

            int sel = listBox1.SelectedIndex;
            button3.Enabled = false;
            button2.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sel = listBox1.SelectedIndex;
            if (sel == 100)
            {
                MessageBox.Show("재생할 영화 예고편을 선택해주세요");
                return;
            }
            wmp.URL = url[sel];
            wmp.Ctlcontrols.play();
            button3.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            wmp.Ctlcontrols.stop();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            wmp.Ctlcontrols.pause();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            wmp2.Show();
            multi moviePlayer = new multi();
            moviePlayer.Start(wmp,wmp2,url);

        }
    }
}
