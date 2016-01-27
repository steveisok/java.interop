using System;

namespace Java.Interop
{
	[JniTypeSignature ("java/lang/Object")]
	unsafe public class JavaObject : IJavaPeerable, IJavaPeerableEx
	{
		readonly static JniPeerMembers _members = new JniPeerMembers ("java/lang/Object", typeof (JavaObject));

		int     keyHandle;
		bool    registered;

#if FEATURE_JNIOBJECTREFERENCE_SAFEHANDLES
		JniObjectReference  reference;
#endif  // FEATURE_JNIOBJECTREFERENCE_SAFEHANDLES
#if FEATURE_JNIOBJECTREFERENCE_INTPTRS
		IntPtr                  handle;
		JniObjectReferenceType  handle_type;
	#pragma warning disable 0169
		// Used by JavaInteropGCBridge
		IntPtr                  weak_handle;
		int                     refs_added;
	#pragma warning restore 0169
#endif  // FEATURE_JNIOBJECTREFERENCE_INTPTRS

		protected   static  readonly    JniObjectReference*     InvalidJniObjectReference  = null;

		~JavaObject ()
		{
			JniEnvironment.Runtime.ValueManager.TryCollectObject (this);
		}

		public          JniObjectReference          PeerReference {
			get {
#if FEATURE_JNIOBJECTREFERENCE_SAFEHANDLES
				return reference;
#endif  // FEATURE_JNIOBJECTREFERENCE_SAFEHANDLES
#if FEATURE_JNIOBJECTREFERENCE_INTPTRS
				return new JniObjectReference (handle, handle_type);
#endif  // FEATURE_JNIOBJECTREFERENCE_INTPTRS
			}
		}

		// Note: JniPeerMembers is invoked virtually from the constructor;
		// it MUST be valid before the derived constructor executes!
		// The pattern MUST be followed.
		public  virtual JniPeerMembers              JniPeerMembers {
			get {return _members;}
		}

		public JavaObject (ref JniObjectReference reference, JniObjectReferenceOptions transfer)
		{
			if (transfer == JniObjectReferenceOptions.None)
				return;

			using (SetPeerReference (ref reference, transfer)) {
			}
		}

		public unsafe JavaObject ()
		{
			var peer = JniPeerMembers.InstanceMethods.StartCreateInstance ("()V", GetType (), null);
			using (SetPeerReference (
					ref peer,
					JniObjectReferenceOptions.CopyAndDispose)) {
				JniPeerMembers.InstanceMethods.FinishCreateInstance ("()V", this, null);
			}
		}

		protected SetPeerReferenceCompletion SetPeerReference (ref JniObjectReference handle, JniObjectReferenceOptions transfer)
		{
			return JniEnvironment.Runtime.ValueManager.SetObjectPeerReference (
					this,
					ref handle,
					transfer,
					a => new SetPeerReferenceCompletion (a));
		}

		public void UnregisterFromRuntime ()
		{
			if (!PeerReference.IsValid)
				throw new ObjectDisposedException (GetType ().FullName);
			JniEnvironment.Runtime.ValueManager.UnRegisterObject (this);
		}

		public void Dispose ()
		{
			JniEnvironment.Runtime.ValueManager.DisposeObject (this);
		}

		public void DisposeUnlessRegistered ()
		{
			if (registered)
				return;
			Dispose ();
		}

		protected virtual void Dispose (bool disposing)
		{
		}

		public override bool Equals (object obj)
		{
			JniPeerMembers.AssertSelf (this);

			if (object.ReferenceEquals (obj, this))
				return true;
			var o = obj as IJavaPeerable;
			if (o != null)
				return JniEnvironment.Types.IsSameObject (PeerReference, o.PeerReference);
			return false;
		}

		public override unsafe int GetHashCode ()
		{
			return _members.InstanceMethods.InvokeVirtualInt32Method ("hashCode.()I", this, null);
		}

		public override unsafe string ToString ()
		{
			var lref = _members.InstanceMethods.InvokeVirtualObjectMethod (
					"toString.()Ljava/lang/String;",
					this,
					null);
			return JniEnvironment.Strings.ToString (ref lref, JniObjectReferenceOptions.CopyAndDispose);
		}

		int IJavaPeerableEx.IdentityHashCode {
			get {return keyHandle;}
			set {keyHandle = value;}
		}

		bool IJavaPeerableEx.Registered {
			get {return registered;}
			set {registered = value;}
		}

		void IJavaPeerableEx.Dispose (bool disposing)
		{
			Dispose (disposing);
		}

		void IJavaPeerableEx.SetPeerReference (JniObjectReference reference)
		{
#if FEATURE_JNIOBJECTREFERENCE_SAFEHANDLES
			this.reference  = reference;
#endif  // FEATURE_JNIOBJECTREFERENCE_SAFEHANDLES
#if FEATURE_JNIOBJECTREFERENCE_INTPTRS
			this.handle         = reference.Handle;
			this.handle_type    = reference.Type;
#endif  // FEATURE_JNIOBJECTREFERENCE_INTPTRS
		}

		protected struct SetPeerReferenceCompletion : IDisposable {

			readonly    Action  action;

			public SetPeerReferenceCompletion (Action action)
			{
				this.action = action;
			}

			public void Dispose ()
			{
				if (action != null)
					action ();
			}
		}
	}
}

