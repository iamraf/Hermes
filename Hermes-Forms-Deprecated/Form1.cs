using Hermes.View;
using System.Windows.Forms;

namespace Hermes
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            //DemoForm form = new DemoForm();
            Loginpage form = new Loginpage();
            //form.TopMost = true;
            form.Show();
        }
    }
}
