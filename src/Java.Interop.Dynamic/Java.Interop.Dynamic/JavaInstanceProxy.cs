using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Text;

using Mono.Linq.Expressions;

using Java.Interop;

namespace Java.Interop.Dynamic {

	[JniTypeInfoAttribute ("java/lang/Object")]
	class JavaInstanceProxy : JavaObject {

		public JavaInstanceProxy (ref JniObjectReference reference, JniHandleOwnership transfer)
			: base (ref reference, transfer)
		{
		}
	}

}