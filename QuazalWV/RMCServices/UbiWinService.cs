﻿using QuazalWV.Attributes;
using QuazalWV.Interfaces;

namespace QuazalWV.RMCServices
{
	/// <summary>
	/// User friends service
	/// </summary>
	[RMCService(RMCP.PROTOCOL.UplayWinService)]
	class UbiWinService : RMCServiceBase
	{
		[RMCMethod(1)]
        public void GetActions()
		{
			UNIMPLEMENTED();
		}

		[RMCMethod(2)]
        public void GetActionsCompleted()
		{
			UNIMPLEMENTED();
		}

		[RMCMethod(3)]
        public void GetActionsCount()
		{
			UNIMPLEMENTED();
		}

		[RMCMethod(4)]
        public void GetActionsCompletedCount()
		{
			UNIMPLEMENTED();
		}

		[RMCMethod(5)]
        public void GetRewards()
		{
			UNIMPLEMENTED();
		}

		[RMCMethod(6)]
        public void GetRewardsPurchased()
		{
			UNIMPLEMENTED();
		}

		[RMCMethod(7)]
        public void UplayWelcome(string culture, string platformCode)
		{
			UNIMPLEMENTED();
		}

		[RMCMethod(8 )]
        public void SetActionCompleted()
		{
			UNIMPLEMENTED();
		}

		[RMCMethod(9)]
        public void SetActionsCompleted()
		{
			UNIMPLEMENTED();
		}

		[RMCMethod(10)]
        public void GetUserToken()
		{
			UNIMPLEMENTED();
		}

		[RMCMethod(11)]
        public void GetVirtualCurrencyUserBalance()
		{
			UNIMPLEMENTED();
		}

		[RMCMethod(12)]
		public void GetSectionsByKey()
		{
			UNIMPLEMENTED();
		}

	}
}
