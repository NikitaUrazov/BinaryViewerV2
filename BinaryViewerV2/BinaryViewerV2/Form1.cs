namespace BinaryViewerV2
{
    public partial class Form1 : Form
    {
        private long bottomOffset;
        private long topOffset;
        private int nlines;
        private int initLines;
        private List<string> lines = new List<string>();
        private System.Windows.Forms.Timer scrollTimer = new System.Windows.Forms.Timer();
        private int topLoadLines;
        public Form1()
        {
            InitializeComponent();
            result_richTextBox.Font = new Font(FontFamily.GenericMonospace, result_richTextBox.Font.Size);
            scrollTimer.Interval = 10;
            scrollTimer.Tick += ScrollTimer_Tick;
            nlines = 100;
            initLines = 200;
        }

        private void ScrollTimer_Tick(object? sender, EventArgs e)
        {
            scrollTimer.Stop();
        }

        private void filePath_button_Click(object sender, EventArgs e)
        {
            string filePath = "";

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;
                    BinaryViewer.Open(filePath);
                    fileSize_label.Text = $"Размер файла: {BinaryViewer.Length.ToString()} байт";
                    InitialRead(0);
                }
            }
        }

        private void InitialRead(long offsetStart)
        {
            result_richTextBox.Clear();
            topOffset = bottomOffset = offsetStart;

            BinaryViewer.ReadForward(bottomOffset, bottomOffset + (16 * initLines) - 1);
            bottomOffset += 16 * initLines;

            lines.Clear();

            foreach (string s in result_richTextBox.Lines)
                lines.Add(s);

            foreach (string s in BinaryViewer.Result)
                lines.Add(s);

            result_richTextBox.Lines = lines.ToArray();
        }

        private void LoadBottomText()
        {
            if (bottomOffset >= BinaryViewer.Length)
            {
                bottomOffset = BinaryViewer.Length;
                return;
            }

            BinaryViewer.ReadForward(bottomOffset, bottomOffset + (16 * nlines) - 1);
            bottomOffset += 16 * nlines;

            lines.Clear();

            foreach (string s in result_richTextBox.Lines)
                lines.Add(s);

            foreach (string s in BinaryViewer.Result)
                lines.Add(s);

            result_richTextBox.Lines = lines.ToArray();
        }

        private void LoadTopText()
        {
            if (topOffset < 0)
            {
                topOffset = 0;
                return;
            }

            long endOffset = topOffset - (16 * nlines) + 1;

            BinaryViewer.ReadBackward(topOffset, endOffset);

            topOffset -= 16 * nlines;
            topLoadLines = BinaryViewer.Result.Count;

            lines.Clear();
            for (int i = BinaryViewer.Result.Count - 1; i >= 0; i--)
                lines.Add(BinaryViewer.Result[i]);

            foreach (string s in result_richTextBox.Lines)
                lines.Add(s);

            result_richTextBox.Lines = lines.ToArray();
        }

        private void DeleteTopText()
        {
            lines.Clear();

            foreach (string s in result_richTextBox.Lines)
                lines.Add(s);

            lines.RemoveRange(0, nlines);

            result_richTextBox.Lines = lines.ToArray();
            topOffset += 16 * nlines;
        }

        private void DeleteBottomText()
        {
            lines.Clear();

            foreach (string s in result_richTextBox.Lines)
                lines.Add(s);

            lines.RemoveRange(lines.Count - nlines, nlines);

            result_richTextBox.Lines = lines.ToArray();

            bottomOffset -= 16 * nlines;
        }

        private void result_richTextBox_VScroll(object sender, EventArgs e)
        {
            OnScroll();
        }

        private void OnScroll()
        {
            int firstVisibleLine = result_richTextBox.GetLineFromCharIndex(result_richTextBox.GetCharIndexFromPosition(new Point(0, 0)));
            int visibleLines = result_richTextBox.ClientSize.Height / result_richTextBox.Font.Height;
            int totalLines = result_richTextBox.Lines.Length;

            if (firstVisibleLine == 0)
            {
                if (scrollTimer.Enabled)
                    return;

                scrollTimer.Start();

                if (topOffset > 0)
                {
                    if (!(result_richTextBox.Lines.Length < initLines))
                        DeleteBottomText();
                    LoadTopText();
                    result_richTextBox.SelectionStart = result_richTextBox.GetFirstCharIndexFromLine(topLoadLines);
                    result_richTextBox.ScrollToCaret();
                }
            }
            if (firstVisibleLine + visibleLines >= totalLines)
            {
                if (scrollTimer.Enabled)
                    return;

                scrollTimer.Start();

                if (!(result_richTextBox.Lines.Length < initLines))
                    DeleteTopText();

                LoadBottomText();
            }
        }

        private void offset_button_Click(object sender, EventArgs e)
        {
            if (!BinaryViewer.IsOpen)
            {
                MessageBox.Show("Выберите файл для открытия.");
                return;
            }

            string userInput = offset_textBox.Text;

            if (Int64.TryParse(userInput, out long offset))
            {
                if (offset >= 0 && offset < BinaryViewer.Length)
                {
                    topOffset = bottomOffset = offset;
                    result_richTextBox.Clear();
                    LoadBottomText();
                    OnScroll();
                }
                else
                {
                    MessageBox.Show("Смещение за пределами размера файла.");
                }

            }
            else
            {
                MessageBox.Show("Некорректное значение.\nУкажите смещение в десятичной системе счисления.");
            }
        }
    }
}