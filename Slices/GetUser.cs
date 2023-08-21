//namespace Arex388.DocuSign;

///// <summary>
///// GetUser request and response containers.
///// </summary>
//public sealed class GetUser {
//	private static Response? _cancelled;
//	private static Response? _failed;
//	private static Response? _timedOut;

//	internal static Response Cancelled => _cancelled ??= new Response {
//		Status = ResponseStatus.Cancelled
//	};
//	internal static Response Failed(
//		Exception? exception) {
//		if (exception is null) {
//			return _failed ??= new Response {
//				Status = ResponseStatus.Failed
//			};
//		}

//		return new Response {
//			Errors = new[] {
//				$"[{exception.GetType().Name}] {exception.Message}"
//			},
//			Status = ResponseStatus.Failed
//		};
//	}
//	internal static Response TimedOut => _timedOut ??= new Response {
//		Status = ResponseStatus.TimeOut
//	};

//	/// <summary>
//	/// GetUser response container.
//	/// </summary>
//	public sealed class Response {
//		/// <summary>
//		/// The response's errors, if any.
//		/// </summary>
//		public IEnumerable<string> Errors { get; init; } = Enumerable.Empty<string>();

//		/// <summary>
//		/// The response's status.
//		/// </summary>
//		public ResponseStatus Status { get; init; }

//		/// <summary>
//		/// The user.
//		/// </summary>
//		public User? User { get; init; }
//	}
//}