﻿using DSFServices.DDL.Models;
using QNetZ;
using QNetZ.Attributes;
using QNetZ.DDL;
using QNetZ.Interfaces;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace DSFServices.Services
{
	/// <summary>
	/// Game session 
	///		Implements the sessions responsible for the gameplay process
	/// </summary>
	[RMCService(RMCProtocolId.GameSessionService)]
	public class GameSessionService : RMCServiceBase
	{
		static uint GameSessionCounter = 22110;

		[RMCMethod(1)]
		public RMCResult CreateSession(GameSession gameSession)
		{
			var plInfo = Context.Client.Info;
			var newSession = new GameSessionData();
			GameSessions.SessionList.Add(newSession);

			newSession.Id = ++GameSessionCounter;
			newSession.HostPID = plInfo.PID;
			newSession.TypeID = gameSession.m_typeID;

			foreach (var attr in gameSession.m_attributes)
				newSession.Attributes[attr.ID] = attr.Value;

			GameSessions.UpdateSessionParticipation(plInfo, newSession.Id, newSession.TypeID, true);

			// TODO: read values from current player gathering
			newSession.Attributes[(uint)GameSessionAttributeType.PublicSlots] = 0;
			newSession.Attributes[(uint)GameSessionAttributeType.PrivateSlots] = 8;
			newSession.Attributes[(uint)GameSessionAttributeType.FilledPublicSlots] = (uint)newSession.PublicParticipants.Count;
			newSession.Attributes[(uint)GameSessionAttributeType.FilledPrivateSlots] = (uint)newSession.Participants.Count;

			// TODO: give names to attributes
			newSession.Attributes[100] = 0;
			newSession.Attributes[101] = 0;
			newSession.Attributes[104] = 0;
			newSession.Attributes[113] = 0;

			// return key
			var result = new GameSessionKey();
			result.m_sessionID = newSession.Id;
			result.m_typeID = newSession.TypeID;

			return Result(result);
		}


		[RMCMethod(2)]
		public RMCResult UpdateSession(GameSessionUpdate gameSessionUpdate)
		{
			var session = GameSessions.SessionList
				.FirstOrDefault(x => x.Id == gameSessionUpdate.m_sessionKey.m_sessionID && 
									 x.TypeID == gameSessionUpdate.m_sessionKey.m_typeID);

			if(session != null)
			{
				// update or add attributes
				foreach (var attr in gameSessionUpdate.m_attributes)
				{
					session.Attributes[attr.ID] = attr.Value;
				}
			}
			else
			{
				QLog.WriteLine(1, $"Error : GameSessionService.UpdateSession - no session with id={gameSessionUpdate.m_sessionKey.m_sessionID}");
			}

			return Error(0);
		}


		[RMCMethod(3)]
		public RMCResult DeleteSession(GameSessionKey gameSessionKey)
		{
			UNIMPLEMENTED();
			return Error(0);
		}


		[RMCMethod(4)]
		public RMCResult MigrateSession(GameSessionKey gameSessionKey)
		{
			var srcSession = GameSessions.SessionList
				.FirstOrDefault(x => x.Id == gameSessionKey.m_sessionID &&
									 x.TypeID == gameSessionKey.m_typeID);

			var gameSessionKeyMigrated = new GameSessionKey();

			if (srcSession != null)
			{
				var plInfo = Context.Client.Info;
				var newSession = new GameSessionData();
				GameSessions.SessionList.Add(newSession);

				newSession.Id = ++GameSessionCounter;
				newSession.HostPID = plInfo.PID;
				newSession.TypeID = srcSession.TypeID;
				
				// move all participants too
				foreach (var pid in srcSession.PublicParticipants)
				{
					var participantPlInfo = NetworkPlayers.GetPlayerInfoByPID(pid);

					if(participantPlInfo != null)
					{
						participantPlInfo.GameData().CurrentSessionID = newSession.Id;
						//GameSessions.UpdateSessionParticipation(participantPlInfo, newSession.Id, newSession.TypeID, false);
					}
				}

				foreach (var pid in srcSession.Participants)
				{
					var participantPlInfo = NetworkPlayers.GetPlayerInfoByPID(pid);

					if (participantPlInfo != null)
					{
						participantPlInfo.GameData().CurrentSessionID = newSession.Id;
						//GameSessions.UpdateSessionParticipation(participantPlInfo, newSession.Id, newSession.TypeID, true);
					}
				}

				newSession.Participants = srcSession.Participants;
				newSession.PublicParticipants = srcSession.PublicParticipants;

				foreach (var attr in srcSession.Attributes)
					newSession.Attributes[attr.Key] = attr.Value;

				gameSessionKeyMigrated.m_sessionID = newSession.Id;
				gameSessionKeyMigrated.m_typeID = newSession.TypeID;
			}
			else
			{
				QLog.WriteLine(1, $"Error : GameSessionService.MigrateSession - no session with id={gameSessionKey.m_sessionID}");
			}

			return Result(gameSessionKeyMigrated);
		}


		[RMCMethod(5)]
		public RMCResult LeaveSession(GameSessionKey gameSessionKey)
		{
			// Same as AbandonSession
			var plInfo = Context.Client.Info;
			var myPlayerId = plInfo.PID;
			var session = GameSessions.SessionList
				.FirstOrDefault(x => x.Id == gameSessionKey.m_sessionID && 
									 x.TypeID == gameSessionKey.m_typeID);

			if(session != null)
			{
				GameSessions.UpdateSessionParticipation(plInfo, uint.MaxValue, uint.MaxValue, false);
			}
			else
			{
				QLog.WriteLine(1, $"Error : GameSessionService.LeaveSession - no session with id={gameSessionKey.m_sessionID}");
			}

			return Error(0);
		}


		[RMCMethod(6)]
		public RMCResult GetSession(GameSessionKey gameSessionKey)
		{
			var searchResult = new GameSessionSearchResult();

			var session = GameSessions.SessionList.FirstOrDefault(x => x.Id == gameSessionKey.m_sessionID && x.TypeID == gameSessionKey.m_typeID);

			if (session != null)
			{
				searchResult = new GameSessionSearchResult()
				{
					m_hostPID = session.HostPID,
					m_hostURLs = session.HostURLs,
					m_attributes = session.Attributes.Select(x => new GameSessionProperty { ID = x.Key, Value = x.Value}).ToArray(),
					m_sessionKey = new GameSessionKey()
					{
						m_sessionID = session.Id,
						m_typeID = session.TypeID
					}
				};
			}

			return Result(searchResult);
		}


		[RMCMethod(7)]
		public RMCResult SearchSessions(uint m_typeID, uint m_queryID, IEnumerable<GameSessionProperty> m_parameters)
		{
			// TODO: where to hold m_queryID??? Are there notifications?

			// orig response data: 09 01 00 00 2A 01 40 01 00 00 07 80 00 00 01 00 00 00 01 00 00 00 84 56 00 00 47 BD 05 00 02 00 00 00 3F 00 70 72 75 64 70 3A 2F 61 64 64 72 65 73 73 3D 31 39 32 2E 31 36 38 2E 31 2E 32 34 35 3B 70 6F 72 74 3D 33 30 37 34 3B 50 49 44 3D 33 37 36 31 33 35 3B 52 56 43 49 44 3D 31 37 31 30 30 36 00 4C 00 70 72 75 64 70 3A 2F 61 64 64 72 65 73 73 3D 39 32 2E 34 36 2E 31 33 31 2E 37 39 3B 70 6F 72 74 3D 31 30 32 34 3B 73 69 64 3D 31 35 3B 74 79 70 65 3D 33 3B 50 49 44 3D 33 37 36 31 33 35 3B 52 56 43 49 44 3D 31 37 31 30 30 36 00 0B 00 00 00 64 00 00 00 00 00 00 00 65 00 00 00 00 00 00 00 03 00 00 00 08 00 00 00 04 00 00 00 00 00 00 00 05 00 00 00 01 00 00 00 06 00 00 00 00 00 00 00 6C 00 00 00 00 00 00 00 69 00 00 00 9A 7E 09 00 6A 00 00 00 04 00 00 00 6D 00 00 00 02 00 00 00 71 00 00 00 00 00 00 00 

			var sessions = GameSessions.SessionList.Where(x => x.TypeID == m_typeID).ToArray();

			var resultList = new List<GameSessionSearchResult>();

			foreach (var ses in sessions)
			{
				// if all parameters match the found attributes, add as search result
				if (m_parameters.Any(p => ses.Attributes.Any(sa => p.ID == sa.Key && p.Value == sa.Value)))
				{
					resultList.Add(new GameSessionSearchResult()
					{
						m_hostPID = ses.HostPID,
						m_hostURLs = ses.HostURLs,
						m_attributes = ses.Attributes.Select(x => new GameSessionProperty { ID = x.Key, Value = x.Value }).ToArray(),
						m_sessionKey = new GameSessionKey()
						{
							m_sessionID = ses.Id,
							m_typeID = ses.TypeID
						},
					});
				}
			}

			return Result(resultList);
		}


		[RMCMethod(8)]
		public RMCResult AddParticipants(GameSessionKey gameSessionKey, IEnumerable<uint> publicParticipantIDs, IEnumerable<uint> privateParticipantIDs)
		{
			var session = GameSessions.SessionList
				.FirstOrDefault(x => x.Id == gameSessionKey.m_sessionID && 
									 x.TypeID == gameSessionKey.m_typeID);

			if(session != null)
			{
				foreach (var pid in publicParticipantIDs)
				{
					session.PublicParticipants.Add(pid);

					var player = NetworkPlayers.GetPlayerInfoByPID(pid);
					if (player != null)
						GameSessions.UpdateSessionParticipation(player, session.Id, session.TypeID, false);
				}

				foreach (var pid in privateParticipantIDs)
				{
					session.Participants.Add(pid);

					var player = NetworkPlayers.GetPlayerInfoByPID(pid);
					if (player != null)
						GameSessions.UpdateSessionParticipation(player, session.Id, session.TypeID, true);
				}

				session.Attributes[(uint)GameSessionAttributeType.FilledPublicSlots] = (uint)session.PublicParticipants.Count;
				session.Attributes[(uint)GameSessionAttributeType.FilledPrivateSlots] = (uint)session.Participants.Count;
			}
			else
			{
				QLog.WriteLine(1, $"Error : GameSessionService.AddParticipants - no session with id={gameSessionKey.m_sessionID}");
			}

			return Error(0);
		}


		[RMCMethod(9)]
		public RMCResult RemoveParticipants(GameSessionKey gameSessionKey, IEnumerable<uint> participantIDs)
		{
			var session = GameSessions.SessionList
				.FirstOrDefault(x => x.Id == gameSessionKey.m_sessionID &&
									 x.TypeID == gameSessionKey.m_typeID);

			if (session != null)
			{
				foreach (var pid in participantIDs)
				{
					var player = NetworkPlayers.GetPlayerInfoByPID(pid);
					if (player != null)
						GameSessions.UpdateSessionParticipation(player, uint.MaxValue, uint.MaxValue, false);
					else
						session.Participants.Remove(pid);
				}

				foreach (var pid in participantIDs)
				{
					var player = NetworkPlayers.GetPlayerInfoByPID(pid);
					if (player != null)
						GameSessions.UpdateSessionParticipation(player, uint.MaxValue, uint.MaxValue, false);
					else
						session.Participants.Remove(pid);
				}

				session.Attributes[(uint)GameSessionAttributeType.FilledPublicSlots] = (uint)session.PublicParticipants.Count;
				session.Attributes[(uint)GameSessionAttributeType.FilledPrivateSlots] = (uint)session.Participants.Count;
			}
			else
			{
				QLog.WriteLine(1, $"Error : GameSessionService.RemoveParticipants - no session with id={gameSessionKey.m_sessionID}");
			}

			return Error(0);
		}


		[RMCMethod(10)]
		public RMCResult GetParticipantCount(GameSessionKey gameSessionKey, IEnumerable<uint> participantIDs)
		{
			UNIMPLEMENTED();
			return Error(0);
		}


		[RMCMethod(11)]
		public void GetParticipants()
		{
			UNIMPLEMENTED();
		}


		[RMCMethod(12)]
		public void SendInvitation()
		{
			UNIMPLEMENTED();
		}


		[RMCMethod(13)]
		public void GetInvitationReceivedCount()
		{
			UNIMPLEMENTED();
		}


		[RMCMethod(14)]
		public void GetInvitationsReceived()
		{
			UNIMPLEMENTED();
		}


		[RMCMethod(15)]
		public void GetInvitationSentCount()
		{
			UNIMPLEMENTED();
		}


		[RMCMethod(16)]
		public void GetInvitationsSent()
		{
			UNIMPLEMENTED();
		}


		[RMCMethod(17)]
		public void AcceptInvitation()
		{
			UNIMPLEMENTED();
		}


		[RMCMethod(18)]
		public void DeclineInvitation()
		{
			UNIMPLEMENTED();
		}


		[RMCMethod(19)]
		public void CancelInvitation()
		{
			UNIMPLEMENTED();
		}


		[RMCMethod(20)]
		public void SendTextMessage()
		{
			UNIMPLEMENTED();
		}


		[RMCMethod(21)]
		public RMCResult RegisterURLs(IEnumerable<StationURL> stationURLs)
		{
			var plInfo = Context.Client.Info;
			var myPlayerId = plInfo.PID;
			var session = GameSessions.SessionList.FirstOrDefault(x => x.HostPID == myPlayerId);

			if (session != null)
			{
				session.HostURLs.Clear();
				session.HostURLs.AddRange(stationURLs);
			}
			else
			{
				QLog.WriteLine(1, $"Error : GameSessionService.RegisterURLs - no session hosted by pid={myPlayerId}");
			}

			return Error(0);
		}


		[RMCMethod(22)]
		public void JoinSession()
		{
			UNIMPLEMENTED();
		}


		[RMCMethod(23)]
		public RMCResult AbandonSession(GameSessionKey gameSessionKey)
		{
			return LeaveSession(gameSessionKey);
		}


		[RMCMethod(24)]
		public void SearchSessionsWithParticipants()
		{
			UNIMPLEMENTED();
		}


		[RMCMethod(25)]
		public void GetSessions()
		{
			UNIMPLEMENTED();
		}


		[RMCMethod(26)]
		public void GetParticipantsURLs()
		{
			UNIMPLEMENTED();
		}


		[RMCMethod(27)]
		public void MigrateSessionHost()
		{
			UNIMPLEMENTED();
		}


		[RMCMethod(28)]
		public void SplitSession()
		{
			UNIMPLEMENTED();
		}


		[RMCMethod(29)]
		public void SearchSocialSessions()
		{
			UNIMPLEMENTED();
		}


		[RMCMethod(30)]
		public void ReportUnsuccessfulJoinSessions()
		{
			UNIMPLEMENTED();
		}


	}
}
