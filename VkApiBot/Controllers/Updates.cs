using System;
using System.Text.Json.Serialization;

namespace VkApiBot.Controllers
{
	[Serializable]
	public class Updates
	{
		/// <summary>
		/// Тип события
		/// </summary>
		[JsonPropertyName("type")]
		public string Type { get; set; }

		/// <summary>
		/// Объект, инициировавший событие
		/// Структура объекта зависит от типа уведомления
		/// </summary>
		[JsonPropertyName("object")]
		public Object Object { get; set; }

		/// <summary>
		/// ID сообщества, в котором произошло событие
		/// </summary>
		[JsonPropertyName("group_id")]
		public long GroupId { get; set; }

		/// <summary>
		/// Секретный ключ. Передается с каждым уведомлением от сервера
		/// </summary>
		[JsonPropertyName("secret")]
		public string Secret { get; set; }
	}

	[Serializable]
	public class Object
	{
		[JsonPropertyName("message")]
		public Message Message { get; set; }

		[JsonPropertyName("client_info")]
		public ClientInfo ClientInfo { get; set; }
	}

	[Serializable]
	public class Message
	{
		[JsonPropertyName("date")]
		public ulong Date { get; set; }

		[JsonPropertyName("from_id")]
		public ulong FromId { get; set; }

		[JsonPropertyName("id")]
		public ulong Id { get; set; }

		[JsonPropertyName("out")]
		public ulong Out { get; set; }

		[JsonPropertyName("peer_id")]
		public ulong PeerId { get; set; }

		[JsonPropertyName("text")]
		public string Text { get; set; }

		[JsonPropertyName("conversation_message_id")]
		public ulong ConversationMessageId { get; set; }

		[JsonPropertyName("fwd_messages")]
		public string[] ForwardMessages { get; set; }

		[JsonPropertyName("important")]
		public bool Important { get; set; }

		[JsonPropertyName("random_id")]
		public ulong RandomId { get; set; }

		[JsonPropertyName("attachments")]
		public string[] Attachments { get; set; }

		[JsonPropertyName("is_hidden")]
		public bool IsHidden { get; set; }
	}

	[Serializable]
	public class ClientInfo
	{
		[JsonPropertyName("button_actions")]
		public string[] ButtonActions { get; set; }

		[JsonPropertyName("keyboard")]
		public bool Keyboard { get; set; }

		[JsonPropertyName("inline_keyboard")]
		public bool InlineKeyboard { get; set; }

		[JsonPropertyName("carousel")]
		public bool Carousel { get; set; }

		[JsonPropertyName("lang_id")]
		public int LangId { get; set; }
	}
}