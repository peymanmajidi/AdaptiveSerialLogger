using AdaptiveSerialLogger.Win.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdaptiveSerialLogger.Win
{
    public partial class TextViewFRM : Form
    {

        public Port Port;
        public TextViewFRM()
        {
            InitializeComponent();
            
        }

        private void SenderFRM_Load(object sender, EventArgs e)
        {

            txtMessageTosend.Text = TextFile.DataToSave;
        }

        private void txtMessageTosend_TextChanged(object sender, EventArgs e)
        {
             TextFile.DataToSave = txtMessageTosend.Text;
           
        }
    }
}
