using System;
using System.Collections.Generic;
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

            EventManager.RegisterClassHandler(typeof(AkerTextBox), TextBox.PreviewTextInputEvent, new TextCompositionEventHandler(AkerPreviewTextInputEvent));
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
            internal set { SetValue(AkerExpenseProperty, value); }
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
            if (AkerTextBoxType == AkerTextBoxType.Expense)
            {
                DataContext = this;
                binding = new Binding("AkerExpense")
                {
                    StringFormat = "{0:#,##0}VND",
                    Mode = BindingMode.TwoWay,
                    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                    //RelativeSource = new RelativeSource()
                    //{
                    //    Mode = RelativeSourceMode.FindAncestor,
                    //    AncestorType = typeof(AkerTextBox)
                    //}
                };
                binding.StringFormat = "{0:#,##0}" + " " + AkerExpenseUnit;
                bindingEx = SetBinding(TextProperty, binding);


                EventManager.RegisterClassHandler(typeof(AkerTextBox), TextBox.TextChangedEvent, new TextChangedEventHandler(AkerExpenseTextChangeEvent));
                EventManager.RegisterClassHandler(typeof(AkerTextBox), TextBox.SelectionChangedEvent, new RoutedEventHandler(AkerExpenseSelectionChangedEvent));
                //DataObject.AddPastingHandler(this, OnAkerTextBoxExpensePaste);
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
            UpdateAkerExpenseSelection("limit_selection");
        }

        private void AkerExpenseTextChangeEvent(object sender, TextChangedEventArgs e)
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
            UpdateAkerExpense();
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

        private void UpdateAkerExpense()
        {
            IsExpenseCallTextChange = true;
            AkerExpense = ConvertExpenseHelper();
            bindingEx?.UpdateTarget();
            IsExpenseCallTextChange = false;
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
                    string value = string.Join("", toArray);

                    result = Convert.ToDouble(value);
                }
            }
            catch (Exception e)
            {
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
            var regex = new Regex(@"^[0-9]*$");
            if ((regex.IsMatch(e.Text) && SelectionStart <= Text.IndexOf(AkerExpenseUnit)) &&
                !(e.Text.Equals("0") && (SelectionStart == 0 || ConvertExpenseHelper() == 0)))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
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
}
