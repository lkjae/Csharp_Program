using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCP_Chatting_Client
{
    public partial class Client : Form
    {
        private Socket socket;
        private Thread receiveThread;
        public Client()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void Client_Load(object sender, EventArgs e)
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Loopback, 12300);

            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(endPoint);

            receiveThread = new Thread(new ThreadStart(Receive));
            receiveThread.IsBackground = true;
            receiveThread.Start();              
        }
        private void Receive()
        {
            while (true)
            {
                byte[] recvBytes = new byte[1024];
                int nRec = socket.Receive(recvBytes);

                string txt = Encoding.UTF8.GetString(recvBytes);
                ShowMsg("You : " + txt);
            }
        }
        private void ShowMsg(string msg)
        {
            richTextBox1.AppendText(msg);
            richTextBox1.AppendText("\r\n");            
            
            textBox2.Focus();
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();          
        }
        private void button1_Click(object sender, EventArgs e)
        {
            byte[] sendBuffer = Encoding.UTF8.GetBytes(textBox2.Text.Trim());
            socket.Send(sendBuffer);            
            ShowMsg("Me : " + textBox2.Text);
            textBox2.Text = "";            
        }
    }
}
