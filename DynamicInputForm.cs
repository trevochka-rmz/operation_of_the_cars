using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Labss
{
    public partial class DynamicInputForm : Form
    {
        public enum ActionType
        {
            Add,
            Delete,
            Update
        }

        private readonly string tableName;
        private readonly SqlConnection sqlConnection;
        private readonly ActionType actionType;
        private readonly DataRow dataRow;

        public DynamicInputForm(string tableName, SqlConnection sqlConnection, ActionType actionType, DataRow dataRow = null)
        {
            InitializeComponent();
            this.tableName = tableName;
            this.sqlConnection = sqlConnection;
            this.actionType = actionType;
            this.dataRow = dataRow;

            InitializeForm();
        }

        private void InitializeForm()
        {
            Text = "";
            switch (actionType)
            {
                case ActionType.Add:
                    Text = "Добавить запись";
                    break;
                case ActionType.Delete:
                    Text = "Удалить запись";
                    break;
                case ActionType.Update:
                    Text = "Изменить запись";
                    break;
                default:
                    Text = "Операция";
                    break;
            }

            var columnInfo = GetTableSchema();

            if (actionType == ActionType.Delete)
            {
                CreateDeleteForm(columnInfo);
            }
            else
            {
                CreateInputForm(columnInfo);
            }
        }

        private List<string> GetTableSchema()
        {
            var columnNames = new List<string>();
            try
            {
                string query = $"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{tableName}'";
                using (var command = new SqlCommand(query, sqlConnection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            columnNames.Add(reader["COLUMN_NAME"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка получения схемы таблицы: {ex.Message}");
            }
            return columnNames;
        }

        private void CreateInputForm(List<string> columnInfo)
        {
            int top = 20;
            int labelWidth = 100;
            int textBoxWidth = 200;
            int padding = 20; // Отступ справа
            int formWidth = this.ClientSize.Width;

            foreach (var columnName in columnInfo)
            {
                // Создаем метку
                var label = new Label
                {
                    Text = columnName,

                    Left = padding,
                    Top = top,
                    Width = labelWidth
                };
                Controls.Add(label);

                // Создаем текстовое поле
                var textBox = new TextBox
                {
                    Name = columnName,
                    Left = padding + labelWidth + 10, // Расположить справа от метки
                    Top = top,
                    Width = formWidth - (padding * 2 + labelWidth + 10) // Выравниваем с учетом отступа
                };

                // Если это операция обновления, устанавливаем текущее значение
                if (actionType == ActionType.Update && dataRow != null)
                {
                    textBox.Text = dataRow[columnName]?.ToString();
                }
                Controls.Add(textBox);

                top += 30; // Расстояние между строками
            }

            // Кнопка действия
            var button = new Button
            {
                Text = actionType == ActionType.Add ? "Добавить" : "Изменить",
                Left = formWidth / 2 - 50, // Центрируем кнопку
                Top = top + 10,
                Width = 100
            };
            button.Click += (sender, e) => HandleSave(columnInfo);
            Controls.Add(button);
        }


        private void CreateDeleteForm(List<string> columnInfo)
        {
            var label = new Label
            {
                Text = $"Введите значение для столбца {columnInfo[0]} (первичный ключ):",
                Left = 20,
                Top = 20,
                Width = 300
            };
            Controls.Add(label);

            var textBox = new TextBox
            {
                Name = "PrimaryKey",
                Left = 20,
                Top = 50,
                Width = 200
            };
            Controls.Add(textBox);

            var button = new Button
            {
                Text = "Удалить",
                Left = 20,
                Top = 90,
                Width = 100
            };
            button.Click += (sender, e) => HandleDelete(columnInfo[0], textBox.Text);
            Controls.Add(button);
        }

        private void HandleSave(List<string> columnInfo)
        {
            try
            {
                string query;
                if (actionType == ActionType.Add)
                {
                    string columns = string.Join(", ", columnInfo);
                    string values = string.Join(", ", columnInfo.Select(c => $"@{c}"));
                    query = $"INSERT INTO {tableName} ({columns}) VALUES ({values})";
                }
                else if (actionType == ActionType.Update)
                {
                    string updates = string.Join(", ", columnInfo.Select(c => $"{c} = @{c}"));
                    string primaryKey = columnInfo[0];
                    query = $"UPDATE {tableName} SET {updates} WHERE {primaryKey} = @{primaryKey}";
                }
                else
                {
                    throw new InvalidOperationException("Некорректный тип операции.");
                }

                using (var command = new SqlCommand(query, sqlConnection))
                {
                    foreach (var columnName in columnInfo)
                    {
                        string value = Controls.OfType<TextBox>().FirstOrDefault(t => t.Name == columnName)?.Text;
                        if (string.IsNullOrEmpty(value))
                        {
                            command.Parameters.AddWithValue($"@{columnName}", DBNull.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue($"@{columnName}", value);
                        }
                    }

                    command.ExecuteNonQuery();
                }

                MessageBox.Show(actionType == ActionType.Add ? "Запись успешно добавлена." : "Запись успешно обновлена.");
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка выполнения операции: {ex.Message}");
            }
        }

        private void HandleDelete(string primaryKey, string value)
        {
            try
            {
                string query = $"DELETE FROM {tableName} WHERE {primaryKey} = @PrimaryKey";
                using (var command = new SqlCommand(query, sqlConnection))
                {
                    command.Parameters.AddWithValue("@PrimaryKey", value);
                    command.ExecuteNonQuery();
                }

                MessageBox.Show("Запись успешно удалена.");
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка удаления записи: {ex.Message}");
            }
        }


        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // DynamicInputForm
            // 
            this.ClientSize = new System.Drawing.Size(282, 253);
            this.Name = "DynamicInputForm";
            this.Load += new System.EventHandler(this.DynamicInputForm_Load);
            this.ResumeLayout(false);

        }

        private void DynamicInputForm_Load(object sender, EventArgs e)
        {
        }
    }
    }
