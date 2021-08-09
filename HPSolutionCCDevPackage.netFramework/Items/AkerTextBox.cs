using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Security;
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
            DependencyProperty.Register("AkerExpense", typeof(decimal), typeof(AkerTextBox),
                                                new PropertyMetadata(defaultAkerExpense,
                                                    new PropertyChangedCallback(AkerExpenseChangedCallback)));


        private static void AkerExpenseChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            AkerTextBox ctrl = d as AkerTextBox;
        }

        /// <summary>
        /// Used to set the AkerExpense
        /// </summary>
        public decimal AkerExpense
        {
            get { return (decimal)GetValue(AkerExpenseProperty); }
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

        #region AkerNumerRange
        public static readonly DependencyProperty AkerNumberRangeProperty =
           DependencyProperty.Register(
                       "AkerNumerRange",
                       typeof(Range),
                       typeof(AkerTextBox),
                       new PropertyMetadata(
                               default(Range),
                               new PropertyChangedCallback(AkerNumberRangeChangedCallback)),
                       new ValidateValueCallback(IsRangeValid));

        private static bool IsRangeValid(object value)
        {
            Range t = (Range)value;
            return t.IsValid(false, false, false, false);
        }

        private static void AkerNumberRangeChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ctrl = d as AkerTextBox;
            ctrl.OnNumberRangeChange((Range)e.OldValue, (Range)e.NewValue);
        }

        public Range AkerNumerRange
        {
            get { return (Range)GetValue(AkerNumberRangeProperty); }
            set { SetValue(AkerNumberRangeProperty, value); }
        }
        #endregion

        #region AkerNumerFormat
        public static readonly DependencyProperty AkerNumerFormatProperty =
           DependencyProperty.Register(
                       "AkerNumerFormat",
                       typeof(string),
                       typeof(AkerTextBox),
                       new PropertyMetadata(
                               default(string),
                               new PropertyChangedCallback(AkerNumerFormatChangedCallback)),
                       null);

        private static void AkerNumerFormatChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ctrl = d as AkerTextBox;
            ctrl.OnNumberFormatChange((string)e.OldValue, (string)e.NewValue);
        }

        public string AkerNumerFormat
        {
            get { return (string)GetValue(AkerNumerFormatProperty); }
            set { SetValue(AkerNumerFormatProperty, value); }
        }

        #endregion

        private static AkerTextBoxType defaultAkerTextBoxType = AkerTextBoxType.Normal;
        private static decimal defaultAkerExpense = default(decimal);
        private static string defaultAkerExpenseUnit = "$";

        private Binding binding;
        private BindingExpressionBase bindingEx;
        private bool IsExpenseCallTextChange = false;
        private int OldLenght = -1;
        private int OldCursorPos = -1;
        private int LastSelectableIndex = 0;

        public override void OnApplyTemplate()
        {
            ValidateAkerProperty();

            base.OnApplyTemplate();
            this.PreviewTextInput += new TextCompositionEventHandler(AkerPreviewTextInputEvent);

            if (AkerTextBoxType == AkerTextBoxType.Expense)
            {

                this.TextChanged -= AkerExpenseTextChangeEvent;
                this.SelectionChanged -= AkerExpenseSelectionChangedEvent;

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
                this.TextChanged += AkerExpenseTextChangeEvent;
                this.SelectionChanged += AkerExpenseSelectionChangedEvent;

            }
            else if (AkerTextBoxType == AkerTextBoxType.Numeric
                || AkerTextBoxType == AkerTextBoxType.Decimal)
            {
                this.TextChanged -= AkerNumberTextChangedEvent;
                this.TextChanged += AkerNumberTextChangedEvent;
                FormatAkerNumber();
            }

        }

        protected override void OnLostFocus(RoutedEventArgs e)
        {
            base.OnLostFocus(e);
            if (AkerTextBoxType == AkerTextBoxType.Numeric
               || AkerTextBoxType == AkerTextBoxType.Decimal)
            {
                FormatAkerNumber();
            }
        }

        private void ValidateAkerProperty()
        {
            if (!String.IsNullOrEmpty(AkerExpenseUnit) && AkerTextBoxType != AkerTextBoxType.Expense)
            {
                throw new InvalidOperationException("Cannot use AkerExpenseUnit without the type is Expense");
            }
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

        /// <summary>
        /// Occurs when aker type is numberic or decimal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AkerNumberTextChangedEvent(object sender, TextChangedEventArgs e)
        {
            if (!IsFocused)
            {
                FormatAkerNumber();
            }
        }

        /// <summary>
        /// Use to format aker number string follow aker format
        /// </summary>
        private void FormatAkerNumber()
        {
            if (AkerTextBoxType == AkerTextBoxType.Decimal
                            || AkerTextBoxType == AkerTextBoxType.Numeric)
            {
                this.TextChanged -= AkerNumberTextChangedEvent;

                if (AkerNumerRange == default(Range))
                {
                    if (String.IsNullOrEmpty(Text))
                    {
                        Text = "0";
                    }
                    // In case Text = "01234", trim the "0" number off the Text
                    else
                    {
                        try
                        {
                            Text = Convert.ToDouble(Text).ToString();
                        }
                        catch
                        {
                            Text = "0";
                        }
                    }
                    this.TextChanged += AkerNumberTextChangedEvent;
                    return;
                }

                this.TextChanged -= AkerNumberTextChangedEvent;

                // Cache the caret index and text lenght
                var oldCaretIndex = CaretIndex;

                var valueToCheck = 0d;

                try
                {
                    valueToCheck = Convert.ToDouble(Text);
                }
                catch { }

                var rangeChecker = AkerNumerRange.InRange(valueToCheck);
                if (rangeChecker == 0)
                {
                    Text = FormatAkerNumber(AkerNumerFormat, Text);
                }
                else if (rangeChecker == -1)
                {
                    Text = FormatAkerNumber(AkerNumerFormat, AkerNumerRange.Min.ToString());
                }
                else if (rangeChecker == 1)
                {
                    Text = FormatAkerNumber(AkerNumerFormat, AkerNumerRange.Max.ToString());
                }

                //Update if Text is empty
                if (String.IsNullOrEmpty(Text))
                {
                    Text = AkerNumerRange.Min.ToString();
                }

                //handle caret index
                var newTextLenght = Text.Length;

                if (newTextLenght < oldCaretIndex)
                {
                    CaretIndex = newTextLenght;
                }
                else
                {
                    CaretIndex = oldCaretIndex;
                }

                this.TextChanged += AkerNumberTextChangedEvent;
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

        /// <summary>
        /// Method handle for case cursor after comma, and the key is back space
        /// </summary>
        /// <param name="e"></param>
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


        /// <summary>
        /// Method to convert current text in textbox to double value
        /// this will get all number from current text, then concat them,
        /// and last convert it to double
        /// </summary>
        /// <returns></returns>
        private decimal ConvertExpenseHelper()
        {
            decimal result = 0;
            try
            {

                if (!String.IsNullOrEmpty(Text))
                {
                    var matches = Regex.Matches(Text, @"\d+");
                    var toArray = from Match match in matches select match.Value;
                    string cED = string.Join("", toArray);

                    if (!String.IsNullOrEmpty(cED))
                    {
                        result = Convert.ToDecimal(cED);
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

        #region Preview Input event
        /// <summary>
        /// Occur whenever an input is executed to aker at preview session
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            else if (e.Text == "-" && !Text.Contains(e.Text) && CaretIndex == 0)
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
            decimal v = 0;
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
            catch
            {
            }

        }

        #endregion

        #region Aker Numberic & Decimal Handler area
        /// <summary>
        /// Occurs whenever the range of number is changed
        /// </summary>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        private void OnNumberRangeChange(Range oldValue, Range newValue)
        {
            if (AkerTextBoxType == AkerTextBoxType.Decimal
                || AkerTextBoxType == AkerTextBoxType.Numeric)
            {
                if (!string.IsNullOrEmpty(Text))
                {
                    var num = Convert.ToDouble(Text);
                    switch (newValue.InRange(num))
                    {
                        case 0:
                            break;
                        case 1:
                            Text = AkerTextBoxType == AkerTextBoxType.Decimal ?
                                Convert.ToDecimal(newValue.Max).ToString()
                                : Convert.ToInt32(newValue.Max).ToString();
                            break;
                        case -1:
                            Text = AkerTextBoxType == AkerTextBoxType.Decimal ?
                                Convert.ToDecimal(newValue.Min).ToString()
                                : Convert.ToInt32(newValue.Min).ToString();
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Occurs whenever the number format string is changed
        /// </summary>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        private void OnNumberFormatChange(string oldValue, string newValue)
        {
            if ((AkerTextBoxType == AkerTextBoxType.Decimal
                    || AkerTextBoxType == AkerTextBoxType.Numeric)
                    && !string.IsNullOrEmpty(AkerNumerFormat))
            {
                if (!string.IsNullOrEmpty(Text))
                {
                    Text = FormatAkerNumber(newValue, Text);
                }
            }
        }

        /// <summary>
        /// Method to convert aker number to string same with the aker format
        /// </summary>
        /// <param name="akerFormat"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        private string FormatAkerNumber(string akerFormat, string v)
        {
            if (string.IsNullOrEmpty(akerFormat)) return v.ToString();

            try
            {
                var num = Convert.ToInt32(v);
                return num.ToString(akerFormat);
            }
            catch
            {
                return "";
            }

        }
        #endregion
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
            decimal result = 0;
            try
            {

                if (!String.IsNullOrEmpty(value.ToString()))
                {
                    var matches = Regex.Matches(value.ToString(), @"\d+");
                    var toArray = from Match match in matches select match.Value;
                    string cED = string.Join("", toArray);

                    if (!String.IsNullOrEmpty(cED))
                    {
                        result = System.Convert.ToDecimal(cED);
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


    [TypeConverter(typeof(RangeTypeConverter))]
    public struct Range : IEquatable<Range>
    {
        #region private field
        private double _min;
        private double _max;
        #endregion

        public double Min { get => _min; set => _min = value; }
        public double Max { get => _max; set => _max = value; }

        public Range(double min, double max)
        {
            _min = min;
            _max = max;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="num"></param>
        /// <returns>0 if in range, -1 if lower than range, 1 if higher than range</returns>
        public int InRange(double num)
        {
            return (num >= Min && num <= Max) ? 0
                : (num < Min) ? -1
                : 1;
        }

        /// <summary>
        /// Verifies if this Thickness contains only valid values
        /// The set of validity checks is passed as parameters.
        /// </summary>
        /// <param name='allowNegative'>allows negative values</param>
        /// <param name='allowNaN'>allows Double.NaN</param>
        /// <param name='allowPositiveInfinity'>allows Double.PositiveInfinity</param>
        /// <param name='allowNegativeInfinity'>allows Double.NegativeInfinity</param>
        /// <returns>Whether or not the thickness complies to the range specified</returns>
        internal bool IsValid(bool allowNegative, bool allowNaN, bool allowPositiveInfinity, bool allowNegativeInfinity)
        {
            if (!allowNegative)
            {
                if (Min < 0d || Max < 0d)
                    return false;
            }

            if (!allowNaN)
            {
                if (Double.IsNaN(Min) || Double.IsNaN(Max))
                    return false;
            }

            if (!allowPositiveInfinity)
            {
                if (Double.IsPositiveInfinity(Min) || Double.IsPositiveInfinity(Max))
                {
                    return false;
                }
            }

            if (!allowNegativeInfinity)
            {
                if (Double.IsNegativeInfinity(Min) || Double.IsNegativeInfinity(Max))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// This function returns a hash code.
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            return _min.GetHashCode() ^ _max.GetHashCode();
        }

        /// <summary>
        /// This function compares to the provided object for type and value equality.
        /// </summary>
        /// <param name="obj">Object to compare</param>
        /// <returns>True if object is a Thickness and all sides of it are equal to this Thickness'.</returns>
        public override bool Equals(object obj)
        {
            if (obj is Range)
            {
                Range otherObj = (Range)obj;
                return (this == otherObj);
            }
            return (false);
        }

        /// <summary>
        /// Compares this instance of Thickness with another instance.
        /// </summary>
        /// <param name="other">Range instance to compare.</param>
        /// <returns><c>true</c>if this Thickness instance has the same value 
        /// and unit type as thickness.</returns>
        public bool Equals(Range other)
        {
            return this.Min == other.Min && this.Max == other.Max;
        }

        public static bool operator ==(Range t1, Range t2)
        {
            return t1.Min == t2.Min && t1.Max == t2.Max;
        }

        public static bool operator !=(Range t1, Range t2)
        {
            return !(t1 == t2);
        }
    }

    internal class RangeTypeConverter : TypeConverter
    {
        #region Public Methods

        /// <summary>
        /// CanConvertFrom - Returns whether or not this class can convert from a given type.
        /// </summary>
        /// <returns>
        /// bool - True if thie converter can convert from the provided type, false if not.
        /// </returns>
        /// <param name="typeDescriptorContext"> The ITypeDescriptorContext for this call. </param>
        /// <param name="sourceType"> The Type being queried for support. </param>
        public override bool CanConvertFrom(ITypeDescriptorContext typeDescriptorContext, Type sourceType)
        {
            // We can only handle strings, integral and floating types
            TypeCode tc = Type.GetTypeCode(sourceType);
            switch (tc)
            {
                case TypeCode.String:
                case TypeCode.Decimal:
                case TypeCode.Single:
                case TypeCode.Double:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    return true;

                default:
                    return false;
            }
        }

        /// <summary>
        /// CanConvertTo - Returns whether or not this class can convert to a given type.
        /// </summary>
        /// <returns>
        /// bool - True if this converter can convert to the provided type, false if not.
        /// </returns>
        /// <param name="typeDescriptorContext"> The ITypeDescriptorContext for this call. </param>
        /// <param name="destinationType"> The Type being queried for support. </param>
        public override bool CanConvertTo(ITypeDescriptorContext typeDescriptorContext, Type destinationType)
        {
            // We can convert to an InstanceDescriptor or to a string.
            if (destinationType == typeof(InstanceDescriptor)
                || destinationType == typeof(string))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// ConvertFrom - Attempt to convert to a Range from the given object
        /// </summary>
        /// <returns>
        /// The Range which was constructed.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// An ArgumentNullException is thrown if the example object is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// An ArgumentException is thrown if the example object is not null and is not a valid type
        /// which can be converted to a Range.
        /// </exception>
        /// <param name="typeDescriptorContext"> The ITypeDescriptorContext for this call. </param>
        /// <param name="cultureInfo"> The CultureInfo which is respected when converting. </param>
        /// <param name="source"> The object to convert to a Range. </param>
        public override object ConvertFrom(ITypeDescriptorContext typeDescriptorContext, CultureInfo cultureInfo, object source)
        {
            if (source != null)
            {
                if (source is string) { return FromString((string)source, cultureInfo); }
            }
            throw GetConvertFromException(source);
        }

        /// <summary>
        /// ConvertTo - Attempt to convert a Range to the given type
        /// </summary>
        /// <returns>
        /// The object which was constructoed.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// An ArgumentNullException is thrown if the example object is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// An ArgumentException is thrown if the object is not null and is not a Range,
        /// or if the destinationType isn't one of the valid destination types.
        /// </exception>
        /// <param name="typeDescriptorContext"> The ITypeDescriptorContext for this call. </param>
        /// <param name="cultureInfo"> The CultureInfo which is respected when converting. </param>
        /// <param name="value"> The Range to convert. </param>
        /// <param name="destinationType">The type to which to convert the Range instance. </param>
        ///<SecurityNote>
        ///     Critical: calls InstanceDescriptor ctor which LinkDemands
        ///     PublicOK: can only make an InstanceDescriptor for Range, not an arbitrary class
        ///</SecurityNote> 
        [SecurityCritical]
        public override object ConvertTo(ITypeDescriptorContext typeDescriptorContext, CultureInfo cultureInfo, object value, Type destinationType)
        {
            if (null == value)
            {
                throw new ArgumentNullException("value");
            }

            if (null == destinationType)
            {
                throw new ArgumentNullException("destinationType");
            }

            if (!(value is Range))
            {
                throw new ArgumentException("Incorrect format");
            }

            Range th = (Range)value;
            if (destinationType == typeof(string)) { return ToString(th, cultureInfo); }
            if (destinationType == typeof(InstanceDescriptor))
            {
                ConstructorInfo ci = typeof(Range).GetConstructor(new Type[] { typeof(double), typeof(double) });
                return new InstanceDescriptor(ci, new object[] { th.Min, th.Max });
            }

            throw new ArgumentException("Incorrect format");

        }


        #endregion Public Methods

        //-------------------------------------------------------------------
        //
        //  Internal Methods
        //
        //-------------------------------------------------------------------

        #region Internal Methods

        static internal string ToString(Range th, CultureInfo cultureInfo)
        {

            // Initial capacity [64] is an estimate based on a sum of:
            // 48 = 4x double (twelve digits is generous for the range of values likely)
            //  8 = 4x Unit Type string (approx two characters)
            //  4 = 4x separator characters
            StringBuilder sb = new StringBuilder(64);


            sb.Append(Convert.ToString(Double.IsNaN(th.Min) ? "Auto" : th.Min.ToString(), cultureInfo));
            sb.Append(",");
            sb.Append(Convert.ToString(Double.IsNaN(th.Min) ? "Auto" : th.Max.ToString(), cultureInfo));
            return sb.ToString();
        }

        static internal Range FromString(string s, CultureInfo cultureInfo)
        {
            var r = s.Split(',');
            if (r.Length == 2)
            {
                try
                {
                    return new Range(Convert.ToDouble(r[0]), Convert.ToDouble(r[1]));
                }
                catch
                {
                    throw new Exception("Incorrect format");
                }
            }
            else
            {
                throw new Exception("Incorrect format");
            }
        }

        #endregion

    }
}
