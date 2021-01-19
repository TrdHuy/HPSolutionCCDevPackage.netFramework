using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;

namespace HPSolutionCCDevPackage.netFramework
{
    public class AkerTextBox : TextBox
    {
        public AkerTextBox()
        {
            DefaultStyleKey = typeof(AkerTextBox);
            this.IsTabStop = true;

        }

        #region AkerTextBoxType
        public static readonly DependencyProperty AkerTextBoxTypeProperty =
           DependencyProperty.Register(
                       "AkerTextBoxType",
                       typeof(AkerTextBoxType),
                       typeof(AkerTextBox),
                       new PropertyMetadata(
                               defaultAkerTextBoxType,
                               new PropertyChangedCallback(AkerTextBoxChangeCallback)),
                       null);

        private static void AkerTextBoxChangeCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }

        public AkerTextBoxType AkerTextBoxType
        {
            get { return (AkerTextBoxType)GetValue(AkerTextBoxTypeProperty); }
            set { SetValue(AkerTextBoxTypeProperty, value); }
        }
        #endregion

        #region AkerExpense
        public static readonly DependencyProperty AkerExpenseProperty =
            DependencyProperty.Register("AkerExpense", typeof(double), typeof(AkerTextBox),
                                                new PropertyMetadata(defaultAkerExpense,
                                                    new PropertyChangedCallback(AkerExpenseChangedCallback)));


        private static void AkerExpenseChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            AkerTextBox ctrl = d as AkerTextBox;
        }

        /// <summary>
        /// Used to set the AkerExpense
        /// </summary>
        public double AkerExpense
        {
            get { return (double)GetValue(AkerExpenseProperty); }
            set { SetValue(AkerExpenseProperty, value); }
        }

        #endregion

        #region AkerExpenseUnit
        public static readonly DependencyProperty AkerExpenseUnitProperty =
          DependencyProperty.Register(
                      "AkerExpenseUnit",
                      typeof(string),
                      typeof(AkerTextBox),
                      new PropertyMetadata(
                              defaultAkerExpenseUnit));


        public string AkerExpenseUnit
        {
            get { return (string)GetValue(AkerExpenseUnitProperty); }
            set { SetValue(AkerExpenseUnitProperty, value); }
        }

        #endregion

        private static AkerTextBoxType defaultAkerTextBoxType = AkerTextBoxType.Normal;
        private static double defaultAkerExpense = 0d;
        private static string defaultAkerExpenseUnit = "$";

        private Binding binding;
        private BindingExpressionBase bindingEx;
        private bool IsExpenseCallTextChange = false;
        private int OldLenght = -1;
        private int OldCursorPos = -1;
        private int LastSelectableIndex = 0;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.PreviewTextInput += new TextCompositionEventHandler(AkerPreviewTextInputEvent);

            if (AkerTextBoxType == AkerTextBoxType.Expense)
            {

                this.TextChanged -= new TextChangedEventHandler(AkerExpenseTextChangeEvent);
                this.SelectionChanged -= new RoutedEventHandler(AkerExpenseSelectionChangedEvent);

                binding = new Binding("AkerExpense")
                {
                    Mode = BindingMode.TwoWay,
                    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                    RelativeSource = new RelativeSource()
                    {
                        Mode = RelativeSourceMode.Self
                    },
                    Converter = new ExpenseConverter(AkerExpenseUnit)
                };
                bindingEx = SetBinding(TextProperty, binding);

                //DataObject.AddPastingHandler(this, OnAkerTextBoxExpensePaste);
                this.TextChanged += new TextChangedEventHandler(AkerExpenseTextChangeEvent);
                this.SelectionChanged += new RoutedEventHandler(AkerExpenseSelectionChangedEvent);

            }

            ValidateAkerProperty();
        }


        private void ValidateAkerProperty()
        {
            if (!String.IsNullOrEmpty(AkerExpenseUnit) && AkerTextBoxType != AkerTextBoxType.Expense)
            {
                throw new InvalidOperationException("Cannot use AkerExpenseUnit without the type is Expense");
            }
        }

        private void OnAkerTextBoxExpensePaste(object sender, DataObjectPastingEventArgs e)
        {
            // Handle paste event here
            // var text = e.SourceDataObject.GetData(DataFormats.UnicodeText) as string;
        }

        private void AkerExpenseSelectionChangedEvent(object sender, RoutedEventArgs e)
        {
            try
            {
                UpdateAkerExpenseSelection("limit_selection");
            }
            catch (Exception ex)
            {
                //MessageBox.Show("AkerExpenseSelectionChangedEvent:" + ex.Message);
            }
        }

        private void AkerExpenseTextChangeEvent(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (IsExpenseCallTextChange)
                {
                    UpdateAkerExpenseSelection("aker_text_change");
                }
                else
                {
                    OldCursorPos = SelectionStart;
                    OldLenght = Text.Length;
                }
                //UpdateAkerExpense();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("AkerExpenseTextChangeEvent:" + ex.Message);
            }

        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            // Special handle for Expense type
            if (AkerTextBoxType == AkerTextBoxType.Expense)
            {
                if (// If the key is back space and current cursor greater than expense unit string position
                       ((e.Key == Key.Back && SelectionStart > LastSelectableIndex) ||
                       // If the key is delete and current cursor equal expense unit string position
                       (e.Key == Key.Delete && SelectionStart == LastSelectableIndex) ||
                       // If the key is back space or delete when expense value equal 0
                       ((e.Key == Key.Delete || e.Key == Key.Back) && ConvertExpenseHelper() == 0)))
                {
                    e.Handled = true;
                }
                // If the key is back space and current cursor is after ',' 
                else if (e.Key == Key.Back && SelectionStart != 0 && Text[SelectionStart - 1].Equals(','))
                {
                    HandleBackSpaceAfterComma(e);
                }
                else
                {
                    base.OnPreviewKeyDown(e);
                }
            }
            else
            {
                base.OnPreviewKeyDown(e);
            }
        }

        // Method handle for case cursor after comma, and the key is back space
        private void HandleBackSpaceAfterComma(KeyEventArgs e)
        {
            e.Handled = false;
            UpdateAkerExpenseSelection("back_space_after_comma");
            base.OnPreviewKeyDown(e);
        }

        private void UpdateAkerExpenseSelection(string handle)
        {
            try
            {
                int selectionEnd = 0;
                if (Text.IndexOf(AkerExpenseUnit) > 0)
                {
                    LastSelectableIndex = Text.IndexOf(AkerExpenseUnit) - 1;
                    selectionEnd = SelectionStart + SelectionLength;
                }
                switch (handle)
                {
                    case "back_space_after_comma":
                        SelectionStart = SelectionStart - 1;
                        break;
                    case "aker_text_change":
                        if (OldCursorPos != -1)
                        {
                            if (Text.Length > OldLenght)
                            {
                                SelectionStart = OldCursorPos + 1;
                            }
                            else if (Text.Length == OldLenght)
                            {
                                SelectionStart = OldCursorPos;
                            }
                            else if (Text.Length < OldLenght && OldCursorPos > 0)
                            {
                                SelectionStart = OldCursorPos - 1;
                            }
                        }
                        break;
                    case "limit_selection":
                        if (SelectionStart > LastSelectableIndex)
                        {
                            SelectionStart = LastSelectableIndex;
                        }
                        else if (selectionEnd > LastSelectableIndex)
                        {
                            SelectionLength -= (selectionEnd - LastSelectableIndex);
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception e)
            {
                //MessageBox.Show("UpdateAkerExpenseSelection:" + "handle = " + handle + "__message:" + e.Message);
            }

        }

        private void UpdateAkerExpense()
        {
            try
            {
                IsExpenseCallTextChange = true;
                AkerExpense = ConvertExpenseHelper();
                bindingEx?.UpdateTarget();
                IsExpenseCallTextChange = false;
            }
            catch (Exception e)
            {
                //MessageBox.Show("UpdateAkerExpense:" + e.ToString());
            }
            finally
            {
                IsExpenseCallTextChange = false;
            }

        }

        // Method to convert current text in textbox to double value
        // this will get all number from current text, then concat them,
        // and last convert it to double
        private double ConvertExpenseHelper()
        {
            double result = 0;
            try
            {

                if (!String.IsNullOrEmpty(Text))
                {
                    var matches = Regex.Matches(Text, @"\d+");
                    var toArray = from Match match in matches select match.Value;
                    string cED = string.Join("", toArray);

                    if (!String.IsNullOrEmpty(cED))
                    {
                        result = Convert.ToDouble(cED);
                    }
                    else
                    {
                        result = 0;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("ConvertExpenseHelper:" + e.ToString());
                result = 0;
            }
            return result;
        }

        private void AkerPreviewTextInputEvent(object sender, TextCompositionEventArgs e)
        {
            AkerTextBox ctrl = sender as AkerTextBox;
            switch (ctrl.AkerTextBoxType)
            {
                case AkerTextBoxType.Decimal:
                    DecimalTextBoxInput(e);
                    break;
                case AkerTextBoxType.Numeric:
                    NumericTextBoxInput(e);
                    break;
                case AkerTextBoxType.Alphabet:
                    AlphabetTextBoxInput(e);
                    break;
                case AkerTextBoxType.Expense:
                    ExpenseTextBoxInput(e);
                    break;
                default:
                    break;
            }
        }

        private void DecimalTextBoxInput(TextCompositionEventArgs e)
        {
            var regex = new Regex(@"^[0-9]*(?:\.[0-9]*)?$");
            if (regex.IsMatch(e.Text) && !(e.Text == "." && Text.Contains(e.Text)))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void NumericTextBoxInput(TextCompositionEventArgs e)
        {
            var regex = new Regex(@"^[0-9]*$");
            if (regex.IsMatch(e.Text))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void AlphabetTextBoxInput(TextCompositionEventArgs e)
        {
            var regex = new Regex(@"^[a-zA-Z_]*$");
            if (regex.IsMatch(e.Text))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void ExpenseTextBoxInput(TextCompositionEventArgs e)
        {
            double v = 0;
            try
            {
                v = ConvertExpenseHelper();
                var regex = new Regex(@"^[0-9]*$");
                if (regex.IsMatch(e.Text) && !(e.Text.Equals("0") && (SelectionStart == 0 || v == 0)))
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("ExpenseTextBoxInput:" + ex.Message);
                //MessageBox.Show("ExpenseTextBoxInput:" + ex.Message + "\n" +
                //    "Text = " + Text == null ? "null" : Text + "\n" +
                //    "SelectionStart = " + SelectionStart + "\n" +
                //    "AkerExpenseUnit = " + AkerExpenseUnit == null ? "null" : AkerExpenseUnit + "\n"
                //    );
            }

        }
    }

    public enum AkerTextBoxType
    {
        Normal = 0,
        Numeric = 1,
        Decimal = 2,
        Alphabet = 3,
        Expense = 4
    }

    internal class ExpenseConverter : IValueConverter
    {
        private string ExpenseUnit;

        internal ExpenseConverter(string unit)
        {
            ExpenseUnit = unit;
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string res = String.Format("{0:N0} " + ExpenseUnit,
                         value);
            return res;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double result = 0;
            try
            {

                if (!String.IsNullOrEmpty(value.ToString()))
                {
                    var matches = Regex.Matches(value.ToString(), @"\d+");
                    var toArray = from Match match in matches select match.Value;
                    string cED = string.Join("", toArray);

                    if (!String.IsNullOrEmpty(cED))
                    {
                        result = System.Convert.ToDouble(cED);
                    }
                    else
                    {
                        result = 0;
                    }
                }
            }
            catch (Exception e)
            {
                result = 0;
            }
            return result;
        }
    }
}
