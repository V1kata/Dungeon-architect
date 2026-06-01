using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DungeonArchitect.Commands;

namespace DungeonArchitect
{
    public partial class MainForm : Form
    {
        private DungeonScene _scene = new DungeonScene();
        private CommandManager _commandManager = new CommandManager();
        private DungeonRenderer _renderer = new DungeonRenderer();
        private Hero _hero => _scene.Elements.OfType<Hero>().FirstOrDefault();
        private bool _isPlaytestMode = false;
        private bool _isRunningPlaytest = false;
        private DungeonElement _selectedElement;
        private Point _dragStart;
        private Point _dragStartElementPos;
        private bool _isDragging = false;

        private Panel _canvas;
        private Button _toggleModeButton;
        private Button _addHeroButton;
        private Button _addTrapButton;
        private Button _addTreasureButton;
        private Button _addWallButton;
        private Button _deleteButton;
        private Button _undoButton;
        private Button _redoButton;
        private Button _showStatsButton;
        private Button _saveButton;
        private Button _loadButton;
        private Label _statusLabel;
        private Timer _gameTimer;
        private FileStorageService _fileStorage = new FileStorageService();

        public MainForm()
        {
            SetupUI();
        }

        private void SetupUI()
        {
            this.Text = "Architect Steps";
            this.Size = new Size(1200, 800);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.Black;
            this.DoubleBuffered = true;

            _canvas = new Panel
            {
                Location = new Point(10, 10),
                Size = new Size(800, 650),
                BackColor = Color.White
            };
            _canvas.Paint += Canvas_Paint;
            _canvas.MouseDown += Canvas_MouseDown;
            _canvas.MouseMove += Canvas_MouseMove;
            _canvas.MouseUp += Canvas_MouseUp;
            _canvas.MouseDoubleClick += Canvas_MouseDoubleClick;
            this.Controls.Add(_canvas);

            int panelX = 820;
            int currentY = 10;

            _statusLabel = new Label
            {
                Location = new Point(panelX, currentY),
                Size = new Size(360, 30),
                Text = "Mode: Design",
                Font = new Font("Arial", 12, FontStyle.Bold),
                ForeColor = Color.Yellow
            };
            this.Controls.Add(_statusLabel);
            currentY += 50;

            _toggleModeButton = new Button
            {
                Location = new Point(panelX, currentY),
                Size = new Size(360, 40),
                Text = "Start Playtest",
                BackColor = Color.Green,
                ForeColor = Color.White,
                Font = new Font("Arial", 10, FontStyle.Bold)
            };
            _toggleModeButton.Click += ToggleMode_Click;
            this.Controls.Add(_toggleModeButton);
            currentY += 50;

            _addHeroButton = new Button
            {
                Location = new Point(panelX, currentY),
                Size = new Size(175, 35),
                Text = "Add Hero",
                BackColor = Color.Green
            };
            _addHeroButton.Click += AddHero_Click;
            this.Controls.Add(_addHeroButton);

            _addTrapButton = new Button
            {
                Location = new Point(panelX + 185, currentY),
                Size = new Size(175, 35),
                Text = "Add Trap",
                BackColor = Color.Red
            };
            _addTrapButton.Click += AddTrap_Click;
            this.Controls.Add(_addTrapButton);
            currentY += 45;

            _addTreasureButton = new Button
            {
                Location = new Point(panelX, currentY),
                Size = new Size(175, 35),
                Text = "Add Treasure",
                BackColor = Color.Gold
            };
            _addTreasureButton.Click += AddTreasure_Click;
            this.Controls.Add(_addTreasureButton);

            _addWallButton = new Button
            {
                Location = new Point(panelX + 185, currentY),
                Size = new Size(175, 35),
                Text = "Add Wall",
                BackColor = Color.Gray
            };
            _addWallButton.Click += AddWall_Click;
            this.Controls.Add(_addWallButton);
            currentY += 45;

            _deleteButton = new Button
            {
                Location = new Point(panelX, currentY),
                Size = new Size(175, 35),
                Text = "Delete Selected",
                BackColor = Color.DarkRed
            };
            _deleteButton.Click += Delete_Click;
            this.Controls.Add(_deleteButton);
            currentY += 45;

            _undoButton = new Button
            {
                Location = new Point(panelX, currentY),
                Size = new Size(175, 35),
                Text = "Undo",
                BackColor = Color.DarkOrange
            };
            _undoButton.Click += Undo_Click;
            this.Controls.Add(_undoButton);

            _redoButton = new Button
            {
                Location = new Point(panelX + 185, currentY),
                Size = new Size(175, 35),
                Text = "Redo",
                BackColor = Color.DarkOrange
            };
            _redoButton.Click += Redo_Click;
            this.Controls.Add(_redoButton);
            currentY += 45;

            _showStatsButton = new Button
            {
                Location = new Point(panelX, currentY),
                Size = new Size(360, 35),
                Text = "Show Stats",
                BackColor = Color.Teal,
                ForeColor = Color.White
            };
            _showStatsButton.Click += ShowStats_Click;
            this.Controls.Add(_showStatsButton);
            currentY += 45;

            _saveButton = new Button
            {
                Location = new Point(panelX, currentY),
                Size = new Size(175, 35),
                Text = "Save",
                BackColor = Color.SteelBlue,
                ForeColor = Color.White
            };
            _saveButton.Click += Save_Click;
            this.Controls.Add(_saveButton);

            _loadButton = new Button
            {
                Location = new Point(panelX + 185, currentY),
                Size = new Size(175, 35),
                Text = "Load",
                BackColor = Color.SteelBlue,
                ForeColor = Color.White
            };
            _loadButton.Click += Load_Click;
            this.Controls.Add(_loadButton);

            _gameTimer = new Timer();
            _gameTimer.Interval = 50;
            _gameTimer.Tick += GameTimer_Tick;

            this.KeyPreview = true;
        }

        private void ToggleMode_Click(object sender, EventArgs e)
        {
            _isPlaytestMode = !_isPlaytestMode;

            if (_isPlaytestMode)
            {
                _toggleModeButton.Text = "Stop Playtest";
                _toggleModeButton.BackColor = Color.Red;
                _statusLabel.Text = "Mode: Playtest (Running)";
                _isRunningPlaytest = true;
                _gameTimer.Start();
                DisableDesignModeButtons();
                this.Focus();
            }
            else
            {
                _toggleModeButton.Text = "Start Playtest";
                _toggleModeButton.BackColor = Color.Green;
                _statusLabel.Text = "Mode: Design";
                _isRunningPlaytest = false;
                _gameTimer.Stop();
                EnableDesignModeButtons();
            }
            _canvas.Invalidate();
        }

        private void AddHero_Click(object sender, EventArgs e)
        {
            if (_isPlaytestMode) return;

            var hero = new Hero(100 + _scene.Elements.Count * 20, 100 + _scene.Elements.Count * 20);
            _commandManager.ExecuteCommand(new AddElementCommand(_scene, hero));
            _canvas.Invalidate();
        }

        private void AddTrap_Click(object sender, EventArgs e)
        {
            if (_isPlaytestMode) return;

            var trap = new Trap(200 + _scene.Elements.Count * 15, 200 + _scene.Elements.Count * 15);
            _commandManager.ExecuteCommand(new AddElementCommand(_scene, trap));
            _canvas.Invalidate();
        }

        private void AddTreasure_Click(object sender, EventArgs e)
        {
            if (_isPlaytestMode) return;

            var chest = new TreasureChest(300 + _scene.Elements.Count * 15, 300 + _scene.Elements.Count * 15);
            _commandManager.ExecuteCommand(new AddElementCommand(_scene, chest));
            _canvas.Invalidate();
        }

        private void AddWall_Click(object sender, EventArgs e)
        {
            if (_isPlaytestMode) return;

            var wall = new Wall(400 + _scene.Elements.Count * 15, 400 + _scene.Elements.Count * 15, 60, 60);
            _commandManager.ExecuteCommand(new AddElementCommand(_scene, wall));
            _canvas.Invalidate();
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if (_isPlaytestMode || _selectedElement == null) return;

            _commandManager.ExecuteCommand(new RemoveElementCommand(_scene, _selectedElement));
            _selectedElement = null;
            _canvas.Invalidate();
        }

        private void Undo_Click(object sender, EventArgs e)
        {
            if (_isPlaytestMode) return;

            _commandManager.Undo();
            if (_selectedElement != null && !_scene.Elements.Contains(_selectedElement))
            {
                _selectedElement = null;
            }
            _canvas.Invalidate();
        }

        private void Redo_Click(object sender, EventArgs e)
        {
            if (_isPlaytestMode) return;

            _commandManager.Redo();
            _canvas.Invalidate();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (_isPlaytestMode && _hero != null && !_hero.IsBroken)
            {
                int dx = 0, dy = 0;
                switch (keyData)
                {
                    case Keys.Up: dy = -15; break;
                    case Keys.Down: dy = 15; break;
                    case Keys.Left: dx = -15; break;
                    case Keys.Right: dx = 15; break;
                }

                if (dx != 0 || dy != 0)
                {
                    if (_scene.TryMoveHero(_hero, dx, dy))
                    {
                        _scene.UpdateProximityEngine(_hero);
                        _canvas.Invalidate();
                        return true;
                    }
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (_isPlaytestMode) return;

            _selectedElement = _scene.GetElementAtPosition(e.X, e.Y);
            if (_selectedElement != null)
            {
                _isDragging = true;
                _dragStart = e.Location;
                _dragStartElementPos = new Point(_selectedElement.X, _selectedElement.Y);
            }
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_isDragging || _selectedElement == null) return;

            int dx = e.X - _dragStart.X;
            int dy = e.Y - _dragStart.Y;
            _selectedElement.Move(dx, dy);
            _dragStart = e.Location;
            _canvas.Invalidate();
        }

        private void Canvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (_isDragging && _selectedElement != null)
            {
                Point dragEndElementPos = new Point(_selectedElement.X, _selectedElement.Y);
                if (_dragStartElementPos != dragEndElementPos)
                {
                    _selectedElement.X = _dragStartElementPos.X;
                    _selectedElement.Y = _dragStartElementPos.Y;
                    _commandManager.ExecuteCommand(new MoveElementCommand(_selectedElement, _dragStartElementPos, dragEndElementPos));
                    _canvas.Invalidate();
                }
            }
            _isDragging = false;
        }

        private void Canvas_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (_isPlaytestMode) return;

            var element = _scene.GetElementAtPosition(e.X, e.Y);
            if (element is TreasureChest chest)
            {
                using var form = new TreasureEditorForm(chest);
                form.ShowDialog();
                _canvas.Invalidate();
            }
            else if (element is Trap trap)
            {
                using var form = new TrapEditorForm(trap);
                form.ShowDialog();
                _canvas.Invalidate();
            }
            else if (element is Wall wall)
            {
                using var form = new WallEditorForm(wall);
                form.ShowDialog();
                _canvas.Invalidate();
            }
        }

        private void ShowStats_Click(object sender, EventArgs e)
        {
            var statsForm = new StatsForm(_scene);
            statsForm.Show();
        }

        private void Save_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "JSON files (*.json)|*.json" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    _fileStorage.SaveToFile(sfd.FileName, _scene);
                    MessageBox.Show("Dungeon saved successfully!");
                }
            }
        }

        private void Load_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "JSON files (*.json)|*.json" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    _fileStorage.LoadFromFile(ofd.FileName, _scene);
                    _canvas.Invalidate();
                    MessageBox.Show("Dungeon loaded successfully!");
                }
            }
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            if (!_isRunningPlaytest || _hero == null || _hero.IsBroken)
                return;
        }

        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);
            _renderer.DrawScene(e.Graphics, _scene);

            if (_selectedElement != null && !_isPlaytestMode)
            {
                var bounds = _selectedElement.GetBounds();
                Pen selectPen = new Pen(Color.Blue, 3);
                e.Graphics.DrawRectangle(selectPen, bounds);
                selectPen.Dispose();
            }
        }

        private void DisableDesignModeButtons()
        {
            _addHeroButton.Enabled = false;
            _addTrapButton.Enabled = false;
            _addTreasureButton.Enabled = false;
            _addWallButton.Enabled = false;
            _deleteButton.Enabled = false;
            _undoButton.Enabled = false;
            _redoButton.Enabled = false;
        }

        private void EnableDesignModeButtons()
        {
            _addHeroButton.Enabled = true;
            _addTrapButton.Enabled = true;
            _addTreasureButton.Enabled = true;
            _addWallButton.Enabled = true;
            _deleteButton.Enabled = true;
            _undoButton.Enabled = true;
            _redoButton.Enabled = true;
        }
    }
}
