using System;
using System.Drawing;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using MonoTouch.CoreGraphics;
using MonoTouch.CoreAnimation;
using MonoTouch.ObjCRuntime;

namespace Spearware.ProgressView
{
    public class CircularProgressLayer : CALayer
    {
        public const string ProgressKey = "progress";

        #region Construct

        [Export("initWithLayer:")]
        public CircularProgressLayer(CALayer other) : base(other)
        {
            var otherProgressLayer = other as CircularProgressLayer;

            if (otherProgressLayer != null)
            {
                InnerColor = otherProgressLayer.InnerColor;
                InsideColor = otherProgressLayer.InsideColor;
                OuterColor = otherProgressLayer.OuterColor;
            }

        }

        public CircularProgressLayer(IntPtr handle) : base(handle)
        {
        }

        public CircularProgressLayer()
        {
        }

        #endregion

        #region Members

        /// <summary>
        /// Color inside of the circle.
        /// </summary>
        /// <value>The color of the inner.</value>
        public UIColor InnerColor { get; set; }

        /// <summary>
        /// Color of the progress
        /// </summary>
        /// <value>The color of the inside.</value>
        public UIColor InsideColor { get; set; }

        /// <summary>
        /// Color outside of the progress
        /// </summary>
        /// <value>The color of the outer.</value>
        public UIColor OuterColor { get; set; }

        #endregion

        [Export("needsDisplayForKey:")]
        static bool NeedsDisplayForKey(NSString key)
        {
            return key == ProgressKey || CALayer.NeedsDisplayForKey(key);
        }

        [Export(ProgressKey)]
        public float Progress { get; set; }

        public override void DrawInContext(CGContext ctx)
        {
            UIGraphics.PushContext(ctx);

            Draw(Bounds);

            UIGraphics.PopContext();
        }

        void Draw(RectangleF rect)
        {
            //// Color Declarations

            //// Frames
            var bgFrame = new RectangleF(0, 0, rect.Width, rect.Height);

            //// Subframes
            var circleGroup = new RectangleF(bgFrame.GetMinX() + (float)Math.Floor(bgFrame.Width * 0.13437f + 0.5f), bgFrame.GetMinY() + (float)Math.Floor(bgFrame.Height * 0.12500f + 0.5f), (float)Math.Floor(bgFrame.Width * 0.85938f + 0.5f) - (float)Math.Floor(bgFrame.Width * 0.13437f + 0.5f), (float)Math.Floor(bgFrame.Height * 0.84688f + 0.5f) - (float)Math.Floor(bgFrame.Height * 0.12500f + 0.5f));


            //// Abstracted Attributes
            var progressOvalEndAngle = Progress;

            //// circleGroup
            {
                //// outerOval Drawing
                var outerOvalPath = UIBezierPath.FromOval(new RectangleF(circleGroup.GetMinX() + (float)Math.Floor(circleGroup.Width * 0.00216f) + 0.5f, circleGroup.GetMinY() + (float)Math.Floor(circleGroup.Height * 0.00000f + 0.5f), (float)Math.Floor(circleGroup.Width * 0.99784f) - (float)Math.Floor(circleGroup.Width * 0.00216f), (float)Math.Floor(circleGroup.Height * 1.00000f + 0.5f) - (float)Math.Floor(circleGroup.Height * 0.00000f + 0.5f)));
                OuterColor.SetFill();
                outerOvalPath.Fill();

                //// progressOval Drawing
                var progressOvalRect = new RectangleF(circleGroup.GetMinX() + (float)Math.Floor(circleGroup.Width * 0.00216f) + 0.5f, circleGroup.GetMinY() + (float)Math.Floor(circleGroup.Height * 0.00000f + 0.5f), (float)Math.Floor(circleGroup.Width * 0.99784f) - (float)Math.Floor(circleGroup.Width * 0.00216f), (float)Math.Floor(circleGroup.Height * 1.00000f + 0.5f) - (float)Math.Floor(circleGroup.Height * 0.00000f + 0.5f));
                var progressOvalPath = new UIBezierPath();
                progressOvalPath.AddArc(new PointF(progressOvalRect.GetMidX(), progressOvalRect.GetMidY()), progressOvalRect.Width / 2, (float)(270 * Math.PI / 180), (float)(progressOvalEndAngle * Math.PI / 180), true);
                progressOvalPath.AddLineTo(new PointF(progressOvalRect.GetMidX(), progressOvalRect.GetMidY()));
                progressOvalPath.ClosePath();

                InnerColor.SetFill();
                progressOvalPath.Fill();


                //// innerOval Drawing
                var innerOvalPath = UIBezierPath.FromOval(new RectangleF(circleGroup.GetMinX() + (float)Math.Floor(circleGroup.Width * 0.09052f + 0.5f), circleGroup.GetMinY() + (float)Math.Floor(circleGroup.Height * 0.09091f + 0.5f), (float)Math.Floor(circleGroup.Width * 0.90948f + 0.5f) - (float)Math.Floor(circleGroup.Width * 0.09052f + 0.5f), (float)Math.Floor(circleGroup.Height * 0.91342f + 0.5f) - (float)Math.Floor(circleGroup.Height * 0.09091f + 0.5f)));
                InsideColor.SetFill();
                innerOvalPath.Fill();
            }
        }
    }

    [Register("CircularProgressView")]
    public class CircularProgressView : UIView
    {
        #region Construct

        public CircularProgressView(RectangleF frame) : base(frame)
        {
            CommonInit();
        }

        public CircularProgressView(IntPtr handle) : base(handle)
        {
            CommonInit();
        }

        void CommonInit()
        {
            SetProgress(StartAngle, StartAngle, false);
            InnerColor = UIColor.FromRGBA(0.824f, 0.824f, 0.824f, 1.000f);
            InsideColor = UIColor.FromRGBA(0.749f, 0.749f, 0.749f, 1.000f);
            OuterColor = UIColor.FromRGBA(0.277f, 0.544f, 0.948f, 0.750f);
        }

        #endregion

        #region Layer

        [Export("layerClass")]
        public static Class LayerClass()
        {
            return new Class(typeof(CircularProgressLayer));
        }

        CircularProgressLayer CircularProgressLayer
        {
            get
            {
                return (CircularProgressLayer)Layer;
            }
        }

        public override void MovedToWindow()
        {
            var windowContentScale = Window.Screen.Scale;
            CircularProgressLayer.ContentsScale = windowContentScale;
            CircularProgressLayer.SetNeedsDisplay();
        }

        #endregion

        #region Members

        const float StartAngle = 270.0f;

        /// <summary>
        /// Color inside of the circle.
        /// </summary>
        /// <value>The color of the inner.</value>
        public UIColor InnerColor
        {
            get{ return CircularProgressLayer.InnerColor; }
            set { CircularProgressLayer.InnerColor = value; }
        }

        /// <summary>
        /// Color of the progress
        /// </summary>
        /// <value>The color of the inside.</value>
        public UIColor InsideColor
        {
            get{ return CircularProgressLayer.InsideColor; }
            set { CircularProgressLayer.InsideColor = value; }
        }

        /// <summary>
        /// Color outside of the progress
        /// </summary>
        /// <value>The color of the outer.</value>
        public UIColor OuterColor
        {
            get{ return CircularProgressLayer.OuterColor; }
            set { CircularProgressLayer.OuterColor = value; }
        }

        [Export(CircularProgressLayer.ProgressKey)]
        public float Progress
        {
            get
            {
                return CircularProgressLayer.Progress;
            }
            set
            {
                SetProgress(Progress, value, Progress > value);
            }
        }

        const string indeterminateAnimation = "indeterminateAnimation";

        void SetProgress(float fromProgress, float toProgress, bool animate)
        {
            Layer.RemoveAnimation(indeterminateAnimation);
            Layer.RemoveAnimation(CircularProgressLayer.ProgressKey);

            var progressPct = Progress / StartAngle;
            var pinnedProgress = Math.Min(Math.Max(progressPct, 0.0f), 1.0f);

            if(toProgress > fromProgress) //Don't want to animate between 360 to 0
            {
                if (animate)
                {
                    var animation = CABasicAnimation.FromKeyPath(CircularProgressLayer.ProgressKey);
                    animation.Duration = Math.Abs(progressPct - pinnedProgress);
                    animation.TimingFunction = CAMediaTimingFunction.FromName(CAMediaTimingFunction.EaseInEaseOut);
                    animation.From = NSNumber.FromFloat(fromProgress);
                    animation.To = NSNumber.FromFloat(toProgress);
                    CircularProgressLayer.AddAnimation(animation, CircularProgressLayer.ProgressKey);
                }
                else
                {
                    CircularProgressLayer.SetNeedsDisplay();
                }
            }

            CircularProgressLayer.Progress = toProgress;
        }

        #endregion

        #region Intermediate progress

        void UpdateProgress()
        {
            if ((Progress + 1) > 360)
                Progress = 0;
            else
                Progress++;
        }

        /// <summary>
        /// Is the progress view running the progress animation
        /// </summary>
        /// <value><c>true</c> If the progress view is running the progress animation <c>false</c>.</value>
        public bool IsRunning
        {
            get
            {
                return _timer != null;
            }
        }

        NSTimer _timer;
        const float timerMilliseconds = 20.0f;

        /// <summary>
        /// Start the intermediate progress
        /// </summary>
        public void Start()
        {
            if (_timer != null)
                return;

            _timer = NSTimer.CreateRepeatingScheduledTimer(TimeSpan.FromSeconds(0.03), UpdateProgress);
        }

        /// <summary>
        /// Stop the intermediate progress
        /// </summary>
        public void Stop()
        {
            if (_timer == null)
                return;

            _timer.Invalidate();
            _timer.Dispose();
            _timer = null;
        }

        #endregion

        #region Clean Up

        protected override void Dispose(bool disposing)
        {
            Stop();
            base.Dispose(disposing);
        }

        #endregion
    }
}

