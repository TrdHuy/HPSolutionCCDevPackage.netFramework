using HPSolutionCCDevPackage.netFramework.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms.Integration;
using System.Windows.Controls;
using System.Windows.Media;
using System.Collections.Specialized;
using System.Runtime.InteropServices;

namespace HPSolutionCCDevPackage.netFramework
{
    /// <summary>
    /// AmunTextBox use textbox from System.Windows.Form
    /// with purpose to set the tab stop postion on text
    /// </summary>
    public class AmunTextBox : Control
    {
        private static Logger logger = new Logger("AmunTextBox");

        public AmunTextBox()
        {
            DefaultStyleKey = typeof(AmunTextBox);
            this.IsTabStop = true;
            IsUsingAmunClippingBorder = false;
        }

        #region ConerRadius
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius",
                typeof(CornerRadius),
                typeof(AmunTextBox),
                new FrameworkPropertyMetadata(default(CornerRadius),
                                        FrameworkPropertyMetadataOptions.AffectsMeasure |
                                        FrameworkPropertyMetadataOptions.AffectsRender), null);

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        #endregion

        #region MultiLine
        public static readonly DependencyProperty MultiLineProperty =
            DependencyProperty.Register(
                    "MultiLine",
                    typeof(bool),
                    typeof(AmunTextBox),
                    new PropertyMetadata(false,
                        new PropertyChangedCallback(TextBoxWFElementChangedCallback)),
                    null);

        private static void TextBoxWFElementChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            logger.I("Amun's properties changed: from " + e.OldValue + " to " + e.NewValue);
            AmunTextBox ctrl = d as AmunTextBox;
            ctrl.SetupTextBoxWFProperties();
        }

        public bool MultiLine
        {
            get
            {
                return (bool)GetValue(MultiLineProperty);
            }
            set
            {
                SetValue(MultiLineProperty, value);
            }
        }
        #endregion

        #region IsReadOnly
        public static readonly DependencyProperty IsReadOnlyProperty =
            DependencyProperty.Register(
                    "IsReadOnly",
                    typeof(bool),
                    typeof(AmunTextBox),
                    new PropertyMetadata(false,
                        new PropertyChangedCallback(TextBoxWFElementChangedCallback)),
                    null);

        public bool IsReadOnly
        {
            get
            {
                return (bool)GetValue(IsReadOnlyProperty);
            }
            set
            {
                SetValue(IsReadOnlyProperty, value);
            }
        }
        #endregion

        #region Text
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(AmunTextBox),
                                                new PropertyMetadata("",
                                                    new PropertyChangedCallback(TextChangedCallback)));


        private static void TextChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            AmunTextBox ctrl = d as AmunTextBox;

            logger.I("Amun's Text changed: IsNeedUpdateTextBoxWFElement = " + ctrl.IsNeedUpdateTextBoxWFElement);

            if (ctrl.IsNeedUpdateTextBoxWFElement)
            {
                ctrl.UpdateText(e.NewValue as string);
            }
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        #endregion

        #region TabStopSource
        public static readonly DependencyProperty TabStopSourceProperty =
            DependencyProperty.Register("TabStopSource", typeof(IEnumerable<int>), typeof(AmunTextBox),
                                                new PropertyMetadata(default(IEnumerable<int>),
                                                    new PropertyChangedCallback(TabStopSourceCallback)));


        private static void TabStopSourceCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            logger.I("Amun's tab stop source changed!");
            AmunTextBox ctrl = d as AmunTextBox;
            IEnumerable<int> oldValue = (IEnumerable<int>)e.OldValue;
            IEnumerable<int> newValue = (IEnumerable<int>)e.NewValue;

            ctrl.OnTabStopSourceChanged(oldValue, newValue);
        }


        public IEnumerable<int> TabStopSource
        {
            get { return (IEnumerable<int>)GetValue(TabStopSourceProperty); }
            set { SetValue(TabStopSourceProperty, value); }
        }
        #endregion

        #region IsUsingAmunClippingBorder
        public static readonly DependencyProperty IsUsingAmunClippingBorderProperty =
            DependencyProperty.Register(
                    "IsUsingAmunClippingBorder",
                    typeof(bool),
                    typeof(AmunTextBox),
                    new FrameworkPropertyMetadata(
                            true,
                            FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender,
                            null),
                    null);

        // temporaly not supported yet
        private bool IsUsingAmunClippingBorder
        {
            get
            {
                return (bool)GetValue(IsUsingAmunClippingBorderProperty);
            }
            set
            {
                SetValue(IsUsingAmunClippingBorderProperty, value);
            }
        }
        #endregion

        private System.Windows.Forms.TextBox TextBoxWFElement { get; set; }
        private WindowsFormsHost WFHostElement { get; set; }
        private StreamGeometry GridContentStreamGeoElement { get; set; }
        private bool IsNeedUpdateTextBoxWFElement { get; set; } = true;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            WFHostElement = GetTemplateChild("WFHost") as WindowsFormsHost;
            GridContentStreamGeoElement = GetTemplateChild("GridContentStreamGeometry") as StreamGeometry;
            if (WFHostElement != null)
            {
                TextBoxWFElement = WFHostElement.Child as System.Windows.Forms.TextBox;
                TextBoxWFElement.TextChanged += TextBoxWFElement_TextChanged;
            }
            SetupTextBoxWFProperties();
            UpdateText(Text);

            this.SizeChanged += OnAmunTextBoxSizeChanged;
        }

        private void OnAmunTextBoxSizeChanged(object sender, SizeChangedEventArgs e)
        {
            logger.I("OnAmunTextBoxSizeChanged: from " + e.PreviousSize + " to " + e.NewSize);

            if (!e.Handled && GridContentStreamGeoElement != null && IsUsingAmunClippingBorder)
            {
                GridContentStreamGeoElement.Clear();
                Thickness borders = BorderThickness;

                Rect boundRect = new Rect(e.NewSize);
                //Rect innerRect = AIVUtil.HelperDeflateRect(boundRect, borders);

                using (StreamGeometryContext ctx = GridContentStreamGeoElement.Open())
                {
                    CornerRadius radii = CornerRadius;
                    Radii innerRadii = new Radii(radii, borders, false);
                    GenerateGeometry(ctx, boundRect, innerRadii);
                }
            }
        }

        private void TextBoxWFElement_TextChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.TextBox ctrl = sender as System.Windows.Forms.TextBox;
            IsNeedUpdateTextBoxWFElement = false;
            Text = ctrl.Text;
            IsNeedUpdateTextBoxWFElement = true;
        }

        private void SetupTextBoxWFProperties()
        {
            if (TextBoxWFElement != null)
            {
                TextBoxWFElement.ReadOnly = IsReadOnly;
                TextBoxWFElement.Multiline = MultiLine;
                TextBoxWFElement.WordWrap = false;
                TextBoxWFElement.BorderStyle = System.Windows.Forms.BorderStyle.None;
            }
        }

        private void UpdateText(string newValue)
        {
            if (TextBoxWFElement != null)
            {
                TextBoxWFElement.Text = newValue;
            }
        }

        private void OnTabStopSourceChanged(IEnumerable<int> oldValue, IEnumerable<int> newValue)
        {
            if (oldValue != null)
            {
                ((INotifyCollectionChanged)oldValue).CollectionChanged -= new NotifyCollectionChangedEventHandler(OnTabStopItemsChanged);
            }
            if (newValue != null)
            {
                ((INotifyCollectionChanged)newValue).CollectionChanged -= new NotifyCollectionChangedEventHandler(OnTabStopItemsChanged);
                ((INotifyCollectionChanged)newValue).CollectionChanged += new NotifyCollectionChangedEventHandler(OnTabStopItemsChanged);
            }

            if (TextBoxWFElement != null)
            {
                var arrayTab = TabStopSource.ToArray();
                ATBUtil.SetTextBoxTabs(TextBoxWFElement, arrayTab);
            }
        }

        private void OnTabStopItemsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            logger.I("Tab stop items collection changed!");
            if (TextBoxWFElement != null)
            {
                var arrayTab = TabStopSource.ToArray();
                ATBUtil.SetTextBoxTabs(TextBoxWFElement, arrayTab);
            }
        }

        private void GenerateGeometry(StreamGeometryContext ctx, Rect rect, Radii radii)
        {
            //
            //  compute the coordinates of the key points
            //
            //
            // L = left; R = right; T = top; B = bottom
            // SP = start point
            //
            //          LT    |    RT
            //    SP ---------|---------
            //        TL|     |     |TR
            //          |     |     |
            //          |     |     |
            //        BL|     |     |BR
            //          ------|------
            //          LB    |    RB
            //

            Point topLeft = new Point(radii.LeftTop, 0);
            Point topRight = new Point(rect.Width - radii.RightTop, 0);
            Point rightTop = new Point(rect.Width, radii.TopRight);
            Point rightBottom = new Point(rect.Width, rect.Height - radii.BottomRight);
            Point bottomRight = new Point(rect.Width - radii.RightBottom, rect.Height);
            Point bottomLeft = new Point(radii.LeftBottom, rect.Height);
            Point leftBottom = new Point(0, rect.Height - radii.BottomLeft);
            Point leftTop = new Point(0, radii.TopLeft);

            //
            //  check keypoints for overlap and resolve by partitioning radii according to
            //  the percentage of each one.  
            //

            //  top edge is handled here
            if (topLeft.X > topRight.X)
            {
                double v = (radii.LeftTop) / (radii.LeftTop + radii.RightTop) * rect.Width;
                topLeft.X = v;
                topRight.X = v;
            }

            //  right edge
            if (rightTop.Y > rightBottom.Y)
            {
                double v = (radii.TopRight) / (radii.TopRight + radii.BottomRight) * rect.Height;
                rightTop.Y = v;
                rightBottom.Y = v;
            }

            //  bottom edge
            if (bottomRight.X < bottomLeft.X)
            {
                double v = (radii.LeftBottom) / (radii.LeftBottom + radii.RightBottom) * rect.Width;
                bottomRight.X = v;
                bottomLeft.X = v;
            }

            // left edge
            if (leftBottom.Y < leftTop.Y)
            {
                double v = (radii.TopLeft) / (radii.TopLeft + radii.BottomLeft) * rect.Height;
                leftBottom.Y = v;
                leftTop.Y = v;
            }

            //
            //  add on offsets
            //

            Vector offset = new Vector(rect.TopLeft.X, rect.TopLeft.Y);
            topLeft += offset;
            topRight += offset;
            rightTop += offset;
            rightBottom += offset;
            bottomRight += offset;
            bottomLeft += offset;
            leftBottom += offset;
            leftTop += offset;

            //
            //  create the border geometry
            //
            ctx.BeginFigure(topLeft, true /* is filled */, true /* is closed */);

            // Top line
            ctx.LineTo(topRight, true /* is stroked */, false /* is smooth join */);

            // Upper-right corner
            double radiusX = rect.TopRight.X - topRight.X;
            double radiusY = rightTop.Y - rect.TopRight.Y;
            if (!ATBUtil.IsZero(radiusX)
                || !ATBUtil.IsZero(radiusY))
            {
                ctx.ArcTo(rightTop, new System.Windows.Size(radiusX, radiusY), 0, false, SweepDirection.Clockwise, true, false);
            }

            // Right line
            ctx.LineTo(rightBottom, true /* is stroked */, false /* is smooth join */);

            // Lower-right corner
            radiusX = rect.BottomRight.X - bottomRight.X;
            radiusY = rect.BottomRight.Y - rightBottom.Y;
            if (!ATBUtil.IsZero(radiusX)
                || !ATBUtil.IsZero(radiusY))
            {
                ctx.ArcTo(bottomRight, new System.Windows.Size(radiusX, radiusY), 0, false, SweepDirection.Clockwise, true, false);
            }

            // Bottom line
            ctx.LineTo(bottomLeft, true /* is stroked */, false /* is smooth join */);

            // Lower-left corner
            radiusX = bottomLeft.X - rect.BottomLeft.X;
            radiusY = rect.BottomLeft.Y - leftBottom.Y;
            if (!ATBUtil.IsZero(radiusX)
                || !ATBUtil.IsZero(radiusY))
            {
                ctx.ArcTo(leftBottom, new System.Windows.Size(radiusX, radiusY), 0, false, SweepDirection.Clockwise, true, false);
            }

            // Left line
            ctx.LineTo(leftTop, true /* is stroked */, false /* is smooth join */);

            // Upper-left corner
            radiusX = topLeft.X - rect.TopLeft.X;
            radiusY = leftTop.Y - rect.TopLeft.Y;
            if (!ATBUtil.IsZero(radiusX)
                || !ATBUtil.IsZero(radiusY))
            {
                ctx.ArcTo(topLeft, new System.Windows.Size(radiusX, radiusY), 0, false, SweepDirection.Clockwise, true, false);
            }

            logger.D("top left = " + topLeft + " top right = " + topRight);
            logger.D("right top = " + rightTop + " right bottom = " + rightBottom);
            logger.D("bottom right = " + bottomRight + " bottom left = " + bottomLeft);
            logger.D("left bottom = " + leftBottom + " left top = " + leftTop);
        }


        #region Private helper
        private static class ATBUtil
        {
            public static bool IsZero(double val)
            {
                return val == 0d;
            }

            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, Int32 wParam, int[] lParam);
            private const uint EM_SETTABSTOPS = 0xCB;

            public static void SetTextBoxTabs(System.Windows.Forms.TextBox txt, int[] tabs)
            {
                SendMessage(txt.Handle, EM_SETTABSTOPS, tabs.Length, tabs);
                logger.I("Message sent");
            }
        }

        private struct Radii
        {
            internal Radii(CornerRadius radii, Thickness borders, bool outer)
            {
                double left = 0.5 * borders.Left;
                double top = 0.5 * borders.Top;
                double right = 0.5 * borders.Right;
                double bottom = 0.5 * borders.Bottom;

                if (outer)
                {
                    if (ATBUtil.IsZero(radii.TopLeft))
                    {
                        LeftTop = TopLeft = 0.0;
                    }
                    else
                    {
                        LeftTop = radii.TopLeft + left;
                        TopLeft = radii.TopLeft + top;
                    }
                    if (ATBUtil.IsZero(radii.TopRight))
                    {
                        TopRight = RightTop = 0.0;
                    }
                    else
                    {
                        TopRight = radii.TopRight + top;
                        RightTop = radii.TopRight + right;
                    }
                    if (ATBUtil.IsZero(radii.BottomRight))
                    {
                        RightBottom = BottomRight = 0.0;
                    }
                    else
                    {
                        RightBottom = radii.BottomRight + right;
                        BottomRight = radii.BottomRight + bottom;
                    }
                    if (ATBUtil.IsZero(radii.BottomLeft))
                    {
                        BottomLeft = LeftBottom = 0.0;
                    }
                    else
                    {
                        BottomLeft = radii.BottomLeft + bottom;
                        LeftBottom = radii.BottomLeft + left;
                    }
                }
                else
                {
                    LeftTop = Math.Max(0.0, radii.TopLeft - left);
                    TopLeft = Math.Max(0.0, radii.TopLeft - top);
                    TopRight = Math.Max(0.0, radii.TopRight - top);
                    RightTop = Math.Max(0.0, radii.TopRight - right);
                    RightBottom = Math.Max(0.0, radii.BottomRight - right);
                    BottomRight = Math.Max(0.0, radii.BottomRight - bottom);
                    BottomLeft = Math.Max(0.0, radii.BottomLeft - bottom);
                    LeftBottom = Math.Max(0.0, radii.BottomLeft - left);
                }
            }

            internal double LeftTop;
            internal double TopLeft;
            internal double TopRight;
            internal double RightTop;
            internal double RightBottom;
            internal double BottomRight;
            internal double BottomLeft;
            internal double LeftBottom;
        }
        #endregion

    }
}
