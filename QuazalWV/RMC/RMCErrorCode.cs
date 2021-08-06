﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
	public enum RMCErrorCode
	{
		Core_NoError = 0x00010001,
		Core_NotImplemented = 0x00010002,
		Core_InvalidPointer = 0x00010003,
		Core_OperationAborted = 0x00010004,
		Core_Exception = 0x00010005,
		Core_AccessDenied = 0x00010006,
		Core_InvalidHandle = 0x00010007,
		Core_InvalidIndex = 0x00010008,
		Core_OutOfMemory = 0x00010009,
		Core_InvalidArgument = 0x0001000A,
		Core_Timeout = 0x0001000B,
		Core_InitializationFailure = 0x0001000C,
		Core_CallInitiationFailure = 0x0001000D,
		Core_RegistrationError = 0x0001000E,
		Core_BufferOverflow = 0x0001000F,
		Core_InvalidLockState = 0x00010010,
		Core_InvalidSequence = 0x00010011,
		Core_SystemError = 0x00010012,
		Core_Cancelled = 0x00010013,

		DDL_InvalidSignature = 0x00020001,
		DDL_IncorrectVersion = 0x00020002,
	
		RendezVous_ConnectionFailure = 0x00030001,
		RendezVous_NotAuthenticated = 0x00030002,

		RendezVous_InvalidUsername = 0x00030064,
		RendezVous_InvalidPassword = 0x00030065,
		RendezVous_UsernameAlreadyExists = 0x00030066,
		RendezVous_AccountDisabled = 0x00030067,
		RendezVous_AccountExpired = 0x00030068,
		RendezVous_ConcurrentLoginDenied = 0x00030069,
		RendezVous_EncryptionFailure = 0x0003006A,
		RendezVous_InvalidPID = 0x0003006B,
		RendezVous_MaxConnectionsReached = 0x0003006C,
		RendezVous_InvalidGID = 0x0003006D,
		RendezVous_InvalidControlScriptID = 0x0003006E,
		RendezVous_InvalidOperationInLiveEnvironment = 0x0003006F,
		RendezVous_DuplicateEntry = 0x00030070,
		RendezVous_ControlScriptFailure = 0x00030071,
		RendezVous_ClassNotFound = 0x00030072,
		RendezVous_SessionVoid = 0x00030073,
		RendezVous_DDLMismatch = 0x00030075,
		RendezVous_InvalidConfiguration = 0x00030076,

		RendezVous_SessionFull = 0x000300C8,
		RendezVous_InvalidGatheringPassword = 0x000300C9,
		RendezVous_WithoutParticipationPeriod = 0x000300CA,
		RendezVous_PersistentGatheringCreationMax = 0x000300CB,
		RendezVous_PersistentGatheringParticipationMax = 0x000300CC,
		RendezVous_DeniedByParticipants = 0x000300CD,
		RendezVous_ParticipantInBlackList = 0x000300CE,
		RendezVous_GameServerMaintenance = 0x000300CF,
		RendezVous_OperationPostpone = 0x000300D0,
		RendezVous_OutOfRatingRange = 0x000300D1,
		RendezVous_ConnectionDisconnected = 0x000300D2,
		RendezVous_InvalidOperation = 0x000300D3,
		RendezVous_NotParticipatedGathering = 0x000300D4,
		RendezVous_MatchmakeSessionUserPasswordUnmatch = 0x000300D5,
		RendezVous_MatchmakeSessionSystemPasswordUnmatch = 0x000300D6,
		RendezVous_UserIsOffline = 0x000300D7,
		RendezVous_AlreadyParticipatedGathering = 0x000300D8,
		RendezVous_PermissionDenied = 0x000300D9,
		RendezVous_NotFriend = 0x000300DA,
		RendezVous_SessionClosed = 0x000300DB,
		RendezVous_DatabaseTemporarilyUnavailable = 0x000300DC,
		RendezVous_InvalidUniqueId = 0x000300DD,
		RendezVous_MatchmakingWithdrawn = 0x000300DE,
		RendezVous_LimitExceeded = 0x000300DF,
		RendezVous_AccountTemporarilyDisabled = 0x000300E0,
		RendezVous_PartiallyServiceClosed = 0x000300E1,
		RendezVous_ConnectionDisconnectedForConcurrentLogin = 0x000300E2,

		Transport_Unknown = 0x00050001,
		Transport_ConnectionFailure = 0x00050002,
		Transport_InvalidUrl = 0x00050003,
		Transport_InvalidKey = 0x00050004,
		Transport_InvalidURLType = 0x00050005,
		Transport_DuplicateEndpoint = 0x00050006,
		Transport_IOError = 0x00050007,
		Transport_Timeout = 0x00050008,
		Transport_ConnectionReset = 0x00050009,
		Transport_IncorrectRemoteAuthentication = 0x0005000A,
		Transport_ServerRequestError = 0x0005000B,
		Transport_DecompressionFailure = 0x0005000C,
		Transport_ReliableSendBufferFullFatal = 0x0005000D,
		Transport_UPnPCannotInit = 0x0005000E,
		Transport_UPnPCannotAddMapping = 0x0005000F,
		Transport_NatPMPCannotInit = 0x00050010,
		Transport_NatPMPCannotAddMapping = 0x00050011,
		Transport_UnsupportedNAT = 0x00050013,
		Transport_DnsError = 0x00050014,
		Transport_ProxyError = 0x00050015,
		Transport_DataRemaining = 0x00050016,
		Transport_NoBuffer = 0x00050017,
		Transport_NotFound = 0x00050018,
		Transport_TemporaryServerError = 0x00050019,
		Transport_PermanentServerError = 0x0005001A,
		Transport_ServiceUnavailable = 0x0005001B,
		Transport_ReliableSendBufferFull = 0x0005001C,
		Transport_InvalidStation = 0x0005001D,
		Transport_InvalidSubStreamID = 0x0005001E,
		Transport_PacketBufferFull = 0x0005001F,
		Transport_NatTraversalError = 0x00050020,
		Transport_NatCheckError = 0x00050021,

		DOCore_StationNotReached = 0x00060001,
		DOCore_TargetStationDisconnect = 0x00060002,
		DOCore_LocalStationLeaving = 0x00060003,
		DOCore_ObjectNotFound = 0x00060004,
		DOCore_InvalidRole = 0x00060005,
		DOCore_CallTimeout = 0x00060006,
		DOCore_RMCDispatchFailed = 0x00060007,
		DOCore_MigrationInProgress = 0x00060008,
		DOCore_NoAuthority = 0x00060009,
		DOCore_NoTargetStationSpecified = 0x0006000A,
		DOCore_JoinFailed = 0x0006000B,
		DOCore_JoinDenied = 0x0006000C,
		DOCore_ConnectivityTestFailed = 0x0006000D,
		DOCore_Unknown = 0x0006000E,
		DOCore_UnfreedReferences = 0x0006000F,
		DOCore_JobTerminationFailed = 0x00060010,
		DOCore_InvalidState = 0x00060011,
		DOCore_FaultRecoveryFatal = 0x00060012,
		DOCore_FaultRecoveryJobProcessFailed = 0x00060013,
		DOCore_StationInconsitency = 0x00060014,
		DOCore_AbnormalMasterState = 0x00060015,
		DOCore_VersionMismatch = 0x00060016,

		Authentication_NASAuthenticateError = 0x00680001,
		Authentication_TokenParseError = 0x00680002,
		Authentication_HttpConnectionError = 0x00680003,
		Authentication_HttpDNSError = 0x00680004,
		Authentication_HttpGetProxySetting = 0x00680005,
		Authentication_TokenExpired = 0x00680006,
		Authentication_ValidationFailed = 0x00680007,
		Authentication_InvalidParam = 0x00680008,
		Authentication_PrincipalIdUnmatched = 0x00680009,
		Authentication_MoveCountUnmatch = 0x0068000A,
		Authentication_UnderMaintenance = 0x0068000B,
		Authentication_UnsupportedVersion = 0x0068000C,
		Authentication_ServerVersionIsOld = 0x0068000D,
		Authentication_Unknown = 0x0068000E,
		Authentication_ClientVersionIsOld = 0x0068000F,
		Authentication_AccountLibraryError = 0x00680010,
		Authentication_ServiceNoLongerAvailable = 0x00680011,
		Authentication_UnknownApplication = 0x00680012,
		Authentication_ApplicationVersionIsOld = 0x00680013,
		Authentication_OutOfService = 0x00680014,
		Authentication_NetworkServiceLicenseRequired = 0x00680015,
		Authentication_NetworkServiceLicenseSystemError = 0x00680016,
		Authentication_NetworkServiceLicenseError3 = 0x00680017,
		Authentication_NetworkServiceLicenseError4 = 0x00680018,

	
	}
}
