﻿using DSFServices.DDL.Models;
using QNetZ;
using QNetZ.Attributes;
using QNetZ.Interfaces;
using System.Collections.Generic;

namespace DSFServices.Services
{
	[RMCService(RMCProtocolId.DriverG2WService)]
	public class DriverG2WService : RMCServiceBase
	{
		[RMCMethod(3)]
		public RMCResult UnlockG2W(List<UnlockInputData> unlocksIds, string table)
		{
			// apparently this does nothing

			return Error(0);
		}

		[RMCMethod(4)]
		public RMCResult UploadedReplay(string replayid, string title, string description)
		{
			UNIMPLEMENTED();
			return Error(0);
		}
	}
}
