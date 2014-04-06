using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

using Spearware.ProgressView;
using Spearware.ProgressView;

namespace ProgressExample
{
    public partial class ProgressExampleViewController : UIViewController
    {
        public ProgressExampleViewController() : base("ProgressExampleViewController", null)
        {
        }

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();
            
            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            View.BackgroundColor = UIColor.White;

            progressView.BackgroundColor = UIColor.Clear;

            startButton.TouchUpInside += (sender, e) =>
            {
                if (progressView.IsRunning)
                    progressView.Stop();
                else
                    progressView.Start();

                UpdateButtonText();
            };

            UpdateButtonText();
        }

        void UpdateButtonText()
        {
            var titleText = progressView.IsRunning ? "Stop" : "Start";
            startButton.SetTitle(titleText, UIControlState.Normal);
        }
    }
}

