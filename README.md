# gcc-voice-migration-tool
Renames voice files from old Grand Chase client (2003-2015) so that they can used in Steam's rerelease (2021).

## options
```
-s, --source <source> (REQUIRED)
-c, --compare <compare> (REQUIRED)
-d, --destination <destination> (REQUIRED)
-l, --locale <locale>
```

## example usage (Batch)
```bat
:: Brazilian portuguese (old-school dub, unavailable in Steam)
GccVoiceMigrationTool.exe ^
	-s "C:\Source\br-classic" ^
	-c "C:\Source\br-kr-us" ^
	-d "C:\Destination\br-classic"

:: Brazilian portuguese (2012 redub)
GccVoiceMigrationTool.exe ^
	-s "C:\Source\br-kr-us" ^
	-c "C:\Source\br-kr-us" ^
	-d "C:\Destination\br" ^
	-l br

:: English
GccVoiceMigrationTool.exe ^
	-s "C:\Source\br-kr-us" ^
	-c "C:\Source\br-kr-us" ^
	-d "C:\Destination\us" ^
	-l us

:: Korean
GccVoiceMigrationTool.exe ^
	-s "C:\Source\br-kr-us" ^
	-c "C:\Source\br-kr-us" ^
	-d "C:\Destination\kr" ^
	-l "" kr &:: the objective of the empty string as first argument is to also include files that do not have a locale marker (they are in Korean by default)

:: Japanese (non-official dub, unavailable in Steam)
GccVoiceMigrationTool.exe ^
	-s "C:\Source\jp" ^
	-c "C:\Source\br-kr-us" ^
	-d "C:\Destination\jp"
```
