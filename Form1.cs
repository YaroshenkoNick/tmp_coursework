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
                label2.Text = "������ ���������";
                label3.Text = "����� ���������";
                textBox2.ReadOnly = false;
                progressBar1.Visible = true;
            }
            else
            {
                textBox2.Text = "";
                richTextBox1.Visible = false;
                label2.Text = "����� ��� ��������";
                label3.Text = "��������� ����������";
                textBox2.ReadOnly = true;
                progressBar1.Visible = false;
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {

            int start = int.Parse(textBox1.Text);

            if (start < 3)
            {
                MessageBox.Show("��������� ��� ����� ������ 2!", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            SendDB sendDB = new SendDB();
            if (radioButton1.Checked)
            {
                try
                {
                    richTextBox1.Text = "���������:" + "\n";
                    int end = int.Parse(textBox2.Text);
                    if (start > end)
                    {
                        MessageBox.Show("������ ��������� ������ ���� ������ �����!", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    Eratosthenes solve = new Eratosthenes();
                    if (start % 2 != 0) start += 1;
                    if (end % 2 != 0) end -= 1;
                    await solve.GoldbachConjectureAsync(start, end, richTextBox1, progressBar1);
                    string output = sendDB.RTBToString(richTextBox1);
                    sendDB.SendRunning("��������", start + ":" + end, output);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("�������������� ������!", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    sendDB.SendRunning("�����", start.ToString(), result);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("�������������� ������!", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    sendDB.SendException(ex);
                }
            }
        }


        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // ���������, �������� �� ������� ������� ������ ��� �������� Backspace
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                // ���� ������� ������� �� �������� ������ � �� �������� �������� Backspace, �������� ��������
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            // ���������, �������� �� ������� ������� ������ ��� �������� Backspace
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                // ���� ������� ������� �� �������� ������ � �� �������� �������� Backspace, �������� ��������
                e.Handled = true;
            }
        }
    }
}