using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using NNDSA_SemB;

namespace NNDSA_SemB.GUI
{
    public partial class Form1 : Form
    {
        private ParkingHouse? currentParkingHouse;
        private string? currentFilePath;

        private ComboBox comboBoxSelectedFloor = null!;
        private NumericUpDown numericUpDownSelectedSpot = null!;
        private TextBox textBoxLicensePlate = null!;
        private Label labelFloorSummary = null!;
        private Label labelSelectionSummary = null!;
        private FlowLayoutPanel flowLayoutPanelParkingSpots = null!;

        public Form1()
        {
            InitializeComponent();
            InitializeCustomParkingView();
            WireUpEventHandlers();
            InitializeGuiState();
        }

        private void InitializeGuiState()
        {
            //richTextBoxLog.Clear();
            //AppendLog("Aplikace spuštìna.");
            //AppendLog("Pro zaèátek mùžeš naèíst soubor nebo vytvoøit ukázková data.");
            RefreshParkingView();
        }

        private void InitializeCustomParkingView()
        {
            panelParkingView.Controls.Clear();

            TableLayoutPanel parkingLayoutPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3,
                BackColor = Color.White
            };

            parkingLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            parkingLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            parkingLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            TableLayoutPanel inputLayoutPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                ColumnCount = 7,
                AutoSize = true,
                Padding = new Padding(0, 0, 0, 8)
            };

            inputLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            inputLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90F));
            inputLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            inputLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90F));
            inputLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            inputLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
            inputLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));

            Label labelFloor = new Label
            {
                Text = "Podlaží:",
                AutoSize = true,
                Anchor = AnchorStyles.Left,
                Margin = new Padding(0, 8, 8, 0)
            };

            comboBoxSelectedFloor = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Width = 80,
                Anchor = AnchorStyles.Left,
                Margin = new Padding(0, 4, 16, 0)
            };

            Label labelSpot = new Label
            {
                Text = "Místo:",
                AutoSize = true,
                Anchor = AnchorStyles.Left,
                Margin = new Padding(0, 8, 8, 0)
            };

            numericUpDownSelectedSpot = new NumericUpDown
            {
                Minimum = 1,
                Maximum = 12,
                Width = 80,
                Anchor = AnchorStyles.Left,
                Margin = new Padding(0, 4, 16, 0)
            };

            Label labelLicensePlate = new Label
            {
                Text = "SPZ:",
                AutoSize = true,
                Anchor = AnchorStyles.Left,
                Margin = new Padding(0, 8, 8, 0)
            };

            textBoxLicensePlate = new TextBox
            {
                Width = 140,
                Anchor = AnchorStyles.Left,
                Margin = new Padding(0, 4, 16, 0)
            };

            Button buttonRefreshView = new Button
            {
                Text = "Obnovit pohled",
                AutoSize = true,
                Anchor = AnchorStyles.Left,
                Margin = new Padding(0, 2, 0, 0)
            };
            buttonRefreshView.Click += (_, _) => RefreshParkingView();

            inputLayoutPanel.Controls.Add(labelFloor, 0, 0);
            inputLayoutPanel.Controls.Add(comboBoxSelectedFloor, 1, 0);
            inputLayoutPanel.Controls.Add(labelSpot, 2, 0);
            inputLayoutPanel.Controls.Add(numericUpDownSelectedSpot, 3, 0);
            inputLayoutPanel.Controls.Add(labelLicensePlate, 4, 0);
            inputLayoutPanel.Controls.Add(textBoxLicensePlate, 5, 0);
            inputLayoutPanel.Controls.Add(buttonRefreshView, 6, 0);

            TableLayoutPanel infoLayoutPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                ColumnCount = 1,
                RowCount = 2,
                AutoSize = true,
                Padding = new Padding(0, 0, 0, 8)
            };

            labelFloorSummary = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Text = "Není naèten parkovací dùm."
            };

            labelSelectionSummary = new Label
            {
                AutoSize = true,
                ForeColor = Color.DimGray,
                Text = "Vyber podlaží a místo."
            };

            infoLayoutPanel.Controls.Add(labelFloorSummary, 0, 0);
            infoLayoutPanel.Controls.Add(labelSelectionSummary, 0, 1);

            flowLayoutPanelParkingSpots = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(4),
                BackColor = Color.WhiteSmoke,
                WrapContents = true
            };

            parkingLayoutPanel.Controls.Add(inputLayoutPanel, 0, 0);
            parkingLayoutPanel.Controls.Add(infoLayoutPanel, 0, 1);
            parkingLayoutPanel.Controls.Add(flowLayoutPanelParkingSpots, 0, 2);

            panelParkingView.Controls.Add(parkingLayoutPanel);
        }

        private void WireUpEventHandlers()
        {

            buttonParkCar.Click += buttonParkCar_Click;
            buttonRemoveCar.Click += buttonRemoveCar_Click;

            buttonFindNearestFreeSpot.Click += buttonFindNearestFreeSpot_Click;
            buttonShowOccupiedSpots.Click += buttonShowOccupiedSpots_Click;
           

            comboBoxSelectedFloor.SelectedIndexChanged += (_, _) => RefreshParkingView();
            numericUpDownSelectedSpot.ValueChanged += (_, _) => UpdateSelectionSummary();
            textBoxLicensePlate.TextChanged += (_, _) => UpdateSelectionSummary();
        }

        private void buttonLoadFromFile_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialogMain.FileName = string.Empty;

                DialogResult dialogResult = openFileDialogMain.ShowDialog(this);
                if (dialogResult != DialogResult.OK)
                {
                    return;
                }

                ParkingHouse loadedParkingHouse =
                    ParkingHouseFileRepository.LoadFromFile(openFileDialogMain.FileName);

                currentParkingHouse = loadedParkingHouse;
                currentFilePath = openFileDialogMain.FileName;

                //AppendLog($"Naèten soubor: {currentFilePath}");
                //AppendLog(GetParkingHouseSummary(currentParkingHouse));

                PopulateFloorSelector();
                RefreshParkingView();
            }
            catch (Exception exception)
            {
                //AppendLog($"Chyba pøi naèítání: {exception.Message}");
                MessageBox.Show(
                    $"Nepodaøilo se naèíst data ze souboru.\n\n{exception.Message}",
                    "Chyba pøi naèítání",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void buttonSaveToFile_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentParkingHouse == null)
                {
                    MessageBox.Show(
                        "Není co uložit. Nejdøíve naèti soubor nebo vytvoø data.",
                        "Uložení není možné",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    //AppendLog("Pokus o uložení bez naètených dat.");
                    return;
                }

                if (!string.IsNullOrWhiteSpace(currentFilePath))
                {
                    saveFileDialogMain.FileName = Path.GetFileName(currentFilePath);
                    saveFileDialogMain.InitialDirectory = Path.GetDirectoryName(currentFilePath);
                }
                else
                {
                    saveFileDialogMain.FileName = "parkinghouse_data.txt";
                }

                DialogResult dialogResult = saveFileDialogMain.ShowDialog(this);
                if (dialogResult != DialogResult.OK)
                {
                    return;
                }

                ParkingHouseFileRepository.SaveToFile(currentParkingHouse, saveFileDialogMain.FileName);
                currentFilePath = saveFileDialogMain.FileName;

                //AppendLog($"Uložen soubor: {currentFilePath}");
            }
            catch (Exception exception)
            {
               // AppendLog($"Chyba pøi ukládání: {exception.Message}");
                MessageBox.Show(
                    $"Nepodaøilo se uložit data do souboru.\n\n{exception.Message}",
                    "Chyba pøi ukládání",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void buttonCreateSampleData_Click(object? sender, EventArgs e)
        {
            currentParkingHouse = new ParkingHouse(4, 12);
            currentFilePath = null;

            List<(int floorNumber, int spotNumber, string licensePlate)> sampleOccupancy = new()
            {
                (1, 1, "1AB1234"), (1, 2, "1BC2345"), (1, 5, "1CD3456"), (1, 6, "1DE4567"), (1, 11, "1EF5678"),
                (2, 2, "2AB1234"), (2, 3, "2BC2345"), (2, 4, "2CD3456"), (2, 8, "2DE4567"), (2, 10, "2EF5678"),
                (3, 1, "3AB1234"), (3, 7, "3BC2345"), (3, 8, "3CD3456"), (3, 9, "3DE4567"), (3, 12, "3EF5678"),
                (4, 3, "4AB1234"), (4, 4, "4BC2345"), (4, 5, "4CD3456"), (4, 9, "4DE4567"), (4, 10, "4EF5678")
            };

            foreach ((int floorNumber, int spotNumber, string licensePlate) occupiedSpot in sampleOccupancy)
            {
                currentParkingHouse.ParkCar(occupiedSpot.floorNumber, occupiedSpot.spotNumber, occupiedSpot.licensePlate);
            }

            PopulateFloorSelector();
            RefreshParkingView();
            //AppendLog("Byla vytvoøena ukázková data splòující minimální rozsah zadání.");
        }

        private void buttonParkCar_Click(object? sender, EventArgs e)
        {
            if (!EnsureParkingHouseIsLoaded())
            {
                return;
            }

            int selectedFloorNumber = GetSelectedFloorNumber();
            int selectedSpotNumber = (int)numericUpDownSelectedSpot.Value;
            string normalizedLicensePlate = textBoxLicensePlate.Text.Trim().ToUpperInvariant();

            if (string.IsNullOrWhiteSpace(normalizedLicensePlate))
            {
                MessageBox.Show(
                    "Zadej prosím SPZ vozidla.",
                    "Chybìjící SPZ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            bool wasParkedSuccessfully = currentParkingHouse!.ParkCar(
                selectedFloorNumber,
                selectedSpotNumber,
                normalizedLicensePlate);

            if (wasParkedSuccessfully)
            {
                //AppendLog($"Obsazeno místo {selectedSpotNumber} na podlaží {selectedFloorNumber}. SPZ: {normalizedLicensePlate}");
                textBoxLicensePlate.Clear();
                RefreshParkingView();
                return;
            }

            if (currentParkingHouse.TryGetLicensePlate(selectedFloorNumber, selectedSpotNumber, out string existingLicensePlate))
            {
               // AppendLog($"Nelze obsadit místo {selectedSpotNumber} na podlaží {selectedFloorNumber}, protože je již obsazené. SPZ: {existingLicensePlate}");
                MessageBox.Show(
                    $"Místo {selectedSpotNumber} na podlaží {selectedFloorNumber} je už obsazené.\nAktuální SPZ: {existingLicensePlate}",
                    "Místo je obsazené",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                //AppendLog($"Obsazení místa {selectedSpotNumber} na podlaží {selectedFloorNumber} se nezdaøilo.");
            }

            RefreshParkingView();
        }

        private void buttonRemoveCar_Click(object? sender, EventArgs e)
        {
            if (!EnsureParkingHouseIsLoaded())
            {
                return;
            }

            int selectedFloorNumber = GetSelectedFloorNumber();
            int selectedSpotNumber = (int)numericUpDownSelectedSpot.Value;
            bool wasRemovedSuccessfully = currentParkingHouse!.RemoveCar(selectedFloorNumber, selectedSpotNumber);

            if (wasRemovedSuccessfully)
            {
                //AppendLog($"Uvolnìno místo {selectedSpotNumber} na podlaží {selectedFloorNumber}.");
            }
            else
            {
                //AppendLog($"Místo {selectedSpotNumber} na podlaží {selectedFloorNumber} nebylo možné uvolnit, protože není obsazené.");
            }

            RefreshParkingView();
        }

        private void buttonCheckOccupied_Click(object? sender, EventArgs e)
        {
            if (!EnsureParkingHouseIsLoaded())
            {
                return;
            }

            int selectedFloorNumber = GetSelectedFloorNumber();
            int selectedSpotNumber = (int)numericUpDownSelectedSpot.Value;
            bool isOccupied = currentParkingHouse!.IsSpotOccupied(selectedFloorNumber, selectedSpotNumber);

            if (isOccupied && currentParkingHouse.TryGetLicensePlate(selectedFloorNumber, selectedSpotNumber, out string licensePlate))
            {
                //AppendLog($"Místo {selectedSpotNumber} na podlaží {selectedFloorNumber} je obsazené. SPZ: {licensePlate}");
            }
            else
            {
              //  AppendLog($"Místo {selectedSpotNumber} na podlaží {selectedFloorNumber} je volné.");
            }

            RefreshParkingView();
        }

        private void buttonFindNearestFreeSpot_Click(object? sender, EventArgs e)
        {
            //if (!EnsureParkingHouseIsLoaded())
            //{
            //    return;
            //}

            //int selectedFloorNumber = GetSelectedFloorNumber();
            //int requestedSpotNumber = (int)numericUpDownSelectedSpot.Value;
            //bool wasFound = currentParkingHouse!.TryFindNearestFreeSpot(selectedFloorNumber, requestedSpotNumber, out int nearestFreeSpotNumber);

            //if (wasFound)
            //{
            //    AppendLog($"Nejbližší volné místo k místu {requestedSpotNumber} na podlaží {selectedFloorNumber} je {nearestFreeSpotNumber}.");
            //    numericUpDownSelectedSpot.Value = nearestFreeSpotNumber;
            //}
            //else
            //{
            //    AppendLog($"Na podlaží {selectedFloorNumber} není žádné volné místo.");
            //}

            //RefreshParkingView();

            if (!EnsureParkingHouseIsLoaded())
            {
                return;
            }

            int selectedFloorNumber = GetSelectedFloorNumber();
            int requestedSpotNumber = (int)numericUpDownSelectedSpot.Value;

            bool wasFound = currentParkingHouse!.TryFindNearestFreeSpot(
                selectedFloorNumber,
                requestedSpotNumber,
                out int nearestFreeSpotNumber);

            if (wasFound)
            {
                numericUpDownSelectedSpot.Value = nearestFreeSpotNumber;
                RefreshParkingView();

                MessageBox.Show(
                    $"Nejbližší volné místo k místu {requestedSpotNumber} na podlaží {selectedFloorNumber} je místo {nearestFreeSpotNumber}.",
                    "Nejbližší volné místo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(
                    $"Na podlaží {selectedFloorNumber} není žádné volné místo.",
                    "Nejbližší volné místo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }


        }

        private void buttonShowOccupiedSpots_Click(object? sender, EventArgs e)
        {
            if (!EnsureParkingHouseIsLoaded())
            {
                return;
            }

            //int selectedFloorNumber = GetSelectedFloorNumber();
            //List<KeyValuePair<int, string>> occupiedSpots = currentParkingHouse!.GetOccupiedSpotsInOrder(selectedFloorNumber);

            //if (occupiedSpots.Count == 0)
            //{
            //    AppendLog($"Podlaží {selectedFloorNumber} nemá žádná obsazená místa.");
            //    return;
            //}

            //StringBuilder stringBuilder = new StringBuilder();
            //stringBuilder.AppendLine($"Obsazená místa na podlaží {selectedFloorNumber}:");

            //foreach (KeyValuePair<int, string> occupiedSpot in occupiedSpots)
            //{
            //    stringBuilder.AppendLine($"- místo {occupiedSpot.Key}: {occupiedSpot.Value}");
            //}

            //AppendLog(stringBuilder.ToString().TrimEnd());

            ShowOccupiedSpotsDialog();
        }

      





        private void ShowOccupiedSpotsDialog()
        {
            if (currentParkingHouse == null)
            {
                return;
            }

            Form occupiedSpotsDialog = new Form
            {
                Text = "Obsazená místa podle podlaží",
                StartPosition = FormStartPosition.CenterParent,
                Size = new Size(520, 420),
                MinimumSize = new Size(420, 320),
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };

            TableLayoutPanel mainLayoutPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3,
                Padding = new Padding(10)
            };

            mainLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            mainLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            mainLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            FlowLayoutPanel topPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false
            };

            Label labelFloor = new Label
            {
                Text = "Vyber podlaží:",
                AutoSize = true,
                Margin = new Padding(0, 8, 8, 0)
            };

            ComboBox comboBoxFloor = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Width = 120
            };

            for (int floorNumber = 1; floorNumber <= currentParkingHouse.FloorCount; floorNumber++)
            {
                comboBoxFloor.Items.Add(floorNumber);
            }

            int defaultFloor = GetSelectedFloorNumber();
            if (defaultFloor >= 1 && defaultFloor <= currentParkingHouse.FloorCount)
            {
                comboBoxFloor.SelectedItem = defaultFloor;
            }
            else if (comboBoxFloor.Items.Count > 0)
            {
                comboBoxFloor.SelectedIndex = 0;
            }

            topPanel.Controls.Add(labelFloor);
            topPanel.Controls.Add(comboBoxFloor);

            RichTextBox richTextBoxOccupiedSpots = new RichTextBox
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                Font = new Font("Consolas", 10F, FontStyle.Regular),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            Button buttonClose = new Button
            {
                Text = "Zavøít",
                AutoSize = true,
                Anchor = AnchorStyles.Right
            };
            buttonClose.Click += (_, _) => occupiedSpotsDialog.Close();

            FlowLayoutPanel bottomPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.RightToLeft,
                AutoSize = true
            };
            bottomPanel.Controls.Add(buttonClose);

            void RefreshOccupiedSpotsList()
            {
                if (comboBoxFloor.SelectedItem is not int selectedFloorNumber)
                {
                    richTextBoxOccupiedSpots.Text = "Není vybráno žádné podlaží.";
                    return;
                }

                List<KeyValuePair<int, string>> occupiedSpots =
                    currentParkingHouse.GetOccupiedSpotsInOrder(selectedFloorNumber);

                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine($"Obsazená místa na podlaží {selectedFloorNumber}:");
                stringBuilder.AppendLine();

                if (occupiedSpots.Count == 0)
                {
                    stringBuilder.AppendLine("Na tomto podlaží nejsou žádná obsazená místa.");
                }
                else
                {
                    foreach (KeyValuePair<int, string> occupiedSpot in occupiedSpots)
                    {
                        stringBuilder.AppendLine($"Místo {occupiedSpot.Key}: {occupiedSpot.Value}");
                    }
                }

                richTextBoxOccupiedSpots.Text = stringBuilder.ToString();
            }

            comboBoxFloor.SelectedIndexChanged += (_, _) => RefreshOccupiedSpotsList();

            mainLayoutPanel.Controls.Add(topPanel, 0, 0);
            mainLayoutPanel.Controls.Add(richTextBoxOccupiedSpots, 0, 1);
            mainLayoutPanel.Controls.Add(bottomPanel, 0, 2);

            occupiedSpotsDialog.Controls.Add(mainLayoutPanel);

            RefreshOccupiedSpotsList();
            occupiedSpotsDialog.ShowDialog(this);
        }



        private void PopulateFloorSelector()
        {
            comboBoxSelectedFloor.Items.Clear();

            if (currentParkingHouse == null)
            {
                comboBoxSelectedFloor.Text = string.Empty;
                return;
            }

            for (int floorNumber = 1; floorNumber <= currentParkingHouse.FloorCount; floorNumber++)
            {
                comboBoxSelectedFloor.Items.Add(floorNumber);
            }

            if (comboBoxSelectedFloor.Items.Count > 0)
            {
                comboBoxSelectedFloor.SelectedIndex = 0;
            }

            numericUpDownSelectedSpot.Minimum = 1;
            numericUpDownSelectedSpot.Maximum = currentParkingHouse.SpotsPerFloor;
            numericUpDownSelectedSpot.Value = 1;
        }

        private void RefreshParkingView()
        {
            flowLayoutPanelParkingSpots.Controls.Clear();

            if (currentParkingHouse == null)
            {
                labelFloorSummary.Text = "Není naèten parkovací dùm.";
                labelSelectionSummary.Text = "";// "Použij naètení souboru nebo ukázková data.";

                Label placeholderLabel = new Label
                {
                    AutoSize = false,
                    Width = Math.Max(flowLayoutPanelParkingSpots.Width - 20, 300),
                    Height = 80,
                    TextAlign = ContentAlignment.MiddleCenter,
                    ForeColor = Color.DimGray,
                    Text = "Zatím nejsou k dispozici žádná data parkovacího domu."
                };

                flowLayoutPanelParkingSpots.Controls.Add(placeholderLabel);
                return;
            }

            int selectedFloorNumber = GetSelectedFloorNumber();
            ParkingFloor selectedFloor = currentParkingHouse.GetFloor(selectedFloorNumber);

            labelFloorSummary.Text =
                $"Podlaží {selectedFloorNumber}: obsazeno {selectedFloor.OccupiedSpotCount} / {selectedFloor.TotalSpotCount}, volno {selectedFloor.FreeSpotCount}";

            for (int spotNumber = 1; spotNumber <= currentParkingHouse.SpotsPerFloor; spotNumber++)
            {
                bool isSpotOccupied = currentParkingHouse.IsSpotOccupied(selectedFloorNumber, spotNumber);
                string buttonText = $"Místo {spotNumber}";

                if (isSpotOccupied && currentParkingHouse.TryGetLicensePlate(selectedFloorNumber, spotNumber, out string licensePlate))
                {
                    buttonText += Environment.NewLine + licensePlate;
                }
                else
                {
                    buttonText += Environment.NewLine + "VOLNÉ";
                }

                Button parkingSpotButton = new Button
                {
                    Width = 110,
                    Height = 58,
                    Margin = new Padding(6),
                    Text = buttonText,
                    Tag = spotNumber,
                    BackColor = isSpotOccupied ? Color.MistyRose : Color.Honeydew,
                    FlatStyle = FlatStyle.Flat,
                    TextAlign = ContentAlignment.MiddleCenter
                };

                parkingSpotButton.Click += ParkingSpotButton_Click;
                flowLayoutPanelParkingSpots.Controls.Add(parkingSpotButton);
            }

            UpdateSelectionSummary();
        }

        private void ParkingSpotButton_Click(object? sender, EventArgs e)
        {
            if (sender is not Button parkingSpotButton || parkingSpotButton.Tag is not int selectedSpotNumber)
            {
                return;
            }

            numericUpDownSelectedSpot.Value = selectedSpotNumber;

            if (currentParkingHouse != null)
            {
                int selectedFloorNumber = GetSelectedFloorNumber();

                if (currentParkingHouse.TryGetLicensePlate(selectedFloorNumber, selectedSpotNumber, out string licensePlate))
                {
                    textBoxLicensePlate.Text = licensePlate;
                }
                else
                {
                    textBoxLicensePlate.Clear();
                }
            }

            UpdateSelectionSummary();
        }

        private void UpdateSelectionSummary()
        {
            if (currentParkingHouse == null)
            {
                labelSelectionSummary.Text = "Vyber podlaží a místo.";
                return;
            }

            int selectedFloorNumber = GetSelectedFloorNumber();
            int selectedSpotNumber = (int)numericUpDownSelectedSpot.Value;
            string licensePlateText = textBoxLicensePlate.Text.Trim();

            if (currentParkingHouse.TryGetLicensePlate(selectedFloorNumber, selectedSpotNumber, out string existingLicensePlate))
            {
                labelSelectionSummary.Text = $"Vybrané místo {selectedSpotNumber} je obsazené. Aktuální SPZ: {existingLicensePlate}";
                labelSelectionSummary.ForeColor = Color.Firebrick;
            }
            else if (string.IsNullOrWhiteSpace(licensePlateText))
            {
                labelSelectionSummary.Text = $"Vybrané místo {selectedSpotNumber} je volné. Pro obsazení doplò SPZ.";
                labelSelectionSummary.ForeColor = Color.DimGray;
            }
            else
            {
                labelSelectionSummary.Text = $"Vybrané místo {selectedSpotNumber} je volné. Pøipraveno k obsazení vozidlem {licensePlateText.ToUpperInvariant()}.";
                labelSelectionSummary.ForeColor = Color.DarkGreen;
            }
        }

        private int GetSelectedFloorNumber()
        {
            if (comboBoxSelectedFloor.SelectedItem is int selectedFloorNumber)
            {
                return selectedFloorNumber;
            }

            return 1;
        }

        private bool EnsureParkingHouseIsLoaded()
        {
            if (currentParkingHouse != null)
            {
                return true;
            }

            MessageBox.Show(
                "Nejdøíve naèti data ze souboru nebo vytvoø ukázková data.",
                "Chybí data",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);

            return false;
        }

        private string GetParkingHouseSummary(ParkingHouse parkingHouse)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"Poèet podlaží: {parkingHouse.FloorCount}");
            stringBuilder.AppendLine($"Míst na podlaží: {parkingHouse.SpotsPerFloor}");

            for (int floorNumber = 1; floorNumber <= parkingHouse.FloorCount; floorNumber++)
            {
                int occupiedCount = parkingHouse.GetOccupiedSpotsInOrder(floorNumber).Count;
                stringBuilder.AppendLine($"Podlaží {floorNumber}: {occupiedCount} obsazených míst");
            }

            return stringBuilder.ToString();
        }

        //private void AppendLog(string message)
        //{
        //    string timestamp = DateTime.Now.ToString("HH:mm:ss");
        //    richTextBoxLog.AppendText($"[{timestamp}] {message}{Environment.NewLine}");
        //}
    }
}