namespace System;

internal static class StringExtensions {
	public static bool HasValue(
		this string? value) => !string.IsNullOrEmpty(value);
}