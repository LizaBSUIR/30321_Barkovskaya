using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace _30321_BarkovskayaDomain.Models
{
	public class ResponseData<T>
	{
		// запрашиваемые данные
		public T Data { get; set; }
		// признак успешного завершения запроса
		public bool Success { get; set; } = true;
		// сообщение в случае неуспешного завершения
		public string? ErrorMessage { get; set; }
	}
}
