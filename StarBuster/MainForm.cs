using System.ComponentModel;
using System.Reflection;
using StarBuster.GameComponents;
using StarBuster.Objects2D;

namespace StarBuster
{
    public partial class MainForm : Form
    {
        Hero hero;

        public MainForm()
        {
            InitializeComponent();

            GameManager gm = GameManager.Instance;

            ClientSize = new Size(1200, 800);
            gm.SetResolution(1200, 800);
        }

        private void MainTimer_Tick(object sender, EventArgs e)
        {
            GameManager.Instance.Update();
            Text = GameManager.Instance.ObjectCount.ToString();

            Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            GameManager.Instance.Render(e.Graphics);
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            GameManager.Instance.KeySet.Add(e.KeyCode);
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            GameManager.Instance.KeySet.Remove(e.KeyCode);
        }
    }
}