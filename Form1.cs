using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Labss
{
    public partial class Form1 : Form
    {
        private SqlConnection sqlConnection;

        public Form1()
        {
            InitializeComponent();
            InitializeDatabase();
            InitializeUI();

        }

        private void InitializeDatabase()
        {
            string connectionString = "Server=localhost\\SQLEXPRESS;Database=Car_maintenance_costs;Trusted_Connection=True;";
            sqlConnection = new SqlConnection(connectionString);
            try
            {
                sqlConnection.Open();
                // Загрузка списка таблиц в ListBox
                LoadTablesToListBox();

                // Загрузка списка таблиц в MenuStrip
                LoadTablesToMenu();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось подключиться к базе данных: " + ex.Message);
            }
        }
      


        private void LoadTablesToListBox()
        {
            try
            {
                var tables = GetTables();
                listBoxTables.Items.AddRange(tables.ToArray());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке списка таблиц: {ex.Message}");
            }
        }
        private void LoadCosts(int carId)
        {
            string query = @"
        SELECT Затраты.Дата, Затраты.Сумма, Затраты.Описание, Виды_расходов.Наименование 
        FROM Затраты
        INNER JOIN Виды_расходов ON Затраты.ID_Вида_Расхода = Виды_расходов.ID_Вида_Расхода
        WHERE Затраты.ID_Автомобиля = @CarId";

            SqlCommand command = new SqlCommand(query, sqlConnection);
            command.Parameters.AddWithValue("@CarId", carId);

            listBoxCosts.Items.Clear();

            try
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string item = $"Дата: {reader["Дата"]}\nСумма: {reader["Сумма"]}\nРасход: {reader["Наименование"]}\nОписание: {reader["Описание"]}";
                        listBoxCosts.Items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки затрат: " + ex.Message);
            }
        }
        private void LoadMiscellaneous(int carId)
        {
            string query = @"
        SELECT Дата_Расхода, Сумма, Описание 
        FROM Прочие_расходы
        WHERE ID_Автомобиля = @CarId";

            SqlCommand command = new SqlCommand(query, sqlConnection);
            command.Parameters.AddWithValue("@CarId", carId);

            listBoxMiscellaneous.Items.Clear();

            try
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string item = $"Дата: {reader["Дата_Расхода"]}\nСумма: {reader["Сумма"]}\nОписание: {reader["Описание"]}";
                        listBoxMiscellaneous.Items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки прочих расходов: " + ex.Message);
            }
        }
        private void LoadInsurance(int carId)
        {
            string query = @"
        SELECT Компания, Номер_Полиса, Дата_Начала, Дата_Окончания, Сумма_Страхования 
        FROM Страхование
        WHERE ID_Автомобиля = @CarId";

            SqlCommand command = new SqlCommand(query, sqlConnection);
            command.Parameters.AddWithValue("@CarId", carId);

            listBoxInsurance.Items.Clear();

            try
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string item = $"Компания: {reader["Компания"]}\nПолис: {reader["Номер_Полиса"]}\nНачало: {reader["Дата_Начала"]}\nОкончание: {reader["Дата_Окончания"]}\nСумма: {reader["Сумма_Страхования"]}";
                        listBoxInsurance.Items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки страхования: " + ex.Message);
            }
        }
        private void LoadMaintenance(int carId)
        {
            string query = @"
        SELECT Дата_ТО, Описание_Работ, Стоимость, Пробег_при_ТО 
        FROM Техническое_обслуживание
        WHERE ID_Автомобиля = @CarId";

            SqlCommand command = new SqlCommand(query, sqlConnection);
            command.Parameters.AddWithValue("@CarId", carId);

            listBoxMaintenance.Items.Clear();

            try
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string item = $"Дата ТО: {reader["Дата_ТО"]}\nРаботы: {reader["Описание_Работ"]}\nСтоимость: {reader["Стоимость"]}\nПробег: {reader["Пробег_при_ТО"]}";
                        listBoxMaintenance.Items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки технического обслуживания: " + ex.Message);
            }
        }
        private void LoadFueling(int carId)
        {
            string query = @"
        SELECT Дата_ТО, Количество_литров, Цена_за_литр, Сумма, Тип_топлива, Октановое_число 
        FROM Топливо
        WHERE ID_Автомобиля = @CarId";

            SqlCommand command = new SqlCommand(query, sqlConnection);
            command.Parameters.AddWithValue("@CarId", carId);

            listBoxFueling.Items.Clear();

            try
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string item = $"Дата: {reader["Дата_ТО"]}\nЛитры: {reader["Количество_литров"]}\nЦена/л: {reader["Цена_за_литр"]}\nСумма: {reader["Сумма"]}\nТип: {reader["Тип_топлива"]}\nОЧ: {reader["Октановое_число"]}";
                        listBoxFueling.Items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки данных заправок: " + ex.Message);
            }
        }



        private void InitializeUI()
        {
            LoadCarBrands();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Загрузка данных в таблицу автомобилей при запуске формы
            LoadData("Автомобили");
        }

        private void LoadCarBrands()
        {
            string query = "SELECT DISTINCT Марка FROM Автомобили";
            SqlCommand command = new SqlCommand(query, sqlConnection);
            SqlDataReader reader = null;

            try
            {
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    comboBoxBrands.Items.Add(reader["Марка"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки марок: " + ex.Message);
            }
            finally
            {
                reader?.Close();
            }
        }
        private void DataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                // Получаем ID_Автомобиля из выбранной строки
                var selectedRow = dataGridView.SelectedRows[0];
                if (selectedRow.Cells["ID_Автомобиля"] != null && selectedRow.Cells["ID_Автомобиля"].Value != DBNull.Value)
                {
                    int carId = Convert.ToInt32(selectedRow.Cells["ID_Автомобиля"].Value);

                    // Загружаем данные для выбранного автомобиля
                    LoadCosts(carId);
                    LoadMiscellaneous(carId);
                    LoadInsurance(carId);
                    LoadMaintenance(carId);
                    LoadFueling(carId);
                }
            }
        }

        private void ComboBoxBrands_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxBrands.SelectedItem != null)
            {
                string selectedBrand = comboBoxBrands.SelectedItem.ToString();
                FilterCarsByBrand(selectedBrand);
            }
        }

        private void FilterCarsByBrand(string brand)
        {
            string query = "SELECT * FROM Автомобили WHERE Марка = @Brand";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(query, sqlConnection);
            dataAdapter.SelectCommand.Parameters.AddWithValue("@Brand", brand);

            DataTable dataTable = new DataTable();
            try
            {
                dataAdapter.Fill(dataTable);
                dataGridView.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка фильтрации автомобилей: " + ex.Message);
            }
        }

        private void LoadData(string tableName)
        {
            string query = $"SELECT * FROM {tableName}";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(query, sqlConnection);
            DataTable dataTable = new DataTable();

            try
            {
                dataAdapter.Fill(dataTable);
                dataGridView.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки данных: " + ex.Message);
            }
        }



        private void GenericListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();

            ListBox listBox = sender as ListBox;

            if (e.Index >= 0 && listBox != null && e.Index < listBox.Items.Count)
            {
                string text = listBox.Items[e.Index].ToString();
                e.Graphics.DrawString(
                    text,
                    e.Font,
                    new SolidBrush(e.ForeColor),
                    e.Bounds
                );
            }

            e.DrawFocusRectangle();
        }

        private void GenericListBox_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            ListBox listBox = sender as ListBox;

            if (e.Index >= 0 && listBox != null && e.Index < listBox.Items.Count)
            {
                string text = listBox.Items[e.Index].ToString();
                var lines = text.Split(new[] { '\n' }, StringSplitOptions.None);
                e.ItemHeight = (int)(lines.Length * e.Graphics.MeasureString(text, listBox.Font).Height);
            }
        }
        private void LoadTablesToMenu()
        {
            try
            {
                var tables = GetTables();
                foreach (var table in tables)
                {
                    var tableItem = new ToolStripMenuItem(table);

                    var addItem = new ToolStripMenuItem("Добавить");
                    addItem.Click += (s, e) => OpenDynamicForm(table, DynamicInputForm.ActionType.Add);

                    var updateItem = new ToolStripMenuItem("Изменить");
                    updateItem.Click += (s, e) => OpenDynamicForm(table, DynamicInputForm.ActionType.Update);

                    var deleteItem = new ToolStripMenuItem("Удалить");
                    deleteItem.Click += (s, e) => OpenDynamicForm(table, DynamicInputForm.ActionType.Delete);

                    tableItem.DropDownItems.Add(addItem);
                    tableItem.DropDownItems.Add(updateItem);
                    tableItem.DropDownItems.Add(deleteItem);

                    menuTables.DropDownItems.Add(tableItem);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке таблиц: {ex.Message}");
            }
        }

        private List<string> GetTables()
        {
            var tables = new List<string>();

            try
            {
                string query = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'";
                using (var command = new SqlCommand(query, sqlConnection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string tableName = reader["TABLE_NAME"].ToString();
                        if (tableName != "sysdiagrams") // Исключаем sysdiagrams
                        {
                            tables.Add(tableName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при получении списка таблиц: {ex.Message}");
            }

            return tables;
        }

        private void listBoxTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxTables.SelectedItem is string selectedTable)
            {
                LoadTableData(selectedTable);
            }
        }

        private void LoadTableData(string tableName)
        {
            try
            {
                var dataTable = GetTableData(tableName);
                dataGridView.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных таблицы: {ex.Message}");
            }
        }

        private DataTable GetTableData(string tableName)
        {
            var dataTable = new DataTable();
            try
            {
                string query = $"SELECT * FROM {tableName}";
                using (var adapter = new SqlDataAdapter(query, sqlConnection))
                {
                    adapter.Fill(dataTable);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при получении данных таблицы {tableName}: {ex.Message}");
            }

            return dataTable;
        }

        private void OpenDynamicForm(string tableName, DynamicInputForm.ActionType actionType)
        {
            DataRow dataRow = null;

            // Если действие "Изменить" или "Удалить", выбираем последнюю строку из таблицы
            if (actionType == DynamicInputForm.ActionType.Update || actionType == DynamicInputForm.ActionType.Delete)
            {
                var dataTable = GetTableData(tableName);
                if (dataTable.Rows.Count > 0)
                {
                    dataRow = dataTable.Rows[dataTable.Rows.Count - 1];
                }
                else
                {
                    MessageBox.Show("В таблице нет данных для выполнения операции.");
                    return;
                }
            }

            var form = new DynamicInputForm(tableName, sqlConnection, actionType, dataRow);
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadTableData(tableName);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (listBoxTables.SelectedItem is string selectedTable)
            {
                OpenDynamicForm(selectedTable, DynamicInputForm.ActionType.Add);
            }
            else
            {
                MessageBox.Show("Выберите таблицу для добавления записи.");
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (listBoxTables.SelectedItem is string selectedTable)
            {
                OpenDynamicForm(selectedTable, DynamicInputForm.ActionType.Update);
            }
            else
            {
                MessageBox.Show("Выберите таблицу для изменения записи.");
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (listBoxTables.SelectedItem is string selectedTable)
            {
                OpenDynamicForm(selectedTable, DynamicInputForm.ActionType.Delete);
            }
            else
            {
                MessageBox.Show("Выберите таблицу для удаления записи.");
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Закрываем соединение с базой данных при выходе
            if (sqlConnection != null)
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
        }



    }
}
