namespace goldbach
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }


        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                textBox2.Text = "";
                richTextBox1.Visible = true;
                label2.Text = "Начало интервала";
                label3.Text = "Конец интервала";
                textBox2.ReadOnly = false;
                progressBar1.Visible = true;
            }
            else
            {
                textBox2.Text = "";
                richTextBox1.Visible = false;
                label2.Text = "Число для проверки";
                label3.Text = "Результат разложения";
                textBox2.ReadOnly = true;
                progressBar1.Visible = false;
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {

            int start = int.Parse(textBox1.Text);

            if (start < 3)
            {
                MessageBox.Show("Выполнимо для чисел больше 2!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            SendDB sendDB = new SendDB();
            if (radioButton1.Checked)
            {
                try
                {
                    richTextBox1.Text = "Результат:" + "\n";
                    int end = int.Parse(textBox2.Text);
                    if (start > end)
                    {
                        MessageBox.Show("Начало интервала должно быть больше конца!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    Eratosthenes solve = new Eratosthenes();
                    if (start % 2 != 0) start += 1;
                    if (end % 2 != 0) end -= 1;
                    await solve.GoldbachConjectureAsync(start, end, richTextBox1, progressBar1);
                    string output = sendDB.RTBToString(richTextBox1);
                    sendDB.SendRunning("Интервал", start + ":" + end, output);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Непредвиденная ошибка!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    sendDB.SendException(ex);
                    richTextBox1.Text += ex.InnerException;
                }
            }
            else
            {
                try
                {
                    string result;

                    Enumeration enumeration = new Enumeration();
                    result = enumeration.GoldbachConjecture(start);
                    textBox2.Text = result;
                    sendDB.SendRunning("Число", start.ToString(), result);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Непредвиденная ошибка!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    sendDB.SendException(ex);
                }
            }
        }


        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Проверяем, является ли нажатая клавиша цифрой или клавишей Backspace
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                // Если нажатая клавиша не является цифрой и не является клавишей Backspace, отменяем действие
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Проверяем, является ли нажатая клавиша цифрой или клавишей Backspace
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                // Если нажатая клавиша не является цифрой и не является клавишей Backspace, отменяем действие
                e.Handled = true;
            }
        }
    }
}