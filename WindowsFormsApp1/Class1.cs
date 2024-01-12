using System;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

public enum Discount
{
    Ten = 10,
    Twenty = 15,
    Thirty = 20,
    Forty = 25,
    Fifty = 30,
    Sixty = 35,
    Seventy = 40
}

public partial class DiscountCalculatorForm : Form
{
    // Declare UI elements
    private Label priceLabel;
    private TextBox priceInput;
    private Label discountLabel;
    private ComboBox discountInput;
    private Label discountAmountLabel;
    private Label discountAmountValue;
    private Label discountedPriceLabel;
    private Label discountedPriceValue;
    private Button calculateButton;
    private Button exitButton;

    public DiscountCalculatorForm()
    {
        // Set form properties
        this.FormBorderStyle = FormBorderStyle.FixedSingle;
        this.MaximizeBox = false;
        this.BackColor = Color.LightBlue;
        this.Text = "Discount Warehouse";
        this.Size = new Size(435, 290);

        // Define fonts
        Font boldFont = new Font(FontFamily.GenericMonospace, 10.0F, FontStyle.Bold);
        Font butt_font = new Font(FontFamily.GenericSansSerif, 11.0F, FontStyle.Bold);

        // Initialize UI elements with properties
        priceLabel = new Label { Text = "Original Price", Location = new Point(20, 20), Font = boldFont, Width = 120 };
        priceInput = new TextBox { Location = new Point(20, 45), Font = boldFont, Width = 120 };
        discountLabel = new Label { Text = "Discount rates(%):", Location = new Point(220, 20), Font = boldFont, Width = 160 };
        discountInput = new ComboBox { Location = new Point(220, 45), Font = boldFont, Width = 70 };
        discountAmountLabel = new Label { Text = "Discount:", Location = new Point(20, 90), Font = boldFont, Width = 120 };
        discountAmountValue = new Label { Location = new Point(5, 0), Font = boldFont };
        discountedPriceLabel = new Label { Text = "Discounted Price:", Location = new Point(220, 90), Font = boldFont, Width = 180 };
        discountedPriceValue = new Label { Location = new Point(5, 0), Font = boldFont };
        calculateButton = new Button { Text = "Calculate", Location = new Point((this.Width - 60) / 5, 200), BackColor = Color.LightGreen, Font = butt_font, Width = 100, Height = 35 };
        exitButton = new Button { Text = "Exit", Location = new Point((this.Width - 50) / 2, 200), BackColor = Color.LightCoral, Font = butt_font, Width = 100, Height = 35 };

        // Populate discountInput with values from Discount enum
        discountInput.DataSource = Enum.GetValues(typeof(Discount))
                                    .Cast<Discount>()
                                    .Select(value => (int)value)
                                    .ToList();
        // Add event handlers for button clicks
        calculateButton.Click += CalculateButton_Click;
        exitButton.Click += (sender, e) => this.Close();

        // Create panels for displaying calculated values
        Panel discountedPricePanel = new Panel { BorderStyle = BorderStyle.FixedSingle, Location = new Point(220, 120), Width = 150, Height = 20 };
        discountedPricePanel.Controls.Add(discountedPriceValue);
        Panel discountAmountPanel = new Panel { BorderStyle = BorderStyle.FixedSingle, Location = new Point(20, 120), Width = 150, Height = 20 };
        discountAmountPanel.Controls.Add(discountAmountValue);

        // Add UI elements to form
        Controls.Add(priceLabel);
        Controls.Add(priceInput);
        Controls.Add(discountLabel);
        Controls.Add(discountInput);
        Controls.Add(discountAmountLabel);
        Controls.Add(discountAmountPanel);
        Controls.Add(discountedPriceLabel);
        Controls.Add(discountedPricePanel);
        Controls.Add(calculateButton);
        Controls.Add(exitButton);
    }

    // Event handler for calculateButton click
    private void CalculateButton_Click(object sender, EventArgs e)
    {
        // Parse price input
        decimal price;
        if (!decimal.TryParse(priceInput.Text, out price))
        {
            MessageBox.Show("Invalid price input. Please enter a valid number.");
            return;
        }
        // Parse selected discount
        Discount discount = (Discount)Enum.Parse(typeof(Discount), discountInput.SelectedItem.ToString());
        // Calculate discount amount and final price
        decimal discountAmount = price * ((decimal)discount / 100);
        decimal finalPrice = price - discountAmount;

        // Display calculated values
        discountAmountValue.Text = $"{discountAmount:F2}";
        discountedPriceValue.Text = $"{finalPrice:F2}";
        this.Refresh();
    }
}