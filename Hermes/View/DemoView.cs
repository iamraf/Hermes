using Hermes.Presenter;
using System;
using System.Windows.Forms;

namespace Hermes.View
{
    public partial class DemoForm : Form, IDemoView
    {
        private readonly DemoPresenter Presenter;

        public DemoForm()
        {
            InitializeComponent();

            Presenter = new DemoPresenter(this);
        }

        public string LabelText
        {
            get
            { 
                return label1.Text; 
            }
            set
            { 
                label1.Text = value;
            }
        }

        public string ErrorDialog 
        { 
            set
            {
                MessageBox.Show(value, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Presenter.LoadName();
        }
    }
}
