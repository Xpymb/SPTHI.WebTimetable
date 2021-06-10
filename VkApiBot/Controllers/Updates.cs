using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace VkApiBot.Controllers
{
	[Serializable]
	public class Updates
	{
		/// <summary>
		/// Тип события
		/// </summary>
		[JsonProperty("type")]
		public string Type { get; set; }

		/// <summary>
		/// Объект, инициировавший событие
		/// Структура объекта зависит от типа уведомления
		/// </summary>
		[JsonProperty("object")]
		public object Object { get; set; }

		/// <summary>
		/// ID сообщества, в котором произошло событие
		/// </summary>
		[JsonProperty("group_id")]
		public long GroupId { get; set; }

		/// <summary>
		/// Секретный ключ. Передается с каждым уведомлением от сервера
		/// </summary>
		[JsonProperty("secret")]
		public string Secret { get; set; }
	}

	[Serializable]
	public class Object
	{
		[JsonProperty("message")]
		public Message Message { get; set; }

		[JsonProperty("client_info")]
		public ClientInfo ClientInfo { get; set; }
	}

	[Serializable]
	public class Message
	{
		[JsonProperty("date")]
		public ulong Date { get; set; }

		[JsonProperty("from_id")]
		public int FromId { get; set; }

		[JsonProperty("id")]
		public ulong Id { get; set; }

		[JsonProperty("out")]
		public ulong Out { get; set; }

		[JsonProperty("peer_id")]
		public int PeerId { get; set; }

		[JsonProperty("text")]
		public string Text { get; set; }

		[JsonProperty("conversation_message_id")]
		public ulong ConversationMessageId { get; set; }

		[JsonProperty("fwd_messages")]
		public string[] ForwardMessages { get; set; }

		[JsonProperty("important")]
		public bool Important { get; set; }

		[JsonProperty("random_id")]
		public ulong RandomId { get; set; }

		[JsonProperty("attachments")]
		public string[] Attachments { get; set; }

		[JsonProperty("is_hidden")]
		public bool IsHidden { get; set; }
	}

	[Serializable]
	public class ClientInfo
	{
		[JsonProperty("button_actions")]
		public string[] ButtonActions { get; set; }

		[JsonProperty("keyboard")]
		public bool Keyboard { get; set; }

		[JsonProperty("inline_keyboard")]
		public bool InlineKeyboard { get; set; }

		[JsonProperty("carousel")]
		public bool Carousel { get; set; }

		[JsonProperty("lang_id")]
		public int LangId { get; set; }
	}
}