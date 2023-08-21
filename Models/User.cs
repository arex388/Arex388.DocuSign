//using Arex388.DocuSign.Converters;
//using System.Text.Json.Serialization;

//namespace Arex388.DocuSign;

//public sealed class User {
//	[JsonPropertyName("userAddedToAccountDateTime")]
//	public DateTimeOffset AddedToAccountAtUtc { get; init; }

//	[JsonConverter(typeof(StringJsonConverter))]
//	public string? Company { get; init; }

//	[JsonPropertyName("createdDateTime")]
//	public DateTimeOffset CreatedAtUtc { get; init; }

//	public string Email { get; init; } = null!;
//	public string FirstName { get; init; } = null!;

//	[JsonPropertyName("groupList")]
//	public IEnumerable<Group> Groups { get; init; } = Enumerable.Empty<Group>();

//	public Address? HomeAddress { get; init; }

//	[JsonPropertyName("userId")]
//	public Guid Id { get; init; }

//	[JsonConverter(typeof(BooleanJsonConverter))]
//	public bool IsAdmin { get; init; }

//	[JsonConverter(typeof(BooleanJsonConverter)), JsonPropertyName("enableConnectForUser")]
//	public bool IsConnectEnabled { get; init; }

//	[JsonConverter(typeof(BooleanJsonConverter))]
//	public bool IsNarEnabled { get; init; }

//	public string LastName { get; init; } = null!;

//	[JsonPropertyName("userProfileLastModifiedDate")]
//	public DateTimeOffset ModifiedAtUtc { get; init; }

//	public int PermissionProfileId { get; init; }
//	public string PermissionProfileName { get; init; } = null!;

//	[JsonConverter(typeof(BooleanJsonConverter))]
//	public bool SendActivationOnInvalidLogin { get; init; }

//	[JsonPropertyName("userSettings")]
//	public UserSettings Settings { get; init; } = null!;

//	[JsonPropertyName(("userStatus"))]
//	public string Status { get; init; } = null!;

//	[JsonConverter(typeof(StringJsonConverter)), JsonPropertyName("jobTitle")]
//	public string? Title { get; init; }

//	[JsonPropertyName("userType")]
//	public string Type { get; init; } = null!;

//	[JsonPropertyName("uri")]
//	public string Url { get; init; } = null!;

//	public string Username { get; init; } = null!;
//	public Address WorkAddress { get; init; } = null!;
//}