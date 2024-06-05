@ECHO OFF
cd ..
cd source
rd ..\artifacts /s /q

REM Angular
cd frontend
call npm run build
cd ..

REM .NET
cd backend
dotnet publish WakeUpServer -c Release -r win-x64 --self-contained -o ../../artifacts/publish/winx64 /p:DebugType=None /p:DebugSymbols=false

pause