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
    public partial class TableCrossSection : Form
    {
        public TableCrossSection()
        {
            InitializeComponent();
        }

        private void buttonTableBack_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tableBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
        
        }

        private void TableCrossSection_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "databaseCrsDataSet2.Table". При необходимости она может быть перемещена или удалена.
            this.tableTableAdapter.Fill(this.databaseCrsDataSet2.Table);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "databaseCrsDataSet1.Table2". При необходимости она может быть перемещена или удалена.
            this.table2TableAdapter.Fill(this.databaseCrsDataSet1.Table2);
        }

        private void table2BindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.table2BindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.databaseCrsDataSet1);

        }

        private void tableDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
