using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using OfficeOpenXml; // EPPlus namespace
using System.Data;
using Excel = Microsoft.Office.Interop.Excel;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

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
            menuExportDatabaseToXml.Click += MenuExportDatabaseToXml_Click;
            menuImportDatabaseFromXml.Click += MenuImportDatabaseFromXml_Click;

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
                // Проверяем, есть ли фото в столбце "Фото"
                if (selectedRow.Cells["Фото"] != null && selectedRow.Cells["Фото"].Value != null)
                {
                    string filePath = selectedRow.Cells["Фото"].Value.ToString();

                    if (System.IO.File.Exists(filePath)) // Проверяем, существует ли файл
                    {
                        pictureBoxPhoto.Image = Image.FromFile(filePath); // Отображаем фото
                    }
                    else
                    {
                        pictureBoxPhoto.Image = null; // Если файла нет, очищаем PictureBox
                    }
                }
                else
                {
                    pictureBoxPhoto.Image = null; // Если столбец пуст, очищаем PictureBox
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
       

        private void ExportToExcel()
         {
            try
            {
                // Создаем приложение Excel
                Excel.Application excelApp = new Excel.Application
                {
                    Visible = true // Выставляем true, чтобы показать Excel после экспорта
                };

                // Создаем новую книгу
                Excel.Workbook workbook = excelApp.Workbooks.Add();

                // Перебираем каждую таблицу в ListBox
                for (int i = 0; i < listBoxTables.Items.Count; i++)
                {
                    string tableName = listBoxTables.Items[i].ToString();

                    // Добавляем новый лист для каждой таблицы
                    Excel.Worksheet worksheet = workbook.Sheets.Add();
                    worksheet.Name = tableName;

                    // Получаем данные из выбранной таблицы
                    DataTable dataTable = GetTableDataExport(tableName);

                    if (dataTable == null) continue;

                    // Заголовки столбцов
                    for (int col = 0; col < dataTable.Columns.Count; col++)
                    {
                        worksheet.Cells[1, col + 1] = dataTable.Columns[col].ColumnName; // Заголовок столбца
                    }

                    // Данные строк
                    for (int row = 0; row < dataTable.Rows.Count; row++)
                    {
                        for (int col = 0; col < dataTable.Columns.Count; col++)
                        {
                            worksheet.Cells[row + 2, col + 1] = dataTable.Rows[row][col]; // Ячейка данных
                        }
                    }
                }

                // Сообщение об успехе
                MessageBox.Show("Экспорт завершен успешно!", "Экспорт в Excel", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при экспорте: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
    }

    private DataTable GetTableDataExport(string tableName)
        {
            try
            {
                // Настраиваем подключение к базе данных
                string connectionString = "Server=localhost\\SQLEXPRESS;Database=Car_maintenance_costs;Trusted_Connection=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // SQL-запрос для получения всех данных таблицы
                    string query = $"SELECT * FROM {tableName}";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);

                    // Наполняем DataTable данными
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    return dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при получении данных таблицы {tableName}: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        private void ExportDatabaseToXml(string filePath)
        {
            try
            {
                DataSet dataSet = new DataSet("Database"); // Создаем DataSet для хранения всех таблиц

                // Список таблиц в базе данных
                List<string> tableNames = new List<string> { "Автомобили", "Виды_расходов", "Затраты", "Прочие_расходы", "Страхование", "Техническое_обслуживание", "Топливо" }; 
                foreach (string tableName in tableNames)
                {
                    DataTable tableData = GetTableDataExport(tableName); // Получаем данные таблицы
                    if (tableData != null)
                    {
                        tableData.TableName = tableName; // Устанавливаем имя таблицы
                        dataSet.Tables.Add(tableData.Copy()); // Копируем данные в DataSet
                    }
                }

                // Сохраняем DataSet в XML-файл
                dataSet.WriteXml(filePath, XmlWriteMode.WriteSchema);

                MessageBox.Show("База данных успешно экспортирована в XML!", "Экспорт завершен", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при экспорте базы данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ImportDatabaseFromXml(string filePath)
        {
            try
            {
                DataSet dataSet = new DataSet();
                dataSet.ReadXml(filePath); // Читаем данные из XML-файла

                // Настраиваем подключение к базе данных
                string connectionString = "Server=localhost\\SQLEXPRESS;Database=Car_maintenance_costs;Trusted_Connection=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    foreach (DataTable table in dataSet.Tables)
                    {
                        string tableName = table.TableName; // Имя таблицы
                        if (table.Rows.Count > 0)
                        {
                            // Удаляем старые данные из таблицы (опционально)
                            SqlCommand deleteCommand = new SqlCommand($"DELETE FROM {tableName}", connection);
                            deleteCommand.ExecuteNonQuery();

                            // Записываем новые данные
                            foreach (DataRow row in table.Rows)
                            {
                                // Генерация команды INSERT
                                string columnNames = string.Join(", ", table.Columns.Cast<DataColumn>().Select(c => c.ColumnName));
                                string values = string.Join(", ", table.Columns.Cast<DataColumn>().Select(c => $"@{c.ColumnName}"));
                                string query = $"INSERT INTO {tableName} ({columnNames}) VALUES ({values})";

                                SqlCommand insertCommand = new SqlCommand(query, connection);

                                foreach (DataColumn column in table.Columns)
                                {
                                    insertCommand.Parameters.AddWithValue($"@{column.ColumnName}", row[column.ColumnName] ?? DBNull.Value);
                                }

                                insertCommand.ExecuteNonQuery();
                            }
                        }
                    }
                }

                MessageBox.Show("База данных успешно импортирована из XML!", "Импорт завершен", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при импорте базы данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MenuExportDatabaseToXml_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "XML файлы (*.xml)|*.xml",
                Title = "Сохранить базу данных как XML"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                ExportDatabaseToXml(saveFileDialog.FileName);
            }
        }

        private void MenuImportDatabaseFromXml_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "XML файлы (*.xml)|*.xml",
                Title = "Открыть XML с базой данных"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                ImportDatabaseFromXml(openFileDialog.FileName);
            }
        }


        private void buttonExportToExcel_Click(object sender, EventArgs e)
        {
            ExportToExcel();
        }

        private void SaveProject(string filePath)
        {
            try
            {
                // Создаем объект ProjectData для сохранения данных
                ProjectData data = new ProjectData
                {
                    TableNames = listBoxTables.Items.Cast<string>().ToList(),
                    SelectedTable = listBoxTables.SelectedItem?.ToString(),
                    ConnectionString = "Server=localhost\\SQLEXPRESS;Database=Car_maintenance_costs;Trusted_Connection=True;"
                };

                // Сериализация данных в файл
                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(fs, data);
                }

                MessageBox.Show("Проект успешно сохранен!", "Сохранение проекта", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении проекта: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void OpenProject(string filePath)
        {
            try
            {
                // Десериализация данных из файла
                using (FileStream fs = new FileStream(filePath, FileMode.Open))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    ProjectData data = (ProjectData)formatter.Deserialize(fs);

                    // Восстановление данных в приложении
                    listBoxTables.Items.Clear();
                    foreach (var table in data.TableNames)
                    {
                        listBoxTables.Items.Add(table);
                    }

                    if (data.SelectedTable != null && listBoxTables.Items.Contains(data.SelectedTable))
                    {
                        listBoxTables.SelectedItem = data.SelectedTable;
                    }

       

                    MessageBox.Show("Проект успешно загружен!", "Открытие проекта", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при открытии проекта: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Project Files (*.proj)|*.proj";
                saveFileDialog.DefaultExt = "proj";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    SaveProject(saveFileDialog.FileName);
                }
            }
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Project Files (*.proj)|*.proj";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    OpenProject(openFileDialog.FileName);
                }
            }
        }
        private void menuAddPhoto_Click(object sender, EventArgs e)
        {
            // Открываем диалог для выбора фото
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Изображения|*.jpg;*.jpeg;*.png;*.bmp";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName; // Полный путь к выбранному файлу

                // Отображаем фото в PictureBox
                pictureBoxPhoto.Image = Image.FromFile(filePath);

                // Проверяем, есть ли столбец "Фото" в таблице
                if (!dataGridView.Columns.Contains("Фото"))
                {
                    dataGridView.Columns.Add("Фото", "Фото");
                }

                // Обновляем фото в выбранной строке
                if (dataGridView.SelectedRows.Count > 0)
                {
                    foreach (DataGridViewRow row in dataGridView.SelectedRows)
                    {
                        // Обновляем значение ячейки "Фото"
                        row.Cells["Фото"].Value = filePath;

                      
                        UpdatePhotoInDatabase(row.Index, filePath); // Передаем индекс строки и путь
                    }
                }
                else
                {
                    MessageBox.Show("Пожалуйста, выберите строку для добавления фото.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        private void UpdatePhotoInDatabase(int rowIndex, string photoPath)
        {
            try
            {
                // Получаем ID или уникальный ключ строки из базы данных (предположим, в первом столбце)
                var id = dataGridView.Rows[rowIndex].Cells[0].Value;

                // SQL-команда для обновления значения в базе данных
                string query = "UPDATE Автомобили SET Фото = @PhotoPath WHERE ID_Автомобиля = @Id";

                using (SqlConnection connection = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=Car_maintenance_costs;Trusted_Connection=True;"))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PhotoPath", photoPath);
                        command.Parameters.AddWithValue("@Id", id);

                        command.ExecuteNonQuery(); // Выполняем команду
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении базы данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
