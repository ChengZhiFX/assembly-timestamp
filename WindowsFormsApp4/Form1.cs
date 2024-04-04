using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp4.Properties;

namespace WindowsFormsApp4;

public class Form1 : Form
{
	private Label timeLabel;

	private TextBox nameTextBox;

	private Button showTimeButton;

	private IContainer components = null;

	public Form1()
	{
		InitializeComponent();
		InitializeControls();
		base.Load += Form1_Load;
		base.FormBorderStyle = FormBorderStyle.FixedSingle;
		base.MaximizeBox = false;
		base.MinimizeBox = true;
		BackgroundImage = Resources.BackgroundImage;
		BackgroundImageLayout = ImageLayout.Stretch;
		base.Size = new Size(350, 180);
		Text = "汇编实验时间戳";
		base.StartPosition = FormStartPosition.CenterScreen;
		base.Icon = Resources.clock;
	}

	private void InitializeControls()
	{
		int num = (Screen.PrimaryScreen.WorkingArea.Width - base.Width) / 2;
		int num2 = (Screen.PrimaryScreen.WorkingArea.Height - base.Height) / 2;
		timeLabel = new Label
		{
			Location = new Point(1, 20),
			Size = new Size(600, 80),
			Text = "请输入姓名",
			Font = new Font("黑体", 14f, FontStyle.Bold),
			ForeColor = Color.DarkBlue,
			BackColor = Color.Transparent
		};
		nameTextBox = new TextBox
		{
			Location = new Point(90, 50),
			Size = new Size(150, 50),
			Text = "请输入姓名",
			Font = new Font("宋体", 10f, FontStyle.Bold)
		};
		nameTextBox.GotFocus += NameTextBox_GotFocus;
		nameTextBox.BringToFront();
		showTimeButton = new Button
		{
			Location = new Point(180, 100),
			Size = new Size(100, 23),
			Text = "时 间 戳",
			DialogResult = DialogResult.OK
		};
		showTimeButton.Click += async delegate(object sender, EventArgs e)
		{
			await ShowTimeButton_ClickAsync(sender, e);
		};
		base.Controls.Add(nameTextBox);
		base.Controls.Add(timeLabel);
		base.Controls.Add(showTimeButton);
	}

	private void Form1_Load(object sender, EventArgs e)
	{
	}

	private void NameTextBox_GotFocus(object sender, EventArgs e)
	{
		nameTextBox.Text = string.Empty;
		nameTextBox.GotFocus -= NameTextBox_GotFocus;
	}

	private async Task ShowTimeButton_ClickAsync(object sender, EventArgs e)
	{
		try
		{
			string fullName = nameTextBox.Text.Trim();
			if (string.IsNullOrEmpty(fullName))
			{
				MessageBox.Show("Please enter your name.");
				return;
			}
			nameTextBox.Visible = false;
			DateTime networkTime = await GetNetworkTimeAsync("ntp.aliyun.com");
			string formattedTime = networkTime.ToString("yyyy-MM-dd HH:mm:ss");
			string dayformattedTime = networkTime.ToString("yyyy-MM-dd");
			string secondformattedTime = networkTime.ToString("HH:mm:ss");
			string lastName = ((fullName.Length > 0) ? fullName[0].ToString() : string.Empty);
			string firstName = ((fullName.Length > 1) ? fullName.Substring(1) : string.Empty);
			timeLabel.Text = "姓" + lastName + "名" + firstName + " " + dayformattedTime + "\n温馨提示现在是\n" + fullName + secondformattedTime + "作答《汇编程序》\n" + formattedTime + " 请劳逸结合 ";
		}
		catch (Exception ex2)
		{
			Exception ex = ex2;
			Console.WriteLine("Error in ShowTimeButton_ClickAsync: " + ex.Message);
			MessageBox.Show("An error occurred while fetching network time.");
		}
	}

	private async Task<DateTime> GetNetworkTimeAsync(string ntpServer)
	{
		byte[] ntpData = new byte[48];
		ntpData[0] = 27;
		IPAddress ipAddress = (await Dns.GetHostAddressesAsync(ntpServer)).FirstOrDefault((IPAddress addr) => addr.AddressFamily == AddressFamily.InterNetwork);
		if (ipAddress == null)
		{
			throw new Exception("No suitable IP address found for NTP server.");
		}
		using (UdpClient client = new UdpClient())
		{
			client.Connect(ipAddress, 123);
			await client.SendAsync(ntpData, ntpData.Length);
			ntpData = (await client.ReceiveAsync()).Buffer;
		}
		if (BitConverter.IsLittleEndian)
		{
			Array.Reverse(ntpData, 40, 4);
			Array.Reverse(ntpData, 44, 4);
		}
		ulong intPart = BitConverter.ToUInt32(ntpData, 40);
		ulong fractPart = BitConverter.ToUInt32(ntpData, 44);
		return DateTime.Now;
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		base.SuspendLayout();
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(800, 450);
		base.Name = "Form1";
		this.Text = "Form1";
		base.Load += new System.EventHandler(Form1_Load);
		base.ResumeLayout(false);
	}
}
