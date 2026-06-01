using System;
using System.Drawing;
using System.Windows.Forms;

namespace architectSteps
{
    public class WallEditorForm : Form
    {
        private Wall _wall;
        private NumericUpDown _widthNumericUpDown;
        private NumericUpDown _heightNumericUpDown;
        private Button _saveButton;
        private Button _cancelButton;

        public WallEditorForm(Wall wall)
        {
            _wall = wall;
            SetupUI();
        }

        private void SetupUI()
        {
            this.Text = "Edit Wall Dimensions";
            this.Size = new Size(300, 220);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            Label widthLabel = new Label
            {
                Text = "Width:",
                Location = new Point(20, 20),
                AutoSize = true
            };
            this.Controls.Add(widthLabel);

            _widthNumericUpDown = new NumericUpDown
            {
                Location = new Point(120, 18),
                Size = new Size(140, 25),
                Minimum = 10,
                Maximum = 500,
                Value = Math.Min(500, Math.Max(10, _wall.Width))
            };
            this.Controls.Add(_widthNumericUpDown);

            Label heightLabel = new Label
            {
                Text = "Height:",
                Location = new Point(20, 60),
                AutoSize = true
            };
            this.Controls.Add(heightLabel);

            _heightNumericUpDown = new NumericUpDown
            {
                Location = new Point(120, 58),
                Size = new Size(140, 25),
                Minimum = 10,
                Maximum = 500,
                Value = Math.Min(500, Math.Max(10, _wall.Height))
            };
            this.Controls.Add(_heightNumericUpDown);

            _saveButton = new Button
            {
                Text = "Save",
                Location = new Point(100, 110),
                Size = new Size(75, 30),
                DialogResult = DialogResult.OK
            };
            _saveButton.Click += SaveButton_Click;
            this.Controls.Add(_saveButton);

            _cancelButton = new Button
            {
                Text = "Cancel",
                Location = new Point(185, 110),
                Size = new Size(75, 30),
                DialogResult = DialogResult.Cancel
            };
            this.Controls.Add(_cancelButton);

            this.AcceptButton = _saveButton;
            this.CancelButton = _cancelButton;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            _wall.Width = (int)_widthNumericUpDown.Value;
            _wall.Height = (int)_heightNumericUpDown.Value;
            this.Close();
        }
    }
}
