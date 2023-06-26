using System.Runtime.InteropServices;
using System;
using System.Text;

namespace 窗口获取句柄_2_
{
    public partial class Form1 : Form
    {
        
        IntPtr handle;
        uint processId;
        [DllImport("user32.dll")]
        static extern IntPtr FindWindow(string lpClassName,string lpWindowName);

        [DllImport("user32.dll")]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);
        [DllImport("kernel32.dll")]
        static extern bool VirtualProtect(IntPtr lpAddress, UIntPtr dwSize, uint flNewProtect, out uint lpflOldProtect);

        [DllImport("kernel32.dll")]
        static extern bool VirtualQuery(IntPtr lpAddress, out MEMORY_BASIC_INFORMATION lpBuffer, UIntPtr dwLength);
        [StructLayout(LayoutKind.Sequential)]
        public struct MEMORY_BASIC_INFORMATION
        {
            public IntPtr BaseAddress;
            public IntPtr AllocationBase;
            public uint AllocationProtect;
            public IntPtr RegionSize;
            public uint State;
            public uint Protect;
            public uint Type;
        }
        public Form1()
        {
            InitializeComponent();
            this.Icon = new Icon("D:\\A___C++_project\\窗口获取句柄（2）\\窗口获取句柄（2）\\favicon.ico");
            foreach (Control control in this.Controls)
            {
                if (control is TextBox)
                {
                    ((TextBox)control).BorderStyle = BorderStyle.FixedSingle;
                }
            }
            this.Text = "查找物理地址";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
           
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
            handle = FindWindow("" , this.textBox1.Text);

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
           
        }
        





        private void button1_Click(object sender, EventArgs e)
        {
            GetWindowThreadProcessId(handle, out processId);
            this.textBox2.Text = processId.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (processId == 0)
            {
                MessageBox.Show("id有误");
            }
            else
            {
                if (textBox3.Text != null)
                {
                    string hexStr = this.textBox3.Text; // 输入框中输入的16进制数
                    int virtualAddrInt = Convert.ToInt32(hexStr, 16); // 解析16进制整数
                    IntPtr virtualAddress = new IntPtr(virtualAddrInt); // 转换为IntPtr类型
                    MEMORY_BASIC_INFORMATION memInfo;
                    VirtualQuery(virtualAddress, out memInfo, new UIntPtr((uint)Marshal.SizeOf(typeof(MEMORY_BASIC_INFORMATION))));
                    IntPtr physicalAddress = memInfo.BaseAddress;
                    this.textBox4.Text = physicalAddress.ToInt64().ToString();
                }
                else
                {
                    textBox3.Text = "未输入虚拟地址";
                }
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (handle != IntPtr.Zero)
            {

                this.textBox5.Text =  handle.ToString();
            }
            else
            {
                this.textBox5.Text = "未找到窗口句柄";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            foreach (Control control in this.Controls)
            {
                if (control is TextBox)
                {
                    ((TextBox)control).Text = string.Empty;
                }
            }
        }

        
    }
}