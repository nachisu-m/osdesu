using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

public class StartMenu : UserControl
{

    private Label titleLabel;
    private PictureBox shutdownBox;


    public StartMenu()
    {
        InitializeComponents();
        this.Hide();
    }

    private void InitializeComponents()
    {
        this.Size = new Size(500,600);
        this.BackColor = Color.Gray;
        this.Location = new System.Drawing.Point(0, Screen.PrimaryScreen.Bounds.Height - 70 - this.Height);


        //タイトルラベルを表示
        titleLabel = new Label();
        titleLabel.Text = "スタートメニュー";
        titleLabel.Font = new Font("Arial", 14, FontStyle.Bold);
        titleLabel.ForeColor = Color.White;
        titleLabel.Size = new Size(200,30);
        titleLabel.Location = new Point(10,10);

        //シャットダウンボタン
        Image shutdownImage = Image.FromFile("pic/shutdown.png");
        shutdownBox = new PictureBox();
        shutdownBox.Image = shutdownImage;
        shutdownBox.Size = new Size(40,40);
        shutdownBox.SizeMode = PictureBoxSizeMode.StretchImage;
        shutdownBox.Location = new Point(5, this.ClientSize.Height - shutdownBox.Height - 20);
        shutdownBox.Click += ShutdownBox_Click;


        //表示
        this.Controls.Add(shutdownBox);
        this.Controls.Add(titleLabel);
    }

        private void ShutdownBox_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
}


public class Form2 : Form
{

    private WebBrowser webBrowser;

    public Form2()
    {
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        this.Text = "Form2";
        this.Size = new System.Drawing.Size(700,900);

        webBrowser = new WebBrowser();
        webBrowser.Dock = DockStyle.Fill;
        this.Controls.Add(webBrowser);

        webBrowser.Navigate("https://www.google.com/");

    }
}


class MyForm : Form
{

    private PictureBox pictureBox;
    private PictureBox pictureBox1;
    private Label timeLabel;
    private Label dateLabel;
    private StartMenu startMenu;
    private PictureBox chromePic;



    public MyForm()
    {

        //windowのタイトルを指定できるらしい
        this.Text = "ウィンドウ";
        //windowのでかさを最大に
        this.WindowState = FormWindowState.Maximized;
        this.FormBorderStyle = FormBorderStyle.None;


        //イメージ読み込み
        Image buttonImage = Image.FromFile("pic/a.png");
        Image buttonImage1 = Image.FromFile("pic/ver.png");
        Image childImage = Image.FromFile("pic/app.png");
        Image wallpaperImage = Image.FromFile("pic/wallpaper.jpg");
        Image chromeImage = Image.FromFile("pic/chrome.png");

        //ボタンのなんか色々
        pictureBox = new PictureBox();
        pictureBox.Image = buttonImage;
        pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
        startMenu = new StartMenu();

        pictureBox.Click += PictureBox_Click;

        this.Controls.Add(startMenu);
        //this.Click += (sender, e) => startMenu.Show();
        

        //タスクバーの方
        pictureBox1 = new PictureBox();
        pictureBox1.Image = buttonImage1;
        pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;

        //時計の色々
        timeLabel = new Label();
        timeLabel.ForeColor = Color.Black;
        timeLabel.BackColor = Color.FromArgb(0xc3, 0xc3, 0xc3);
        timeLabel.Font = new Font("Arial", 12, FontStyle.Bold);
        timeLabel.AutoSize = false;
        timeLabel.Size = new System.Drawing.Size(100,20);

        //日付とか
        dateLabel = new Label();
        dateLabel.ForeColor = Color.Black;
        dateLabel.BackColor = Color.FromArgb(0xc3, 0xc3, 0xc3);
        dateLabel.Font = new Font("Arial", 12, FontStyle.Bold);
        dateLabel.AutoSize = false;
        dateLabel.Size = new System.Drawing.Size(150, 20);

        //chromeのicon
        chromePic = new PictureBox();
        chromePic.Image = chromeImage;
        chromePic.Size = new Size(40,40);
        chromePic.SizeMode = PictureBoxSizeMode.StretchImage;
        chromePic.Click += OpenFormButton_Click;

        


        //壁紙の方
        this.BackgroundImage = wallpaperImage;
        this.BackgroundImageLayout = ImageLayout.Stretch;

        //大きさとか座標
        pictureBox.Location = new System.Drawing.Point(0, this.ClientSize.Height - pictureBox.Height);

        this.Resize += MyForm_Resize;

        ArrangeImage();

        //なんか入れる
        Controls.Add(pictureBox);
        Controls.Add(pictureBox1);
        Controls.Add(timeLabel);
        Controls.Add(chromePic);

        //時間の色々
        Timer tiemr = new Timer();
        tiemr.Interval = 1000;
        tiemr.Tick += Timer_Tick;
        tiemr.Start();

        //日付取得
        //最後のDateを付けないと時間も取得しちゃうから注意
        Timer dt = new Timer();
        dt.Interval = 1000;
        dt.Tick += Date_Tick;

        Controls.Add(dateLabel);

        dt.Start();

    }

    private void OpenFormButton_Click(object sender, EventArgs e)
    {
        Form2 f = new Form2();
        f.TopLevel = false;
        Controls.Add(f);
        f.Show();
        f.BringToFront();
    }

    private void PictureBox_Click(object sender, EventArgs e)
    {
        startMenu.Visible = !startMenu.Visible;
    }



    private void MyForm_Resize(object sender, EventArgs e)
    {
        ArrangeImage();
    }


    private void Timer_Tick(object sender, EventArgs e)
    {
        timeLabel.Text = DateTime.Now.ToString("HH:mm");
    }

    private void Date_Tick(object sender, EventArgs e)
    {
        dateLabel.Text = DateTime.Now.Date.ToString("yyyy年MM月dd日");
    }

    private void ArrangeImage()
    {
        pictureBox1.Location = new System.Drawing.Point(0, this.ClientSize.Height - pictureBox1.Height);
        pictureBox.Location = new System.Drawing.Point(0, this.ClientSize.Height - pictureBox.Height);

        //時間
        timeLabel.Location = new System.Drawing.Point(this.ClientSize.Width - timeLabel.Width, this.ClientSize.Height - timeLabel.Height - 35);
        timeLabel.BringToFront();

        //日付
        dateLabel.Location = new System.Drawing.Point(this.ClientSize.Width - dateLabel.Width , this.ClientSize.Height - dateLabel.Height - 5);
        dateLabel.BringToFront();
    }   
}

class Program
{
    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new MyForm());
    }
}