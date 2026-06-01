using System;
using System.Drawing;
using System.Windows.Forms;

namespace architectSteps
{
    public class TreasureEditorForm : Form
    {
        private TreasureChest _chest;
        private TextBox _rewardTextBox;
        private Button _saveButton;
        private Button _cancelButton;

        public TreasureEditorForm(TreasureChest chest)
        {
            _chest = chest;
            SetupUI();
        }

        private void SetupUI()
        {
            this.Text = "Edit Treasure Chest";
            this.Size = new Size(300, 180);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            Label label = new Label
            {
                Text = "Reward Item:",
                Location = new Point(20, 20),
                AutoSize = true
            };
            this.Controls.Add(label);

            _rewardTextBox = new TextBox
            {
                Location = new Point(20, 50),
                Size = new Size(240, 25),
                Text = _chest.RewardItem
            };
            this.Controls.Add(_rewardTextBox);

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
            _chest.RewardItem = _rewardTextBox.Text;
            this.Close();
        }
    }
}
