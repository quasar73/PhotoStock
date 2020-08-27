using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoStock.Common
{
	public class JwtBearerTokenSettings
	{
		public string SecretKey { get; set; }
		public string Audience { get; set; }
		public string Issuer { get; set; }
		public int ExpiryTimeInSeconds { get; set; }
	}
}
