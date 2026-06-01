using System;
using System.Drawing;
using System.Windows.Forms;

namespace architectSteps
{
    public class TrapEditorForm : Form
    {
        private Trap _trap;
        private NumericUpDown _radiusNumericUpDown;
        private Button _saveButton;
        private Button _cancelButton;

        public TrapEditorForm(Trap trap)
        {
            _trap = trap;
            SetupUI();
        }

        private void SetupUI()
        {
            this.Text = "Edit Trap";
            this.Size = new Size(300, 180);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            Label label = new Label
            {
                Text = "Activation Radius (Max 150):",
                Location = new Point(20, 20),
                AutoSize = true
            };
            this.Controls.Add(label);

            _radiusNumericUpDown = new NumericUpDown
            {
                Location = new Point(20, 50),
                Size = new Size(240, 25),
                Minimum = 10,
                Maximum = 150,
                Value = Math.Min(150, Math.Max(10, _trap.ActivationRadius))
            };
            this.Controls.Add(_radiusNumericUpDown);

            _saveButton = new Button
            {
                Text = "Save",
                Location = new Point(100, 90),
                Size = new Size(75, 30),
                DialogResult = DialogResult.OK
            };
            _saveButton.Click += SaveButton_Click;
            this.Controls.Add(_saveButton);

            _cancelButton = new Button
            {
                Text = "Cancel",
                Location = new Point(185, 90),
                Size = new Size(75, 30),
                DialogResult = DialogResult.Cancel
            };
            this.Controls.Add(_cancelButton);

            this.AcceptButton = _saveButton;
            this.CancelButton = _cancelButton;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            _trap.ActivationRadius = (int)_radiusNumericUpDown.Value;
            this.Close();
        }
    }
}
