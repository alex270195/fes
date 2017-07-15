using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FES
{
    public partial class tabCrs : Form
    {
        public tabCrs()
        {
            InitializeComponent();
          //  dataGridView1.DataError += new DataGridViewDataErrorEventHandler(DataGridView1_DataError);


        }

        private void tableBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.tableBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.databaseCrsDataSet2);

        }

        private void tabCrs_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "databaseCrsDataSet2.Table". При необходимости она может быть перемещена или удалена.
            this.tableTableAdapter.Fill(this.databaseCrsDataSet2.Table);

        }

        public void DataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs anError)
        {
            MessageBox.Show("Ошибка ввода!");
            anError.ThrowException = false;
        }


        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
