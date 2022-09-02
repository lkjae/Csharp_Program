using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace WindowsFormsApp5
{
    public partial class Form1 : Form
    {
        WindowsMediaPlayer wmp = new WindowsMediaPlayer();
        int a = 0;        
        private int iTick = 0;

        string[] Pic = new string[4]
       {
            @"C:\\Temp/vancouver.PNG","C:\\Temp/호랑수월가.PNG","C:\\Temp/사랑은 늘 도망가.PNG", "C:\\Temp/fromnyx.PNG"
       };

        string[] Music = new string[4]
        {
            @"C:\\Temp/vancouver.mp3","C:\\Temp/호랑수월가.mp3","C:\\Temp/사랑은 늘 도망가.mp3","C:\\Temp/나의x에게.mp3"
        };

        public void musicControl(int a)
        {
            pictureBox1.Image = Image.FromFile(Pic[a]);
            wmp.URL = Music[a];
            wmp.controls.play();
        }


        public Form1()
        {
            InitializeComponent();          
        }

        private void Form1_Load(object sender, EventArgs e)
        {                       
            pictureBox1.Image = Image.FromFile(Pic[0]);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void iTimer_Tick(object sender, EventArgs e)
        {
            iTick += 1;
            tickChecker.Text = iTick.ToString();    
            if(iTick == 60)
            {
                a++;
                if (a == 4) a = 0;
                iTick = 0;
                musicControl(a); 
            }           
        }

        private void Music_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            switch (btn.Name)
            {
                case "startMusic":
                    Timer oTimer = new Timer();
                    oTimer.Enabled = true;
                    oTimer.Interval = 1000;
                    oTimer.Tick += iTimer_Tick;
                    oTimer.Start();
                    wmp.controls.stop();
                    int start_num = 0;
                    musicControl(start_num);                  
                    break;
                
                case "nextMusic":
                    iTick = 0;
                    wmp.controls.stop();
                    a++;
                    if (a > 3) a = 0;
                    musicControl(a);                  
                    break;
               
                case "preMusic":
                    iTick = 0;
                    wmp.controls.stop();
                    a--;
                    if (a < 0) a = 3;
                    musicControl(a);
                    break;
            }

        }
    }

}


