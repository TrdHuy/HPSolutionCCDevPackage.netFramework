using System;
using System.Collections.Generic;
using System.Windows;
using System.IO;
using System.Linq;
using System.Runtime.Caching;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Diagnostics;
using System.Windows.Data;
using System.Windows.Markup.Primitives;
using HPSolutionCCDevPackage.netFramework.Utils;
using System.Reflection;

namespace HPSolutionCCDevPackage.netFramework
{
    public class AtumImageView : Control
    {
        private static Logger logger = new Logger("AtumImageView");

        public AtumImageView()
        {
            DefaultStyleKey = typeof(AtumImageView);
            IsUsingAtumClippingBorder = true;
            IsSupportAtumLocator = false;
            IsSupportAtumZoomer = false;
            SpiningTextRatio = 1d;
            IsSupportLoadingAnimation = true;
        }

        #region Public properties

        #region ConerRadius
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius",
                typeof(CornerRadius),
                typeof(AtumImageView),
                new FrameworkPropertyMetadata(default(CornerRadius),
                                        FrameworkPropertyMetadataOptions.AffectsMeasure |
                                        FrameworkPropertyMetadataOptions.AffectsRender), null);

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        #endregion

        #region ImageSource
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register(
                    "Source",
                    typeof(ImageSource),
                    typeof(AtumImageView),
                    new FrameworkPropertyMetadata(
                            null,
                            FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender,
                            new PropertyChangedCallback(OnSourceChanged),
                            null),
                    null);

        private static void OnSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            AtumImageView ctrl = d as AtumImageView;

            var x = new ImageSourceChangedEventArgs(AtumImageView.ImageSourceChangedEvent,
                e.OldValue as ImageSource,
                e.NewValue as ImageSource);

            ctrl.OnAtumImageSourceChanged(x);
        }


        public ImageSource Source
        {
            get
            {
                return (ImageSource)GetValue(SourceProperty);
            }
            set
            {
                SetValue(SourceProperty, value);
            }
        }
        #endregion

        #region Stretch

        public static readonly DependencyProperty StretchProperty =
                Viewbox.StretchProperty.AddOwner(typeof(AtumImageView));

        public Stretch Stretch
        {
            get { return (Stretch)GetValue(StretchProperty); }
            set { SetValue(StretchProperty, value); }
        }

        #endregion

        #region StretchDirection
        public static readonly DependencyProperty StretchDirectionProperty =
               Viewbox.StretchDirectionProperty.AddOwner(typeof(AtumImageView));

        public StretchDirection StretchDirection
        {
            get { return (StretchDirection)GetValue(StretchDirectionProperty); }
            set { SetValue(StretchDirectionProperty, value); }
        }
        #endregion

        #region ImageFailed
        public static readonly RoutedEvent ImageFailedEvent =
            EventManager.RegisterRoutedEvent(
                            "ImageFailed",
                            RoutingStrategy.Bubble,
                            typeof(EventHandler<ExceptionRoutedEventArgs>),
                            typeof(AtumImageView));

        public event EventHandler<ExceptionRoutedEventArgs> ImageFailed
        {
            add { AddHandler(ImageFailedEvent, value); }
            remove { RemoveHandler(ImageFailedEvent, value); }
        }
        #endregion

        #region AtumImageSourceRenderedEvent
        public static readonly RoutedEvent AtumImageSourceRenderedEvent =
            EventManager.RegisterRoutedEvent("ImageSourceRendered", RoutingStrategy.Direct,
                         typeof(AtumImageSourceRenderedHandler), typeof(AtumImageView));

        public event AtumImageSourceRenderedHandler ImageSourceRendered
        {
            add
            {
                AddHandler(AtumImageSourceRenderedEvent, value);
            }
            remove
            {
                RemoveHandler(AtumImageSourceRenderedEvent, value);
            }
        }
        #endregion

        #region TempUserDataChangedEvent
        public static readonly RoutedEvent TempUserDataChangedEvent =
            EventManager.RegisterRoutedEvent("TempUserDataChanged", RoutingStrategy.Direct,
                         typeof(TempUserDataChangedHandler), typeof(AtumImageView));

        public event TempUserDataChangedHandler TempUserDataChanged
        {
            add
            {
                AddHandler(TempUserDataChangedEvent, value);
            }
            remove
            {
                RemoveHandler(TempUserDataChangedEvent, value);
            }
        }
        #endregion

        #region ImageSourceChangedEvent
        public static readonly RoutedEvent ImageSourceChangedEvent =
            EventManager.RegisterRoutedEvent("ImageSourceChanged", RoutingStrategy.Direct,
                         typeof(ImageSourceChangedHandler), typeof(AtumImageView));

        public event ImageSourceChangedHandler ImageSourceChanged
        {
            add
            {
                AddHandler(ImageSourceChangedEvent, value);
            }
            remove
            {
                RemoveHandler(ImageSourceChangedEvent, value);
            }
        }
        #endregion

        #region PreviewAsyncSourceUpdated
        public static readonly RoutedEvent PreviewAsyncSourceUpdatedEvent =
            EventManager.RegisterRoutedEvent("PreviewAsyncSourceUpdated", RoutingStrategy.Direct,
                         typeof(PreviewAsyncSourceUpdatedHandler), typeof(AtumImageView));

        public event PreviewAsyncSourceUpdatedHandler PreviewAsyncSourceUpdated
        {
            add
            {
                AddHandler(PreviewAsyncSourceUpdatedEvent, value);
            }
            remove
            {
                RemoveHandler(PreviewAsyncSourceUpdatedEvent, value);
            }
        }
        #endregion

        #region ImagePath
        public static readonly DependencyProperty ImagePathProperty =
            DependencyProperty.Register(
                    "ImagePath",
                    typeof(string),
                    typeof(AtumImageView),
                    new FrameworkPropertyMetadata(
                            null,
                            FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender,
                            new PropertyChangedCallback(OnImagePathChanged),
                            null),
                    null);

        private static void OnImagePathChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            AtumImageView ctrl = d as AtumImageView;

            ctrl.UpdateAtumSource(ctrl, e.NewValue as string, e.OldValue as string);
        }

        /// <summary>
        /// Use image path to load image source async
        /// </summary>
        public string ImagePath
        {
            get
            {
                return (string)GetValue(ImagePathProperty);
            }
            set
            {
                SetValue(ImagePathProperty, value);
            }
        }
        #endregion

        #region IsUsingAtumClippingBorder
        public static readonly DependencyProperty IsUsingAtumClippingBorderProperty =
            DependencyProperty.Register(
                    "IsUsingAtumClippingBorder",
                    typeof(bool),
                    typeof(AtumImageView),
                    new FrameworkPropertyMetadata(
                            true,
                            FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender,
                            null),
                    null);

        public bool IsUsingAtumClippingBorder
        {
            get
            {
                return (bool)GetValue(IsUsingAtumClippingBorderProperty);
            }
            set
            {
                SetValue(IsUsingAtumClippingBorderProperty, value);
            }
        }
        #endregion

        #region IsSupportAtumLocator
        public static readonly DependencyProperty IsSupportAtumLocatorProperty =
            DependencyProperty.Register(
                    "IsSupportAtumLocator",
                    typeof(bool),
                    typeof(AtumImageView),
                    new PropertyMetadata(
                            false,
                            new PropertyChangedCallback(IsSupportAtumLocatorChangedCallback)),
                    null);

        private static void IsSupportAtumLocatorChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            AtumImageView ctrl = d as AtumImageView;
            ctrl.ValidatePropertyFeasibility();
        }

        public bool IsSupportAtumLocator
        {
            get
            {
                return (bool)GetValue(IsSupportAtumLocatorProperty);
            }
            set
            {
                SetValue(IsSupportAtumLocatorProperty, value);
            }
        }
        #endregion

        #region IsSupportAtumZoomer
        public static readonly DependencyProperty IsSupportAtumZoomerProperty =
            DependencyProperty.Register(
                    "IsSupportAtumZoomer",
                    typeof(bool),
                    typeof(AtumImageView),
                    new PropertyMetadata(
                            false,
                            new PropertyChangedCallback(IsSupportAtumZoomerChangedCallback)),
                    null);

        private static void IsSupportAtumZoomerChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            AtumImageView ctrl = d as AtumImageView;
            ctrl.ValidatePropertyFeasibility();
        }

        public bool IsSupportAtumZoomer
        {
            get
            {
                return (bool)GetValue(IsSupportAtumZoomerProperty);
            }
            set
            {
                SetValue(IsSupportAtumZoomerProperty, value);
            }
        }
        #endregion

        #region IsSupportImageLocateHelper
        public static readonly DependencyProperty IsSupportImageLocateHelperProperty =
            DependencyProperty.Register(
                    "IsSupportImageLocateHelper",
                    typeof(bool),
                    typeof(AtumImageView),
                    new PropertyMetadata(
                            false,
                            new PropertyChangedCallback(IsSupportImageLocateHelperChangedCallback)),
                    null);

        private static void IsSupportImageLocateHelperChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            AtumImageView ctrl = d as AtumImageView;
            ctrl.ValidatePropertyFeasibility();
        }

        /// <summary>
        /// Use image locate helper window to get image location data
        /// </summary>
        public bool IsSupportImageLocateHelper
        {
            get
            {
                return (bool)GetValue(IsSupportImageLocateHelperProperty);
            }
            set
            {
                SetValue(IsSupportImageLocateHelperProperty, value);
            }
        }
        #endregion

        #region ImageFileLoadingDelayTime
        public static readonly DependencyProperty ImageFileLoadingDelayTimeProperty =
           DependencyProperty.Register("ImageFileLoadingDelayTime", typeof(long), typeof(AtumImageView),
             new PropertyMetadata(default(long)));

        /// <summary>
        /// Delay a period of specifictime when reading a new image from file
        /// </summary>
        public long ImageFileLoadingDelayTime
        {
            get { return (long)GetValue(ImageFileLoadingDelayTimeProperty); }
            set { SetValue(ImageFileLoadingDelayTimeProperty, value); }
        }
        #endregion

        #region AtumUserData
        public static readonly DependencyProperty AtumUserDataProperty =
            DependencyProperty.Register(
                    "AtumImageData",
                    typeof(AtumUserData),
                    typeof(AtumImageView),
                    new FrameworkPropertyMetadata(
                            default(AtumUserData),
                            FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender,
                            new PropertyChangedCallback(AtumUserDataChangedCallback)),
                    null);

        private static void AtumUserDataChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            AtumImageView ctrl = d as AtumImageView;
        }

        public AtumUserData AtumImageData
        {
            get
            {
                return (AtumUserData)GetValue(AtumUserDataProperty);
            }
            set
            {
                SetValue(AtumUserDataProperty, value);
            }
        }
        #endregion

        #region IsEnableAtumTransformer
        public static readonly DependencyProperty IsEnableAtumTransformerProperty =
            DependencyProperty.Register(
                    "IsEnableAtumTransformer",
                    typeof(bool),
                    typeof(AtumImageView),
                    new PropertyMetadata(
                            false,
                            new PropertyChangedCallback(IsEnableAtumTransformerChangedCallback)),
                    null);

        private static void IsEnableAtumTransformerChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            AtumImageView ctrl = d as AtumImageView;
            ctrl.UpdateAtumTransformerFunction();
        }

        public bool IsEnableAtumTransformer
        {
            get
            {
                return (bool)GetValue(IsEnableAtumTransformerProperty);
            }
            set
            {
                SetValue(IsEnableAtumTransformerProperty, value);
            }
        }
        #endregion

        #region IsBusy
        public static readonly DependencyProperty IsBusyProperty =
            DependencyProperty.Register(
                    "IsBusy",
                    typeof(bool),
                    typeof(AtumImageView),
                    new PropertyMetadata(
                            false,
                            new PropertyChangedCallback(IsBusyChangedCallback)),
                    null);

        private static void IsBusyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            AtumImageView ctrl = d as AtumImageView;
        }

        public bool IsBusy
        {
            get
            {
                return (bool)GetValue(IsBusyProperty);
            }
            private set
            {
                SetValue(IsBusyProperty, value);
            }
        }
        #endregion

        #region SpiningTextRatio
        public static readonly DependencyProperty SpiningTextRatioProperty =
           DependencyProperty.Register("SpiningTextRatio", typeof(double), typeof(AtumImageView),
             new PropertyMetadata(default(double)));

        public double SpiningTextRatio
        {
            get { return (double)GetValue(SpiningTextRatioProperty); }
            set { SetValue(SpiningTextRatioProperty, value); }
        }
        #endregion

        #region IsSupportLoadingAnimation
        public static readonly DependencyProperty IsSupportLoadingAnimationProperty =
            DependencyProperty.Register(
                    "IsSupportLoadingAnimation",
                    typeof(bool),
                    typeof(AtumImageView),
                    new PropertyMetadata(true),
                    null);

        public bool IsSupportLoadingAnimation
        {
            get
            {
                return (bool)GetValue(IsSupportLoadingAnimationProperty);
            }
            set
            {
                SetValue(IsSupportLoadingAnimationProperty, value);
            }
        }
        #endregion

        #endregion



        //------------------------------------------



        #region Private properties
        private AtumLocator atumLocator { get; set; }
        private AtumZoomer atumZoomer { get; set; }
        private TransformHelperWindow locateHelperWindow { get; set; }
        private int sourceChangedCounter { get; set; } = 0;
        private bool isSourceUpdatedByImagePath { get; set; } = false;

        private AtumImage AtumImageElement { get; set; }
        private Viewbox SpinningViewBoxElement { get; set; }
        private Grid AtumContentGridElement { get; set; }
        private Grid AtumMainGridElement { get; set; }
        private StreamGeometry GridContentStreamGeoElement { get; set; }

        internal AtumUserData tempUserData { get; set; } = new AtumUserData();

        #endregion



        //------------------------------------------



        #region Public methods

        public void ResetAtumTransform()
        {
            AtumImageData = null;
            tempUserData = null;

            if (IsSupportAtumLocator && IsEnableAtumTransformer)
            {
                atumLocator.ResetLocator();
            }
            if (IsSupportAtumZoomer && IsEnableAtumTransformer)
            {
                atumZoomer.ResetZoomer();
            }
        }

        public AtumUserData GetTempUserData()
        {
            return tempUserData;
        }

        public void ShowWindowHelper()
        {
            if (locateHelperWindow != null && IsSupportImageLocateHelper)
            {
                locateHelperWindow.Show();
            }
        }
        #endregion



        //------------------------------------------



        #region Override field
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            // Validate source and image path property
            if (ReadLocalValue(SourceProperty) != DependencyProperty.UnsetValue &&
                ReadLocalValue(ImagePathProperty) != DependencyProperty.UnsetValue &&
                !isSourceUpdatedByImagePath)
            {
                logger.E("InvalidOperationException: Can not use both Source and Imagepath at the same time");
                throw new InvalidOperationException("Can not use both Source and Imagepath at the same time");
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            try
            {
                AtumImageElement = GetTemplateChild("ImagePresenter") as AtumImage;
                AtumContentGridElement = GetTemplateChild("AtumContentGrid") as Grid;
                AtumMainGridElement = GetTemplateChild("AtumMainGrid") as Grid;
                GridContentStreamGeoElement = GetTemplateChild("GridContentStreamGeometry") as StreamGeometry;
                SpinningViewBoxElement = GetTemplateChild("SpiningText") as Viewbox;

                AtumMainGridElement.SizeChanged += OnAtumMainGridSizeChanged;
                AtumImageElement.ImageSourceRendered += OnAtumImageSourceRendered;
                SpinningViewBoxElement.IsVisibleChanged += OnSpinningViewBoxVisibleChanged;
                ValidatePropertyFeasibility();
                SetUpFeature();
            }
            catch (Exception e)
            {
                logger.E("Exception: " + e.Message);
                SetUpAlternativeFeature();
            }
        }

        private void OnSpinningViewBoxVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            // Measure spining view box size
            if (SpinningViewBoxElement != null &&
                SpinningViewBoxElement.Visibility == Visibility.Visible &&
                AtumMainGridElement != null)
            {
                var x = AtumMainGridElement.RenderSize;
                SpiningTextRatio = SpiningTextRatio > 1 ? 1 : SpiningTextRatio < 0 ? 0 : SpiningTextRatio;
                var mainSize = x.Height < x.Width ? x.Height : x.Width;
                SpinningViewBoxElement.Height = mainSize * SpiningTextRatio;
                SpinningViewBoxElement.Width = mainSize * SpiningTextRatio;
            }
        }

        private void OnAtumImageSourceRendered(object sender, AtumImageSourceRenderedEventArgs e)
        {
            if (AtumImageElement != null && AtumImageElement.Source != null)
            {
                OnImageSourceRendered(e.DrawingContext);
            }
        }

        protected virtual void OnAtumMainGridSizeChanged(object sender, SizeChangedEventArgs e)
        {
            logger.I("OnMainGridSizeChanged: from " + e.PreviousSize + " to " + e.NewSize);

            if (!e.Handled && GridContentStreamGeoElement != null && IsUsingAtumClippingBorder)
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

            // Measure spining view box size
            if (SpinningViewBoxElement != null &&
                SpinningViewBoxElement.Visibility == Visibility.Visible &&
                AtumMainGridElement != null)
            {
                var x = AtumMainGridElement.RenderSize;
                SpiningTextRatio = SpiningTextRatio > 1 ? 1 : SpiningTextRatio < 0 ? 0 : SpiningTextRatio;
                var mainSize = x.Height < x.Width ? x.Height : x.Width;
                SpinningViewBoxElement.Height = mainSize * SpiningTextRatio;
                SpinningViewBoxElement.Width = mainSize * SpiningTextRatio;
            }

        }

        protected virtual void OnAtumImageSourceChanged(ImageSourceChangedEventArgs e)
        {
            logger.I("OnImageSourceChanged: New source: " + e.NewValue as string);

            // Increase source change counter everytime source change
            sourceChangedCounter++;

            // Clear atum image data
            // Avoid clear atum data when the first time source change
            if (sourceChangedCounter > 1)
            {
                AtumImageData = null;
                tempUserData = null;
            }

            RaiseEvent(e);
        }

        protected virtual void OnImageSourceRendered(DrawingContext drawingContext)
        {
            logger.I("[OnImageSourceRendered]");

            AtumImageSourceRenderedEventArgs e = new AtumImageSourceRenderedEventArgs(
                 AtumImageView.AtumImageSourceRenderedEvent,
                 drawingContext,
                 Source);

            RaiseEvent(e);
        }

        protected virtual void OnPreviewAsyncSourceUpdated(PreviewAsyncSourceUpdatedEventArgs preSourceArg)
        {
            logger.I("[OnPreviewAsyncSourceUpdated]");

            RaiseEvent(preSourceArg);
        }
        #endregion



        //------------------------------------------



        #region Private methods
        private void SetUpFeature()
        {
            if (IsSupportAtumLocator)
            {
                atumLocator = new AtumLocator(
                    this,
                AtumMainGridElement,
                AtumContentGridElement,
                AtumImageElement);

                if (IsSupportAtumZoomer)
                {
                    atumZoomer = new AtumZoomer(this, AtumImageElement, AtumContentGridElement, AtumMainGridElement);
                }

                if (IsSupportImageLocateHelper)
                {
                    locateHelperWindow = new TransformHelperWindow(this);
                }

                UpdateAtumTransformerFunction();
            }
            else
            {
                SetUpAlternativeFeature();
            }

        }


        private void SetUpAlternativeFeature()
        {
            if (AtumImageElement != null)
            {
                var bindingVertical = new Binding("VerticalContentAlignment")
                {
                    Mode = BindingMode.TwoWay,
                    RelativeSource = new RelativeSource()
                    {
                        Mode = RelativeSourceMode.TemplatedParent
                    }
                };

                var horizontalVertical = new Binding("HorizontalContentAlignment")
                {
                    Mode = BindingMode.TwoWay,
                    RelativeSource = new RelativeSource()
                    {
                        Mode = RelativeSourceMode.TemplatedParent
                    }
                };

                AtumImageElement.SetBinding(VerticalAlignmentProperty, bindingVertical);
                AtumImageElement.SetBinding(HorizontalAlignmentProperty, horizontalVertical);
            }

        }



        private void UpdateAtumTransformerFunction()
        {
            if (AtumImageElement != null && IsInitialized)
            {
                if (atumLocator != null && IsEnableAtumTransformer)
                {
                    if (!IsSupportImageLocateHelper)
                    {
                        AtumImageElement.MouseLeftButtonDown -= atumLocator.AtumImagePrsenter_MouseLeftDown;
                        AtumImageElement.MouseLeftButtonUp -= atumLocator.AtumImagePrsenter_MouseLeftUp;
                        AtumImageElement.MouseMove -= atumLocator.AtumImagePresenter_MouseMove;
                        AtumImageElement.MouseLeave -= atumLocator.AtumImagePrsenter_MouseLeave;

                        AtumImageElement.MouseLeftButtonDown += atumLocator.AtumImagePrsenter_MouseLeftDown;
                        AtumImageElement.MouseLeftButtonUp += atumLocator.AtumImagePrsenter_MouseLeftUp;
                        AtumImageElement.MouseMove += atumLocator.AtumImagePresenter_MouseMove;
                        AtumImageElement.MouseLeave += atumLocator.AtumImagePrsenter_MouseLeave;
                    }
                    else
                    {
                        this.MouseLeftButtonDown -= locateHelperWindow.AtumImageView_MouseLeftButtonDown;
                        this.MouseEnter -= locateHelperWindow.AtumImageView_MouseEnter;

                        this.MouseLeftButtonDown += locateHelperWindow.AtumImageView_MouseLeftButtonDown;
                        this.MouseEnter += locateHelperWindow.AtumImageView_MouseEnter;
                    }
                }

                if (atumZoomer != null && IsEnableAtumTransformer)
                {
                    if (!IsSupportImageLocateHelper)
                    {
                        AtumImageElement.MouseWheel -= atumZoomer.AtumImagePrsenter_MouseWheel;
                        AtumImageElement.MouseWheel += atumZoomer.AtumImagePrsenter_MouseWheel;
                    }
                }

            }

        }

        private void ValidatePropertyFeasibility()
        {
            if (!IsInitialized)
            {
                return;
            }

            if (AtumImageData != null)
            {
                if (!IsSupportAtumLocator)
                {
                    logger.E("AtumImageData must be used with AtumLocator or AtumZoomer");
                    throw new InvalidOperationException("AtumImageData must be used with AtumLocator or AtumZoomer");
                }
            }

            if (IsSupportAtumLocator)
            {
                if (Stretch != Stretch.UniformToFill)
                {
                    logger.E("IsSupportAtumLocator must be used with stretch UniformToFill");
                    throw new InvalidOperationException("IsSupportAtumLocator must be used with stretch UniformToFill");
                }

                if (!IsUsingAtumClippingBorder)
                {
                    logger.E("IsSupportAtumLocator must be used with Atum Clipping Border");
                    throw new InvalidOperationException("IsSupportAtumLocator must be used with Atum Clipping Border");
                }

            }

            if (IsSupportAtumZoomer)
            {
                if (!IsSupportAtumLocator)
                {
                    logger.E("AtumZoomer must be used with Atum Locator");
                    throw new InvalidOperationException("AtumZoomer must be used with Atum Locator");
                }
            }

            if (IsSupportImageLocateHelper)
            {
                if (!IsSupportAtumLocator)
                {
                    logger.E("Image Locate Helper must be used with Atum Locator");
                    throw new InvalidOperationException("Image Locate Helper must be used with Atum Locator");
                }
            }

        }

        private void UpdateStates(bool useTransitions, bool isBusy)
        {
            logger.I("[AtumImageView] State change: busy=" + isBusy);
            IsBusy = isBusy;
        }

        private async void UpdateAtumSource(object sender, string newImagePath, string oldImagePath)
        {
            logger.I("[UpdateAtumSource] newImagePath=" + newImagePath + "oldImagePath=" + oldImagePath);

            Task<ImageSource> getSource = GetImageSource(newImagePath);

            var watch = Stopwatch.StartNew();

            var preSourceArg = new PreviewAsyncSourceUpdatedEventArgs(
                       AtumImageView.PreviewAsyncSourceUpdatedEvent,
                       oldImagePath,
                       newImagePath);

            OnPreviewAsyncSourceUpdated(preSourceArg);

            //Preview async source update
            if (!preSourceArg.Handled)
            {
                bool isBusy = true;

                // Update the visual state
                UpdateStates(true, isBusy);

                var newSource = await getSource;

                //Update source
                Source = newSource;

                var rest = ImageFileLoadingDelayTime - watch.ElapsedMilliseconds;

                if (rest > 0 && IsSupportLoadingAnimation)
                {
                    await Task.Delay(Convert.ToInt32(rest));
                }

                // Update the flag
                isSourceUpdatedByImagePath = true;

                // Update the visual state
                isBusy = false;
                UpdateStates(true, isBusy);

            }
            else
            {
                watch.Stop();
            }

        }
        private async Task<ImageSource> GetImageSource(string imagePath)
        {
            // Delay for const of time to avoid blocking UI
            await Task.Delay(300);

            var x = AIVUtil.ToImageSource(AIVUtil.GetBitmapFromName(imagePath));

            return x;
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
            if (!AIVUtil.IsZero(radiusX)
                || !AIVUtil.IsZero(radiusY))
            {
                ctx.ArcTo(rightTop, new System.Windows.Size(radiusX, radiusY), 0, false, SweepDirection.Clockwise, true, false);
            }

            // Right line
            ctx.LineTo(rightBottom, true /* is stroked */, false /* is smooth join */);

            // Lower-right corner
            radiusX = rect.BottomRight.X - bottomRight.X;
            radiusY = rect.BottomRight.Y - rightBottom.Y;
            if (!AIVUtil.IsZero(radiusX)
                || !AIVUtil.IsZero(radiusY))
            {
                ctx.ArcTo(bottomRight, new System.Windows.Size(radiusX, radiusY), 0, false, SweepDirection.Clockwise, true, false);
            }

            // Bottom line
            ctx.LineTo(bottomLeft, true /* is stroked */, false /* is smooth join */);

            // Lower-left corner
            radiusX = bottomLeft.X - rect.BottomLeft.X;
            radiusY = rect.BottomLeft.Y - leftBottom.Y;
            if (!AIVUtil.IsZero(radiusX)
                || !AIVUtil.IsZero(radiusY))
            {
                ctx.ArcTo(leftBottom, new System.Windows.Size(radiusX, radiusY), 0, false, SweepDirection.Clockwise, true, false);
            }

            // Left line
            ctx.LineTo(leftTop, true /* is stroked */, false /* is smooth join */);

            // Upper-left corner
            radiusX = topLeft.X - rect.TopLeft.X;
            radiusY = leftTop.Y - rect.TopLeft.Y;
            if (!AIVUtil.IsZero(radiusX)
                || !AIVUtil.IsZero(radiusY))
            {
                ctx.ArcTo(topLeft, new System.Windows.Size(radiusX, radiusY), 0, false, SweepDirection.Clockwise, true, false);
            }
        }

        #endregion



        //------------------------------------------



        #region Private Helper
        private static class AIVUtil
        {
            public static bool IsZero(double val)
            {
                return val == 0d;
            }

            public static Rect HelperDeflateRect(Rect rt, Thickness thick)
            {
                return new Rect(rt.Left + thick.Left,
                                rt.Top + thick.Top,
                                Math.Max(0.0, rt.Width - thick.Left - thick.Right),
                                Math.Max(0.0, rt.Height - thick.Top - thick.Bottom));
            }

            public static System.Drawing.Bitmap GetBitmapFromName(string imagePath)
            {
                try
                {
                    if (File.Exists(imagePath))
                    {
                        using (var origin = System.Drawing.Image.FromFile(imagePath))
                        {
                            System.Drawing.Bitmap bit = new System.Drawing.Bitmap(origin);
                            origin.Dispose();
                            return bit;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
                catch
                {
                    return null;
                }
            }

            [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
            [return: MarshalAs(UnmanagedType.Bool)]
            private static extern bool DeleteObject([In] IntPtr hObject);
            public static ImageSource ToImageSource(System.Drawing.Bitmap bitmap)
            {
                if (bitmap == null)
                {
                    return null;
                }
                var handle = bitmap.GetHbitmap();
                try
                {
                    return Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, Int32Rect.Empty,
                        BitmapSizeOptions.FromEmptyOptions());
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    DeleteObject(handle);
                }
            }
        }
        public static class DependencyObjectHelper
        {
            public static List<DependencyProperty> GetDependencyProperties(Object element)
            {
                List<DependencyProperty> properties = new List<DependencyProperty>();
                MarkupObject markupObject = MarkupWriter.GetMarkupObjectFor(element);
                if (markupObject != null)
                {
                    foreach (MarkupProperty mp in markupObject.Properties)
                    {
                        if (mp.DependencyProperty != null)
                        {
                            properties.Add(mp.DependencyProperty);
                        }
                    }
                }

                return properties;
            }

            public static List<DependencyProperty> GetAttachedProperties(Object element)
            {
                List<DependencyProperty> attachedProperties = new List<DependencyProperty>();
                MarkupObject markupObject = MarkupWriter.GetMarkupObjectFor(element);
                if (markupObject != null)
                {
                    foreach (MarkupProperty mp in markupObject.Properties)
                    {
                        if (mp.IsAttached)
                        {
                            attachedProperties.Add(mp.DependencyProperty);
                        }
                    }
                }

                return attachedProperties;
            }

            public static object CloneProperties(object obj)
            {
                if (obj != null)
                {
                    var listProperties = obj.GetType().GetProperties();
                    var x = Activator.CreateInstance(obj.GetType());
                    foreach (PropertyInfo pi in listProperties)
                    {
                        x.GetType().GetProperty(pi.Name).SetValue(x, pi.GetValue(obj));
                    }
                    return x;
                }
                return null;
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
                    if (AIVUtil.IsZero(radii.TopLeft))
                    {
                        LeftTop = TopLeft = 0.0;
                    }
                    else
                    {
                        LeftTop = radii.TopLeft + left;
                        TopLeft = radii.TopLeft + top;
                    }
                    if (AIVUtil.IsZero(radii.TopRight))
                    {
                        TopRight = RightTop = 0.0;
                    }
                    else
                    {
                        TopRight = radii.TopRight + top;
                        RightTop = radii.TopRight + right;
                    }
                    if (AIVUtil.IsZero(radii.BottomRight))
                    {
                        RightBottom = BottomRight = 0.0;
                    }
                    else
                    {
                        RightBottom = radii.BottomRight + right;
                        BottomRight = radii.BottomRight + bottom;
                    }
                    if (AIVUtil.IsZero(radii.BottomLeft))
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
        private class AtumLocator
        {
            private Grid frame { get; set; }
            private Grid contentParent { get; set; }
            private AtumImage imagePresenter { get; set; }
            private AtumImageView border { get; set; }
            private Thickness initMargin { get; set; }

            private double minLeft { get; set; }
            private double minTop { get; set; }
            private double maxLeft { get; set; }
            private double maxTop { get; set; }
            private Size imageSize { get; set; }
            private Size minSize { get; set; }

            public bool CanChangeImageLocation { get; private set; }
            private Point lastPointPressed { get; set; }
            private bool isNeedSetInitMargin { get; set; }

            public AtumLocator(AtumImageView border, Grid frame, Grid contentParent, AtumImage imagePresenter)
            {
                if (border == null || frame == null || imagePresenter == null || contentParent == null)
                {
                    logger.E("Can not set up transform function, deal to null exception!");
                    throw new InvalidOperationException("Can not set up transform function, deal to null exception!");
                }
                this.frame = frame;
                this.imagePresenter = imagePresenter;
                this.contentParent = contentParent;
                this.border = border;

                imagePresenter.SizeChanged -= atumImage_SizeChanged;
                imagePresenter.SizeChanged += atumImage_SizeChanged;

                imagePresenter.ImageSourceRendered -= atumImage_SourceRendered;
                imagePresenter.ImageSourceRendered += atumImage_SourceRendered;

                border.ImageSourceChanged -= border_ImageSourceChanged;
                border.ImageSourceChanged += border_ImageSourceChanged;

                frame.SizeChanged -= frame_SizeChanged;
                frame.SizeChanged += frame_SizeChanged;
            }

            private void frame_SizeChanged(object sender, SizeChangedEventArgs e)
            {
                UpdateMinSize();

                if (CanChangeImageLocation)
                {
                    var newLeft = imagePresenter.Margin.Left;
                    var newTop = imagePresenter.Margin.Top;

                    if (!e.PreviousSize.IsZero())
                    {
                        var withRatioChange = e.NewSize.Width / e.PreviousSize.Width;
                        var heightRatioChange = e.NewSize.Height / e.PreviousSize.Height;
                        newLeft *= withRatioChange;
                        newTop *= heightRatioChange;
                    }

                    UpdateImageLocation(new Thickness(newLeft, newTop, 0.0, 0.0));

                    logger.V(":image location: new left = " + newLeft + " new top = " + newTop);
                }
            }

            // This was called whenever image source is changed
            // and the firstime source is rendered
            private void InitalizeAlignment()
            {
                var data = border.AtumImageData;
                if (data != null)
                {
                    var frameWidthRatio = frame.RenderSize.Width / data.FrameSize.Width;
                    var frameHeightRatio = frame.RenderSize.Height / data.FrameSize.Height;

                    var lastLeft = data.AtumImageMargin.Left;
                    var lastTop = data.AtumImageMargin.Top;

                    var initLeft = lastLeft * frameWidthRatio;
                    var initTop = lastTop * frameHeightRatio;

                    UpdateLocatorBoundary(true);

                    if (!double.IsNaN(initLeft) && !double.IsNaN(initTop))
                    {
                        initMargin = new Thickness(initLeft, initTop, 0.0, 0.0);
                    }
                    else
                    {
                        initMargin = new Thickness(0.0);
                    }
                }
                else
                {
                    UpdateLocatorBoundary(false);

                    double left = 0.0;
                    double top = 0.0;
                    switch (contentParent.VerticalAlignment)
                    {

                        case VerticalAlignment.Bottom:
                            top = (frame.ActualHeight - minSize.Height);
                            break;
                        case VerticalAlignment.Center:
                            top = (frame.ActualHeight - minSize.Height) / 2;
                            break;
                        case VerticalAlignment.Top:
                        case VerticalAlignment.Stretch:
                            top = 0.0d;
                            break;
                    }
                    switch (contentParent.HorizontalAlignment)
                    {
                        case HorizontalAlignment.Right:
                            left = (frame.ActualWidth - minSize.Width);
                            break;
                        case HorizontalAlignment.Center:
                            left = (frame.ActualWidth - minSize.Width) / 2;
                            break;
                        case HorizontalAlignment.Left:
                        case HorizontalAlignment.Stretch:
                            left = 0.0d;
                            break;
                    }
                    initMargin = new Thickness(left, top, 0.0, 0.0);

                    logger.V(":init margin " + initMargin.ToString());
                }

            }

            private void border_ImageSourceChanged(object sender, ImageSourceChangedEventArgs e)
            {
                isNeedSetInitMargin = true;
                if (e.NewValue != null)
                {
                    Size imgSize = new Size(e.NewValue.Width, e.NewValue.Height);
                    imageSize = imgSize;
                    UpdateMinSize();
                }
            }

            // Call this when:
            // # frame size changed
            // # image size changed
            private void UpdateMinSize()
            {
                Size fixedSize = frame.RenderSize;

                double heightRatio = imageSize.Height / fixedSize.Height;
                double widthRatio = imageSize.Width / fixedSize.Width;
                double uniformRatio = heightRatio < widthRatio ? heightRatio : widthRatio;
                minSize = new Size(imageSize.Width / uniformRatio, imageSize.Height / uniformRatio);
            }

            private void atumImage_SourceRendered(object sender, AtumImageSourceRenderedEventArgs e)
            {
                var imgSize = new Size(e.NewValue.Width, e.NewValue.Height);

                // Update origin image size and minimum image size
                if (imageSize.IsDifferent(imgSize))
                {
                    imageSize = imgSize;

                    UpdateMinSize();
                }

                UpdateLocatorBoundary(false);

                if (isNeedSetInitMargin || imagePresenter.IsFirstTimeSourceRendered)
                {
                    //Refresh alignment value
                    InitalizeAlignment();

                    UpdateImageLocation(initMargin);
                    isNeedSetInitMargin = false;
                }

            }

            //When zoom is change, only the size of atum image presenter is change
            //Hence need to update the image location base on the ratio of old size
            //and new size.
            private void atumImage_SizeChanged(object sender, SizeChangedEventArgs e)
            {
                UpdateLocatorBoundary(false);

                if (CanChangeImageLocation)
                {
                    var oldLeft = imagePresenter.Margin.Left;
                    var oldTop = imagePresenter.Margin.Top;

                    if (!e.PreviousSize.IsZero())
                    {
                        var withRatioChange = e.NewSize.Width / e.PreviousSize.Width;
                        var heightRatioChange = e.NewSize.Height / e.PreviousSize.Height;
                        var newLeft = oldLeft * withRatioChange < minLeft ? oldLeft : oldLeft * withRatioChange;
                        var newTop = oldTop * heightRatioChange < minLeft ? oldTop : oldTop * heightRatioChange;
                        UpdateImageLocation(new Thickness(newLeft, newTop, 0.0, 0.0));
                    }

                    UpdateImageLocation(new Thickness(oldLeft, oldTop, 0.0, 0.0));
                }
            }


            // this method use to determine the boundary of image locator
            // left equal with X coordinator
            // top equal with Y coordinator
            // this is called when
            // # frame size changed
            // # image presenter size changed
            private void UpdateLocatorBoundary(bool initUserData)
            {
                if (!initUserData)
                {
                    minLeft = frame.ActualWidth - imagePresenter.ActualWidth;
                    minTop = frame.ActualHeight - imagePresenter.ActualHeight;
                    maxLeft = 0d;
                    maxTop = 0d;

                    if (minLeft >= 0 && minTop >= 0)
                    {
                        CanChangeImageLocation = false;
                    }
                    else
                    {
                        if (minLeft >= 0)
                        {
                            minLeft = 0;
                        }

                        if (minTop >= 0)
                        {
                            minTop = 0;
                        }
                        CanChangeImageLocation = true;
                    }
                }
                else
                {
                    var lastRect = border.AtumImageData.GetLocatorBoundary();

                    var frameWidthRatio = frame.RenderSize.Width / border.AtumImageData.FrameSize.Width;
                    var frameHeightRatio = frame.RenderSize.Height / border.AtumImageData.FrameSize.Height;

                    minLeft = lastRect.TopLeft.X * frameWidthRatio;
                    minTop = lastRect.TopLeft.Y * frameHeightRatio;
                    maxLeft = lastRect.BottomRight.X * frameWidthRatio;
                    maxTop = lastRect.BottomRight.Y * frameHeightRatio;
                }

                logger.V(":boundary minLeft = " + minLeft + " minTop = " + minTop);
                logger.V(":boundary maxLeft = " + maxLeft + " maxTop = " + maxTop);

            }

            private void UpdateImageLocation(Thickness margin)
            {
                if (margin != null)
                {
                    // update new left value accord to the boundary
                    var newLeft = margin.Left <= minLeft ? minLeft : margin.Left >= maxLeft ? maxLeft : margin.Left;

                    // update new top value accord to the boundary
                    var newTop = margin.Top <= minTop ? minTop : margin.Top >= maxTop ? maxTop : margin.Top;

                    imagePresenter.Margin = new Thickness(newLeft, newTop, 0.0, 0.0);

                    UpdateUserImageData();

                    logger.I("::::: image location: left = " + newLeft + " top = " + newTop);
                }
            }

            private void UpdateUserImageData()
            {
                // Update user data
                if (border.tempUserData == null)
                {
                    border.tempUserData = new AtumUserData();
                }
                var data = border.tempUserData;

                data.AtumImageMargin = new Thickness(imagePresenter.Margin.Left, imagePresenter.Margin.Top, 0.0, 0.0);
                data.FrameSize = new Size(frame.RenderSize.Width, frame.RenderSize.Height);
                data.ImageSize = new Size(imageSize.Width, imageSize.Height);
                data.ImageMinSize = new Size(minSize.Width, minSize.Height);

                //Raise user data proerty changed event 
                TempUserDataChangedEventArgs eventArgs = new TempUserDataChangedEventArgs(AtumImageView.TempUserDataChangedEvent, data);
                border.RaiseEvent(eventArgs);

                logger.D(":margin = " + data.AtumImageMargin.ToString());
                logger.D(":frame size = " + data.FrameSize.ToString());
                logger.D(":image size:" + data.ImageSize.ToString());
                logger.D(":image min size:" + data.ImageMinSize.ToString());

            }

            public void ResetLocator()
            {
                InitalizeAlignment();
                UpdateImageLocation(initMargin);
            }

            public void AtumImagePresenter_MouseMove(object sender, MouseEventArgs e)
            {
                if (frame.Cursor == Cursors.ScrollAll)
                {
                    if (!CanChangeImageLocation)
                    {
                        return;
                    }

                    Point currentMPoint = e.GetPosition(frame);
                    Point pressedMPoint = lastPointPressed;

                    lastPointPressed = currentMPoint;

                    double offsetLeft = currentMPoint.X - pressedMPoint.X;
                    double offsetTop = currentMPoint.Y - pressedMPoint.Y;

                    double newLeft = offsetLeft;
                    double newTop = offsetTop;

                    if (imagePresenter.Margin != null)
                    {
                        newLeft = imagePresenter.Margin.Left;
                        newTop = imagePresenter.Margin.Top;
                        newLeft += offsetLeft;
                        newTop += offsetTop;
                    }

                    UpdateImageLocation(new Thickness(newLeft, newTop, 0.0, 0.0));

                    logger.D("current mouse point: X = " + currentMPoint.X + " Y = " + currentMPoint.Y);
                    logger.D("pressed mouse point: X = " + pressedMPoint.X + " Y = " + pressedMPoint.Y);
                    logger.D("Set Margin: New Left = " + newLeft + "   New Top = " + newTop);

                }
            }

            public void AtumImagePrsenter_MouseLeftDown(object sender, MouseButtonEventArgs e)
            {
                if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
                {
                    frame.Cursor = Cursors.ScrollAll;
                    lastPointPressed = e.GetPosition(frame);
                }
            }

            public void AtumImagePrsenter_MouseLeftUp(object sender, MouseButtonEventArgs e)
            {
                frame.Cursor = Cursors.Arrow;
            }

            public void AtumImagePrsenter_MouseLeave(object sender, MouseEventArgs e)
            {
                frame.Cursor = Cursors.Arrow;
            }

        }
        private class AtumZoomer
        {
            private const double minZoom = 1d;
            private const double maxZoom = 2d;

            private AtumImageView border { get; set; }
            private AtumImage imagePresenter { get; set; }
            private Grid contentParent { get; set; }
            private Grid frame { get; set; }

            // the minimum size of image
            private Size minSize { get; set; }

            // the origin size of image
            private Size imageSize { get; set; }

            private double currentZoom { get; set; } = 1d;

            public AtumZoomer(AtumImageView border, AtumImage imagePresenter, Grid contentParent, Grid frame)
            {
                if (border == null || imagePresenter == null || contentParent == null || frame == null)
                {
                    logger.E("Can not set up zoom function, deal to null exception!");
                    throw new InvalidOperationException("Can not set up zoom function, deal to null exception!");
                }
                this.border = border;
                this.imagePresenter = imagePresenter;
                this.contentParent = contentParent;
                this.frame = frame;

                minSize = new Size(0.0, 0.0);
                border.ImageSourceChanged += border_ImageSourceChanged;
                frame.SizeChanged += frame_SizeChanged;

                imagePresenter.ImageSourceRendered += ImagePresenter_ImageSourceRendered;
            }

            private void ImagePresenter_ImageSourceRendered(object sender, AtumImageSourceRenderedEventArgs e)
            {

                var imgSize = new Size(e.NewValue.Width, e.NewValue.Height);

                logger.D("[ImagePresenter_ImageSourceRendered] newImageSize=" + imgSize);

                // When current origin image size is different from current rendered image
                // need to update new image size and minimum size
                if (imageSize.IsDifferent(imgSize))
                {
                    imageSize = imgSize;

                    UpdateMinSize();
                }

                // When enter the image locate helper window, the image source have been change before 
                // the event was registered, hence it won't update the user zoom data, and image size
                // base on user's zoom.
                // So need to init the user zoom, and update image size.
                if (imagePresenter.IsFirstTimeSourceRendered)
                {
                    InitalizeUserZoom();

                    UpdateImagePresenterSize();
                }
            }

            private void InitalizeUserZoom()
            {
                if (border.AtumImageData != null)
                {
                    currentZoom = border.AtumImageData.Zoom;
                }
            }

            public void ResetZoomer()
            {
                InitalizeUserZoom();

                UpdateImagePresenterSize();
            }

            private void frame_SizeChanged(object sender, SizeChangedEventArgs e)
            {
                UpdateMinSize();

                // when frame size change and source has been rendered
                // must update image presenter size
                if (imagePresenter.IsSourceHasBeenRendered)
                {
                    UpdateImagePresenterSize();
                }
            }

            private void border_ImageSourceChanged(object sender, ImageSourceChangedEventArgs e)
            {

                InitalizeUserZoom();

                // when new source was updated
                // update origin image size and minimum size as well
                // also reset zoom to minimum zoom as concept
                if (e.NewValue != null)
                {
                    Size imgSize = new Size(e.NewValue.Width, e.NewValue.Height);
                    imageSize = imgSize;

                    UpdateMinSize();

                    currentZoom = minZoom;

                    UpdateImagePresenterSize();
                }
            }

            private void UpdateMinSize()
            {
                Size fixedSize = frame.RenderSize;

                double heightRatio = imageSize.Height / fixedSize.Height;
                double widthRatio = imageSize.Width / fixedSize.Width;
                double uniformRatio = heightRatio < widthRatio ? heightRatio : widthRatio;

                minSize = new Size(imageSize.Width / uniformRatio, imageSize.Height / uniformRatio);
            }

            private void UpdateImagePresenterSize()
            {
                if (!minSize.IsZero())
                {
                    // Limit current zoom
                    currentZoom = currentZoom >= maxZoom ? maxZoom : currentZoom <= minZoom ? minZoom : currentZoom;

                    var newSize = minSize.MultipleSize(currentZoom);

                    imagePresenter.Height = newSize.Height;
                    imagePresenter.Width = newSize.Width;
                    imagePresenter.InvalidateVisual();

                    UpdateUserData();
                }
            }

            private void UpdateUserData()
            {
                // Update user data
                if (border.tempUserData == null)
                {
                    border.tempUserData = new AtumUserData();
                }
                var data = border.tempUserData;
                data.Zoom = currentZoom;

                //Raise user data proerty changed event 
                TempUserDataChangedEventArgs eventArgs = new TempUserDataChangedEventArgs(AtumImageView.TempUserDataChangedEvent, data);
                border.RaiseEvent(eventArgs);

                logger.D("currentZoom=" + currentZoom + " imageSize=" + imageSize);
            }

            public void AtumImagePrsenter_MouseWheel(object sender, MouseWheelEventArgs e)
            {
                // Hold Ctrl key and use mouse wheel as concept to zoom
                if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
                {
                    double zoom = e.Delta > 0 ? .2 : -.2;
                    currentZoom += zoom;
                    UpdateImagePresenterSize();
                }
            }

        }
        private class TransformHelperWindow
        {
            private readonly double panelAndContentWidthRatio = 1.2d;
            private readonly double panelAndContentHeightRatio = 1.05d;
            private readonly double imageAndScreenRatio = 0.5d;

            private readonly double screenWidth = SystemParameters.MaximizedPrimaryScreenWidth;
            private readonly double screenHeight = SystemParameters.MaximizedPrimaryScreenHeight;

            private AtumImageView atumImageView { get; set; }
            private AtumImageView clonedView { get; set; }
            private Grid panel { get; set; }

            private double fixedBorderWidth { get; set; }
            private double fixedBorderHeight { get; set; }

            private AnubisMessageBox helperWindow;
            public TransformHelperWindow(AtumImageView border)
            {
                atumImageView = border;
                panel = new Grid();
                fixedBorderWidth = screenWidth * imageAndScreenRatio;
                fixedBorderHeight = screenHeight * imageAndScreenRatio;
            }

            public void Show()
            {
                logger.I("[OnTransformHelperWindowShow]");

                UpdateCloneViewProperties();

                UpdateCloneViewSize();

                panel.Children.Clear();
                panel.Children.Add(clonedView);
                panel.Width = clonedView.Width * panelAndContentWidthRatio;
                panel.Height = clonedView.Height * panelAndContentHeightRatio;

                helperWindow = new AnubisMessageBox(
                    panel,
                    AnubisMessageBoxType.Default,
                    "Locate your image")
                {
                    BorderThickness = new Thickness(1)
                };

                var x = helperWindow.Show();

                if (x == AnubisMessgaeResult.ResultOK)
                {
                    if (!clonedView.tempUserData.ImageSize.IsZero())
                    {
                        atumImageView.AtumImageData = clonedView.tempUserData;

                        // Must reset zoomer to get image presenter size first
                        atumImageView.atumZoomer.ResetZoomer();

                        atumImageView.atumLocator.ResetLocator();
                    }
                }
            }

            private void UpdateCloneViewProperties()
            {
                clonedView = CloneAtum(atumImageView) as AtumImageView;
                clonedView.IsSupportImageLocateHelper = false;
                clonedView.IsEnableAtumTransformer = true;

                clonedView.AtumImageData = DependencyObjectHelper.CloneProperties(atumImageView.tempUserData) as AtumUserData;
            }

            private void UpdateCloneViewSize()
            {
                if (double.IsNaN(clonedView.Height))
                {
                    clonedView.Height = atumImageView.ActualHeight;
                }

                if (double.IsNaN(clonedView.Width))
                {
                    clonedView.Width = atumImageView.ActualWidth;
                }


                // Compute accord to fixed border size
                var widthHeightRatio = clonedView.Width / clonedView.Height;

                if (clonedView.Width < fixedBorderWidth && clonedView.Height < fixedBorderHeight)
                {
                    if (widthHeightRatio > 1)
                    {
                        clonedView.Width = fixedBorderWidth;
                        clonedView.Height = fixedBorderWidth / widthHeightRatio;
                    }
                    else
                    {
                        clonedView.Height = fixedBorderHeight;
                        clonedView.Width = fixedBorderHeight * widthHeightRatio;
                    }
                }
            }

            private object CloneAtum(AtumImageView view)
            {
                var x = new AtumImageView();

                List<DependencyProperty> re = DependencyObjectHelper.GetDependencyProperties(view);

                foreach (DependencyProperty dp in re)
                {
                    if (dp != ImagePathProperty)
                    {
                        x.SetValue(dp, view.GetValue(dp));
                    }
                }

                return x;
            }

            internal void AtumImageView_MouseEnter(object sender, MouseEventArgs e)
            {
                atumImageView.Cursor = Cursors.Hand;
            }

            internal void AtumImageView_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
            {
                Show();
            }
        }

        public class AtumUserData
        {
            public AtumUserData()
            {
                Zoom = 1.0;
            }

            // Last left and top value
            public Thickness AtumImageMargin { get; set; }

            // Original image size
            public Size ImageSize { get; set; }

            // Last minimum image size to uniform with frame size
            public Size ImageMinSize { get; set; }

            // Last fixed size 
            public Size FrameSize { get; set; }

            // Last zoom ratio
            public double Zoom { get; set; }

            // Last locator boundary
            public Rect GetLocatorBoundary()
            {
                var minLeft = FrameSize.Width - ImageMinSize.MultipleSize(Zoom).Width;
                var minTop = FrameSize.Height - ImageMinSize.MultipleSize(Zoom).Height;
                var maxLeft = 0d;
                var maxTop = 0d;

                return new Rect(new Point(minLeft, minTop), new Point(maxLeft, maxTop));
            }
        }

        #endregion

    }

    public class AtumImage : System.Windows.Controls.Image
    {

        #region AtumImageSourceRenderedEvent
        public static readonly RoutedEvent AtumImageSourceRenderedEvent =
           EventManager.RegisterRoutedEvent("ImageSourceRendered", RoutingStrategy.Direct,
                         typeof(AtumImageSourceRenderedHandler), typeof(AtumImage));

        public event AtumImageSourceRenderedHandler ImageSourceRendered
        {
            add { AddHandler(AtumImageSourceRenderedEvent, value); }
            remove { RemoveHandler(AtumImageSourceRenderedEvent, value); }
        }
        #endregion

        /// <summary>
        /// true if source has been rendered the first time
        /// </summary>
        public bool IsFirstTimeSourceRendered { get; private set; }

        /// <summary>
        /// true if source has been rendered
        /// </summary>
        public bool IsSourceHasBeenRendered { get { return SourceRenderdCounterCache > 0; } }

        private int SourceRenderdCounterCache = 0;
        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            if (Source != null)
            {
                //Update source rendered counter
                SourceRenderdCounterCache += 1;

                // Update first time source updated
                IsFirstTimeSourceRendered = SourceRenderdCounterCache == 1;

                OnImageSourceRendered(dc);
            }
        }

        private void OnImageSourceRendered(DrawingContext dc)
        {
            AtumImageSourceRenderedEventArgs e = new AtumImageSourceRenderedEventArgs(
                AtumImage.AtumImageSourceRenderedEvent,
                dc,
                Source);
            RaiseEvent(e);
        }

    }

    public delegate void AtumImageSourceRenderedHandler(object sender, AtumImageSourceRenderedEventArgs e);
    public delegate void ImageSourceChangedHandler(object sender, ImageSourceChangedEventArgs e);
    public delegate void PreviewAsyncSourceUpdatedHandler(object sender, PreviewAsyncSourceUpdatedEventArgs e);
    public delegate void TempUserDataChangedHandler(object sender, TempUserDataChangedEventArgs e);

    public class AtumImageSourceRenderedEventArgs : RoutedEventArgs
    {
        private ImageSource newValue;
        private DrawingContext drawingContext;
        public AtumImageSourceRenderedEventArgs(RoutedEvent id, DrawingContext dc, ImageSource newVal)
        {
            drawingContext = dc;
            newValue = newVal;
            RoutedEvent = id;
        }

        public ImageSource NewValue
        {
            get { return newValue; }
        }

        public DrawingContext DrawingContext
        {
            get { return drawingContext; }
        }
    }
    public class PreviewAsyncSourceUpdatedEventArgs : RoutedEventArgs
    {
        private string oldPath;
        private string newPath;
        public bool Handled { get; set; } = false;

        public PreviewAsyncSourceUpdatedEventArgs(RoutedEvent id, string oldPat, string newPat)
        {
            oldPath = oldPat;
            newPath = newPat;
            RoutedEvent = id;
        }

        public string OldPath
        {
            get { return oldPath; }
        }
        public string NewPath
        {
            get { return newPath; }
        }
    }
    public class ImageSourceChangedEventArgs : RoutedEventArgs
    {
        private ImageSource newValue;
        private ImageSource oldValue;

        public ImageSourceChangedEventArgs(RoutedEvent id, ImageSource oldVal, ImageSource newVal)
        {
            newValue = newVal;
            oldValue = oldVal;
            RoutedEvent = id;
        }

        public ImageSource OldValue
        {
            get { return oldValue; }
        }
        public ImageSource NewValue
        {
            get { return newValue; }
        }
    }
    public class TempUserDataChangedEventArgs : RoutedEventArgs
    {
        private AtumImageView.AtumUserData _userData;
        public TempUserDataChangedEventArgs(RoutedEvent id, AtumImageView.AtumUserData userData)
        {
            RoutedEvent = id;
            _userData = userData;
        }

        public AtumImageView.AtumUserData UserData
        {
            get { return _userData; }
        }

    }

}