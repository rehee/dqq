using System.Text.Json;

namespace DQQ.Helper
{
	public static class JSONHelper
	{
		public static string ToJson(this object obj)
		{
			return JsonSerializer.Serialize(obj);
		}

		public static T? FromJson<T>(this string? json)
		{
			return JsonSerializer.Deserialize<T>(json?? "");
		}
	}
}
