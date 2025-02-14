using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;

namespace Xamarin.Test {

	// Metadata.xml XPath class reference: path="/api/package[@name='xamarin.test']/class[@name='SomeObject']"
	[global::Android.Runtime.Register ("xamarin/test/SomeObject", DoNotGenerateAcw=true)]
	public partial class SomeObject : global::Java.Lang.Object {



		// Metadata.xml XPath field reference: path="/api/package[@name='xamarin.test']/class[@name='SomeObject']/field[@name='Value']"
		[Register ("Value")]
		public static int Value {
			get {
				const string __id = "Value.I";

				var __v = _members.StaticFields.GetInt32Value (__id);
				return __v;
			}
		}


		// Metadata.xml XPath field reference: path="/api/package[@name='xamarin.test']/class[@name='SomeObject']/field[@name='Value2']"
		[Register ("Value2")]
		public static int Value2 {
			get {
				const string __id = "Value2.I";

				var __v = _members.StaticFields.GetInt32Value (__id);
				return __v;
			}
			set {
				const string __id = "Value2.I";

				try {
					_members.StaticFields.SetValue (__id, value);
				} finally {
				}
			}
		}
		internal static new readonly JniPeerMembers _members = new JniPeerMembers ("xamarin/test/SomeObject", typeof (SomeObject));
		internal static new IntPtr class_ref {
			get {
				return _members.JniPeerType.PeerReference.Handle;
			}
		}

		public override global::Java.Interop.JniPeerMembers JniPeerMembers {
			get { return _members; }
		}

		protected override IntPtr ThresholdClass {
			get { return _members.JniPeerType.PeerReference.Handle; }
		}

		protected override global::System.Type ThresholdType {
			get { return _members.ManagedPeerType; }
		}

		protected SomeObject (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

	}
}
