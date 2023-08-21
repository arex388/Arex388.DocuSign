namespace System;

internal static class StringExtensions {
	public static bool HasValue(
		this string? value) => !string.IsNullOrEmpty(value);

	public static string StringJoin(
		this IEnumerable<string> values,
		string separator) => string.Join(separator, values);
}