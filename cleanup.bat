RD /S /Q "Be.Windows.Forms.HexBox\obj"
RD /S /Q "Be.Windows.Forms.HexBox\bin\Debug"
DEL /Q /F /S "Be.Windows.Forms.HexBox\bin\Release\*.pdb"
RD /S /Q "DareDebuggerWV\obj"
RD /S /Q "DareDebuggerWV\bin\Debug"
DEL /Q /F /S "DareDebuggerWV\bin\Release\*.pdb"
DEL /Q /F /S "DareDebuggerWV\bin\Release\*.config"
DEL /Q /F /S "DareDebuggerWV\bin\Release\*.vshost.*"
RD /S /Q "DareParserWV\obj"
RD /S /Q "DareParserWV\bin\Debug"
DEL /Q /F /S "DareParserWV\bin\Release\*.pdb"
DEL /Q /F /S "DareParserWV\bin\Release\*.config"
DEL /Q /F /S "DareParserWV\bin\Release\*.vshost.*"
RD /S /Q "DTBReaderWV\obj"
RD /S /Q "DTBReaderWV\bin\Debug"
DEL /Q /F /S "DTBReaderWV\bin\Release\*.pdb"
DEL /Q /F /S "DTBReaderWV\bin\Release\*.config"
DEL /Q /F /S "DTBReaderWV\bin\Release\*.vshost.*"
RD /S /Q "GRP_Hook\AICLASS_PCClient_R\Release"
DEL /Q /F /S "GRP_Hook\Release\*.exp"
DEL /Q /F /S "GRP_Hook\Release\*.lib"
DEL /Q /F /S "GRP_Hook\Release\*.pdb"
RD /S /Q "GRPBackendWV\obj"
RD /S /Q "GRPBackendWV\bin\Debug"
DEL /Q /F /S "GRPBackendWV\bin\Release\*.pdb"
DEL /Q /F /S "GRPBackendWV\bin\Release\*.config"
DEL /Q /F /S "GRPBackendWV\bin\Release\*.vshost.*"
DEL /Q /F /S "GRPBackendWV\bin\Release\*.txt"
RD /S /Q "GRPExplorerWV\obj"
RD /S /Q "GRPExplorerWV\bin\Debug"
DEL /Q /F /S "GRPExplorerWV\bin\Release\*.pdb"
DEL /Q /F /S "GRPExplorerWV\bin\Release\*.config"
DEL /Q /F /S "GRPExplorerWV\bin\Release\*.vshost.*"
RD /S /Q "GRPMemoryToolWV\obj"
RD /S /Q "GRPMemoryToolWV\bin\Debug"
DEL /Q /F /S "GRPMemoryToolWV\bin\Release\*.pdb"
DEL /Q /F /S "GRPMemoryToolWV\bin\Release\*.config"
DEL /Q /F /S "GRPMemoryToolWV\bin\Release\*.vshost.*"
RD /S /Q "QuazalSharkWV\obj"
RD /S /Q "QuazalSharkWV\bin\Debug"
DEL /Q /F /S "QuazalSharkWV\bin\Release\*.pdb"
DEL /Q /F /S "QuazalSharkWV\bin\Release\*.config"
DEL /Q /F /S "QuazalSharkWV\bin\Release\*.vshost.*"
RD /S /Q "QuazalWV\obj"
RD /S /Q "QuazalWV\bin\Debug"
DEL /Q /F /S "QuazalWV\bin\Release\*.pdb"
DEL /Q /F /S "QuazalWV\bin\Release\*.config"
DEL /Q /F /S "QuazalWV\bin\Release\*.vshost.*"
RD /S /Q "GRPDedicatedServerWV\obj"
RD /S /Q "GRPDedicatedServerWV\bin\Debug"
DEL /Q /F /S "GRPDedicatedServerWV\bin\Release\*.pdb"
DEL /Q /F /S "GRPDedicatedServerWV\bin\Release\*.config"
DEL /Q /F /S "GRPDedicatedServerWV\bin\Release\*.vshost.*"
DEL /Q /F /S "GRPDedicatedServerWV\bin\Release\*.txt"
DEL /Q /F /S "GRPBackendWV.sdf"
RD /S /Q "ipch"
streams.exe -s -d
pause