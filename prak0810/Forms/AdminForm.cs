using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prak0810.Forms
{
    public partial class AdminForm : Form
    {
        public AdminForm()
        {
            InitializeComponent();
        }

        private void accountsBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.accountsBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.module4DBDataSet);

        }

        private void AdminForm_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "module4DBDataSet.Accounts". При необходимости она может быть перемещена или удалена.
            this.accountsTableAdapter.Fill(this.module4DBDataSet.Accounts);

        }

        private void accountsBindingNavigator_RefreshItems(object sender, EventArgs e)
        {
            this.Validate();
            this.accountsBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.module4DBDataSet);
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            MainForm from = new MainForm();
            from.Show();
            Close();
        }

        private void accountsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
