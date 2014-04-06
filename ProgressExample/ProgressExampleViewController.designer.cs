// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace ProgressExample
{
	[Register ("ProgressExampleViewController")]
	partial class ProgressExampleViewController
	{
		[Outlet]
		Spearware.ProgressView.CircularProgressView progressView { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton startButton { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (progressView != null) {
				progressView.Dispose ();
				progressView = null;
			}

			if (startButton != null) {
				startButton.Dispose ();
				startButton = null;
			}
		}
	}
}
